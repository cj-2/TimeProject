drop table if exists minutes;
drop table if exists periods;
drop table if exists sessions;
drop table if exists record_resumes;
drop table if exists records;
drop table if exists categories;
drop table if exists confirm_codes;
drop table if exists user_access_logs;
drop table if exists user_providers;
drop table if exists user_passwords;
drop table if exists users;

/* functions */
create or replace function update_timestamp()
    returns trigger as
$$
begin
    new.updated_at = now();
    return new;
end;
$$ language plpgsql;

/* users */
create table if not exists users
(
    user_id    serial,
    name       varchar(120) not null,
    email      varchar(64)  not null unique,
    role       int          not null,
    timezone   varchar(64)  not null,
    is_active  bool         not null,
    created_at timestamptz  not null default now(),
    updated_at timestamptz  not null default now(),
    constraint pk_users primary key (user_id)
);

create index idx_users_email on users (email);

create or replace trigger users_update_timestamp_trigger
    before update
    on users
    for each row
execute procedure update_timestamp();

/* user_passwords */
create table if not exists user_passwords
(
    password_id serial,
    password    varchar(72) not null,
    is_active   bool        not null,
    user_id     int         not null,
    created_at  timestamptz not null default now(),
    updated_at  timestamptz not null default now(),
    constraint pk_user_passwords primary key (password_id),
    constraint fk_user_passwords_users foreign key (user_id) references users (user_id) on delete cascade
);

create index idx_user_passwords_user_id on user_passwords (user_id);

create or replace trigger user_passwords_update_timestamp_trigger
    before update
    on user_passwords
    for each row
execute procedure update_timestamp();

/* user_providers */
create table if not exists user_providers
(
    provider_id serial,
    provider    int         not null,
    external_id varchar(72) not null,
    user_id     int         not null,
    created_at  timestamptz not null default now(),
    updated_at  timestamptz not null default now(),
    constraint pk_user_providers primary key (provider_id),
    constraint fk_user_providers_users foreign key (user_id) references users (user_id) on delete cascade,
    constraint uq_user_providers_user_id_provider unique (user_id, provider)
);

create or replace trigger user_providers_update_timestamp_trigger
    before update
    on user_providers
    for each row
execute procedure update_timestamp();

/* user_access_logs */
create table if not exists user_access_logs
(
    log_id      serial,
    type        int         not null,
    client_ip   inet        not null,
    user_agent  text        not null,
    provider    int,
    user_id     int         not null,
    accessed_at timestamptz not null default now(),
    created_at  timestamptz not null default now(),
    updated_at  timestamptz not null default now(),
    constraint pk_user_access_logs primary key (log_id),
    constraint fk_user_access_logs_users foreign key (user_id) references users (user_id) on delete set null
);

create index idx_user_access_logs_user_id on user_access_logs (user_id);

create or replace trigger user_access_logs_update_timestamp_trigger
    before update
    on user_access_logs
    for each row
execute procedure update_timestamp();

/* confirm_codes */
create table if not exists confirm_codes
(
    code_id    uuid        not null default gen_random_uuid(),
    type       int         not null,
    is_used    bool        not null default false,
    was_sent   bool        not null default false,
    expiration timestamptz not null,
    user_id    int         not null,
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now(),
    constraint pk_confirm_codes primary key (code_id),
    constraint fk_confirm_codes_users foreign key (user_id) references users (user_id) on delete cascade
);

create index idx_confirm_codes_user_id_type on confirm_codes (user_id, type);

create or replace trigger confirm_codes_update_timestamp_trigger
    before update
    on confirm_codes
    for each row
execute procedure update_timestamp();

/* categories */
create table if not exists categories
(
    category_id serial,
    name        varchar(64) not null,
    user_id     int         not null,
    created_at  timestamptz not null default now(),
    updated_at  timestamptz not null default now(),
    constraint pk_categories primary key (category_id),
    constraint fk_categories_users foreign key (user_id) references users (user_id) on delete cascade,
    constraint uq_categories_users_name unique (user_id, name)
);

