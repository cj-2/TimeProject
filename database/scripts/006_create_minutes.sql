/* 
 Esse script foi gerado com o auxilio de IA.
 */

CREATE OR REPLACE FUNCTION gerar_minutes_usuario(
    p_user_id INT,
    p_min_minutes INT DEFAULT 500,
    p_max_minutes INT DEFAULT 2000
)
    RETURNS VOID AS
$$
DECLARE
    v_total_minutes INT;
    v_contador      INT := 0;
    v_data_base     TIMESTAMPTZ;
    v_minute_date   TIMESTAMPTZ;
    v_total_seconds INT;
    v_record_id     INT;
    v_category_id   INT;
    v_session_id    INT;
    v_tem_record    BOOLEAN;
    v_tem_categoria BOOLEAN;
    v_tem_sessao    BOOLEAN;
    v_session_type  INT;
    v_session_from  VARCHAR(20);
BEGIN
    -- Número aleatório de minutes para este usuário
    v_total_minutes := floor(random() * (p_max_minutes - p_min_minutes + 1)) + p_min_minutes;

    RAISE NOTICE 'Gerando % minutes para usuário ID: %', v_total_minutes, p_user_id;

    -- Data base (últimos 180 dias)
    v_data_base := CURRENT_TIMESTAMP - (floor(random() * 180) || ' days')::INTERVAL;

    -- Para cada minute a ser criado
    WHILE v_contador < v_total_minutes
        LOOP
            -- Determinar se este minute terá record, categoria, sessão ou nenhum
            v_tem_record := random() < 0.7; -- 70% com record
            v_tem_categoria := random() < 0.6; -- 60% com categoria
            v_tem_sessao := random() < 0.3;
            -- 30% com sessão

            -- Selecionar record aleatório do usuário
            IF v_tem_record THEN
                SELECT record_id
                INTO v_record_id
                FROM records
                WHERE user_id = p_user_id
                ORDER BY random()
                LIMIT 1;
            ELSE
                v_record_id := NULL;
            END IF;

            -- Selecionar categoria aleatória
            IF v_tem_categoria THEN
                SELECT category_id
                INTO v_category_id
                FROM categories
                WHERE user_id = p_user_id
                ORDER BY random()
                LIMIT 1;
            ELSE
                v_category_id := NULL;
            END IF;

            -- Criar sessão se necessário
            v_session_id := NULL;
            IF v_tem_sessao THEN
                v_session_type := floor(random() * 4)::INT;
                v_session_from := CASE floor(random() * 4)
                                      WHEN 0 THEN 'web'
                                      WHEN 1 THEN 'mobile'
                                      WHEN 2 THEN 'desktop'
                                      ELSE 'api'
                    END;

                -- Data da sessão (últimos 180 dias)
                v_minute_date := CURRENT_TIMESTAMP - (floor(random() * 180 * 86400) || ' seconds')::INTERVAL;

                INSERT INTO sessions (type, date, session_from, user_id, record_id, category_id)
                VALUES (v_session_type,
                        v_minute_date,
                        v_session_from,
                        p_user_id,
                        v_record_id,
                        v_category_id)
                RETURNING session_id INTO v_session_id;
            END IF;

            -- Gerar data para o minute (últimos 180 dias, arredondado para minuto)
            v_minute_date := DATE_TRUNC('minute',
                                        CURRENT_TIMESTAMP - (floor(random() * 180 * 86400) || ' seconds')::INTERVAL
                             );

            -- Total de segundos neste minuto (0-60)
            v_total_seconds := floor(random() * 61);

            -- Inserir o minute
            INSERT INTO minutes (date,
                                 total,
                                 user_id,
                                 record_id,
                                 session_id,
                                 category_id)
            VALUES (v_minute_date,
                    v_total_seconds,
                    p_user_id,
                    v_record_id,
                    v_session_id,
                    v_category_id);

            v_contador := v_contador + 1;

            -- Progresso a cada 100 inserções
            IF v_contador % 100 = 0 THEN
                RAISE NOTICE 'Usuário %: % minutes criados...', p_user_id, v_contador;
            END IF;
        END LOOP;

    RAISE NOTICE 'Concluído: % minutes criados para usuário %', v_contador, p_user_id;
END;
$$ LANGUAGE plpgsql;

-- Executar a função para todos os usuários ativos
DO
$$
    DECLARE
        usuario RECORD;
    BEGIN

        FOR usuario IN SELECT user_id FROM users WHERE is_active = true ORDER BY user_id
            LOOP
                -- Gerar entre 500-2000 minutes por usuário
                PERFORM gerar_minutes_usuario(usuario.user_id, 300, 1000);
            END LOOP;

        RAISE NOTICE 'Todos os minutes foram gerados com sucesso!';
    END
$$;

-- Remover a função se não for mais necessária
DROP FUNCTION gerar_minutes_usuario(INT, INT, INT);