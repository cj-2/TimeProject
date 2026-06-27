-- Usuário 1 (admin)
WITH new_user AS (
    INSERT INTO users (name, email, role, timezone, is_active)
        VALUES ('Administrador Sistema',
                'admin@exemplo.com',
                0,
                'America/Sao_Paulo',
                true)
        RETURNING user_id)
INSERT
INTO user_passwords (password, is_active, user_id)
SELECT
    -- Senha: "Admin@123" hash bcrypt
    '$2b$12$wr16m61cU5cYCckd0Emw5OojzABGGjw598mvCSAUcoiShpKBbWmwW',
    true,
    user_id
FROM new_user;

-- Usuário 2 (usuário comum)
WITH new_user AS (
    INSERT INTO users (name, email, role, timezone, is_active)
        VALUES ('João Silva',
                'joao.silva@exemplo.com',
                1,
                'America/Sao_Paulo',
                true)
        RETURNING user_id)
INSERT
INTO user_passwords (password, is_active, user_id)
SELECT
    -- Senha: "Joao@123" hash bcrypt
    '$2b$12$k1L9fqfU6TWWNhMCMFTVX.2FAqKto3UpVpBFTMT1732uQ9Bq.Piei',
    true,
    user_id
FROM new_user;

-- Usuário 3 (usuário comum)
WITH new_user AS (
    INSERT INTO users (name, email, role, timezone, is_active)
        VALUES ('Maria Santos',
                'maria.santos@exemplo.com',
                1,
                'America/Sao_Paulo',
                true)
        RETURNING user_id)
INSERT
INTO user_passwords (password, is_active, user_id)
SELECT
    -- Senha: "Maria@123" hash bcrypt
    '$2b$12$deIce3H2pzVvTsF6vHRoTO6WUJr6f8fHPi2gKgDFdJuCMmULKDl56',
    true,
    user_id
FROM new_user;

-- Usuário 4 (usuário comum)
WITH new_user AS (
    INSERT INTO users (name, email, role, timezone, is_active)
        VALUES ('Pedro Oliveira',
                'pedro.oliveira@exemplo.com',
                1,
                'America/Sao_Paulo',
                true)
        RETURNING user_id)
INSERT
INTO user_passwords (password, is_active, user_id)
SELECT
    -- Senha: "Pedro@123" hash bcrypt
    '$2b$12$amhI3fw6WwCmnlBMU4qzxeqKJ69yh93lS6pfLIEfUOTKJdg29gFEC',
    true,
    user_id
FROM new_user;

-- Usuário 5 (usuário comum)
WITH new_user AS (
    INSERT INTO users (name, email, role, timezone, is_active)
        VALUES ('Ana Costa',
                'ana.costa@exemplo.com',
                1,
                'America/Sao_Paulo',
                true)
        RETURNING user_id)
INSERT
INTO user_passwords (password, is_active, user_id)
SELECT
    -- Senha: "Ana@12345" hash bcrypt
    '$2b$12$0dObXV8aRgzfN5b6/t9t9uKoi7eIU4Nimy97pxrIEXdo7.KWL40im',
    true,
    user_id
FROM new_user;

-- Verificação dos dados inseridos
SELECT u.user_id,
       u.name,
       u.email,
       u.role,
       u.timezone,
       u.is_active,
       u.created_at,
       up.password_id,
       up.is_active as password_active
FROM users u
         LEFT JOIN user_passwords up ON u.user_id = up.user_id
ORDER BY u.user_id;