create or replace trigger categories_update_timestamp_trigger
    before update
    on categories
    for each row
execute procedure update_timestamp();

/* records */
create table if not exists records
(
    record_id     serial,
    code          varchar(36) not null default gen_random_uuid(),
    name          varchar(120),
    description   varchar(240),
    external_link varchar(120),
    user_id       int         not null,
    category_id   int,
    created_at    timestamptz not null default now(),
    updated_at    timestamptz not null default now(),
    constraint pk_records primary key (record_id),
    constraint fk_records_users foreign key (user_id) references users (user_id) on delete cascade,
    constraint fk_records_categories foreign key (category_id) references categories (category_id) on delete set null
);

create index idx_records_user_id_code on records (user_id, code);
create index idx_records_code on records (code);

create or replace trigger records_update_timestamp_trigger
    before update
    on records
    for each row
execute procedure update_timestamp();

/* record_resumes */
create table if not exists record_resumes
(
    record_id  int         not null unique,
    seconds    float8 check (seconds >= 0),
    formatted  varchar(24),
    first_date timestamptz          default now(),
    last_date  timestamptz          default now(),
    count      int4 check (count >= 0),
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now(),
    constraint pk_record_resumes primary key (record_id),
    constraint fk_record_resumes_records foreign key (record_id) references records (record_id) on delete cascade
);

create or replace trigger record_resumes_update_timestamp_trigger
    before update
    on record_resumes
    for each row
execute procedure update_timestamp();

/* sessions */
create table if not exists sessions
(
    session_id   serial,
    type         int         not null,
    date         timestamptz not null default now(),
    session_from varchar(20),
    user_id      int,
    record_id    int         not null,
    created_at   timestamptz not null default now(),
    updated_at   timestamptz not null default now(),
    constraint pk_sessions primary key (session_id),
    constraint fk_sessions_users foreign key (user_id) references users (user_id) on delete set null,
    constraint fk_sessions_records foreign key (record_id) references records (record_id) on delete cascade
);

create index idx_sessions_user_id on sessions (user_id);
create index idx_sessions_record_id on sessions (record_id);

create or replace trigger sessions_update_timestamp_trigger
    before update
    on sessions
    for each row
execute procedure update_timestamp();

/* periods */
create table if not exists periods
(
    period_id    serial,
    start_period timestamptz not null,
    end_period   timestamptz not null check (end_period > start_period),
    user_id      int,
    record_id    int         not null,
    session_id   int         not null,
    created_at   timestamptz not null default now(),
    updated_at   timestamptz not null default now(),
    constraint pk_periods primary key (period_id),
    constraint fk_periods_users foreign key (user_id) references users (user_id) on delete set null,
    constraint fk_periods_records foreign key (record_id) references records (record_id) on delete cascade,
    constraint fk_periods_sessions foreign key (session_id) references sessions (session_id) on delete cascade
);

create index idx_periods_user_id on periods (user_id);
create index idx_periods_record_id on periods (record_id);
create index idx_periods_session_id on periods (session_id);

create or replace trigger periods_update_timestamp_trigger
    before update
    on periods
    for each row
execute procedure update_timestamp();

/* minutes */
create table if not exists minutes
(
    minute_id  serial,
    date       timestamptz not null,
    total      int         not null check (total >= 0),
    user_id    int,
    record_id  int         not null,
    session_id int         not null,
    created_at timestamptz not null default now(),
    updated_at timestamptz not null default now(),
    constraint pk_minutes primary key (minute_id),
    constraint fk_minutes_users foreign key (user_id) references users (user_id) on delete set null,
    constraint fk_minutes_records foreign key (record_id) references records (record_id) on delete cascade,
    constraint fk_minutes_sessions foreign key (session_id) references sessions (session_id) on delete cascade
);

create index idx_minutes_user_id on minutes (user_id);
create index idx_minutes_record_id on minutes (record_id);
create index idx_minutes_session_id on minutes (session_id);

create or replace trigger minutes_update_timestamp_trigger
    before update
    on minutes
    for each row
execute procedure update_timestamp();