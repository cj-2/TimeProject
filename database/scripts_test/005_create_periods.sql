/* 
 Esse script foi gerado com o auxilio de IA.
 */

CREATE OR REPLACE FUNCTION gerar_periodos_usuario(
    p_user_id INT,
    p_min_periodos INT DEFAULT 300,
    p_max_periodos INT DEFAULT 1000
)
    RETURNS VOID AS
$$
DECLARE
    v_total_periodos   INT;
    v_contador         INT := 0;
    v_data_base        TIMESTAMPTZ;
    v_data_atual       DATE;
    v_hora_inicio      TIME;
    v_hora_fim         TIME;
    v_duracao_minutos  INT;
    v_record_id        INT;
    v_category_id      INT;
    v_session_id       INT;
    v_tem_record       BOOLEAN;
    v_tem_categoria    BOOLEAN;
    v_tem_sessao       BOOLEAN;
    v_intervalo_dias   INT;
    v_timestamp_sessao TIMESTAMPTZ;
    v_start_period     TIMESTAMPTZ;
    v_end_period       TIMESTAMPTZ;
BEGIN
    -- Número aleatório de períodos para este usuário
    v_total_periodos := floor(random() * (p_max_periodos - p_min_periodos + 1)) + p_min_periodos;

    RAISE NOTICE 'Gerando % períodos para usuário ID: %', v_total_periodos, p_user_id;

    -- Data base (últimos 6 meses)
    v_data_base := CURRENT_TIMESTAMP - (floor(random() * 180) || ' days')::INTERVAL;

    -- Para cada período a ser criado
    WHILE v_contador < v_total_periodos
        LOOP
            -- Determinar se este período terá record, categoria, sessão ou nenhum
            v_tem_record := random() < 0.7; -- 70% com record
            v_tem_categoria := random() < 0.6; -- 60% com categoria
            v_tem_sessao := random() < 0.4;
            -- 40% com sessão

            -- Selecionar record aleatório do usuário (se existir e se for ter record)
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

            -- Selecionar categoria aleatória (se existir e se for ter categoria)
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
                -- Gerar timestamp para a sessão
                v_timestamp_sessao := CURRENT_TIMESTAMP - (floor(random() * 180) || ' days')::INTERVAL;

                INSERT INTO sessions (type, date, session_from, user_id, record_id, category_id)
                VALUES (floor(random() * 4)::INT, -- tipo entre 0-3
                        v_timestamp_sessao,
                        CASE floor(random() * 4)
                            WHEN 0 THEN 'web'
                            WHEN 1 THEN 'mobile'
                            WHEN 2 THEN 'desktop'
                            ELSE 'api'
                            END,
                        p_user_id,
                        v_record_id,
                        v_category_id)
                RETURNING session_id INTO v_session_id;
            END IF;

            -- Calcular data e horário para o período
            v_intervalo_dias := floor(random() * 180); -- Últimos 180 dias
            v_data_atual := CURRENT_DATE - v_intervalo_dias;

            -- Horário de início (entre 6h e 22h)
            v_hora_inicio := ((floor(random() * 16 + 6) || ' hours')::INTERVAL +
                              (floor(random() * 60) || ' minutes')::INTERVAL +
                              (floor(random() * 60) || ' seconds')::INTERVAL)::TIME;

            -- Duração do período (entre 5 minutos e 4 horas) - GARANTIR que seja positivo
            v_duracao_minutos := floor(random() * 235) + 5;
            -- 5-240 minutos

            -- Calcular start_period
            v_start_period := (v_data_atual + v_hora_inicio)::TIMESTAMPTZ;

            -- Calcular end_period se não for NULL
            IF random() < 0.95 THEN
                -- Garantir que end_period > start_period
                v_end_period := v_start_period + (v_duracao_minutos || ' minutes')::INTERVAL;

                -- VERIFICAÇÃO EXTRA: garantir que end > start
                IF v_end_period <= v_start_period THEN
                    -- Se por algum motivo end não for maior, adicionar 1 minuto
                    v_end_period := v_start_period + INTERVAL '1 minute';
                END IF;
            ELSE
                v_end_period := NULL; -- 5% dos períodos sem fim (em andamento)
            END IF;

            -- Inserir o período com validação
            BEGIN
                INSERT INTO periods (start_period,
                                     end_period,
                                     user_id,
                                     record_id,
                                     session_id,
                                     category_id)
                VALUES (v_start_period,
                        v_end_period,
                        p_user_id,
                        v_record_id,
                        v_session_id,
                        v_category_id);
            EXCEPTION
                WHEN check_violation THEN
                    -- Se houver violação da constraint, tentar corrigir
                    RAISE NOTICE 'Corrigindo período inválido para usuário %', p_user_id;

                    -- Se end_period não for NULL e não for maior que start, ajustar
                    IF v_end_period IS NOT NULL AND v_end_period <= v_start_period THEN
                        v_end_period := v_start_period + INTERVAL '5 minutes';

                        INSERT INTO periods (start_period,
                                             end_period,
                                             user_id,
                                             record_id,
                                             session_id,
                                             category_id)
                        VALUES (v_start_period,
                                v_end_period,
                                p_user_id,
                                v_record_id,
                                v_session_id,
                                v_category_id);
                    END IF;
            END;

            v_contador := v_contador + 1;

            -- Progresso a cada 100 inserções
            IF v_contador % 100 = 0 THEN
                RAISE NOTICE 'Usuário %: % períodos criados...', p_user_id, v_contador;
            END IF;
        END LOOP;

    RAISE NOTICE 'Concluído: % períodos criados para usuário %', v_contador, p_user_id;
END;
$$ LANGUAGE plpgsql;


DO
$$
    DECLARE
        usuario RECORD;
    BEGIN
        FOR usuario IN SELECT user_id FROM users WHERE is_active = true ORDER BY user_id
            LOOP
                -- Chamar a função para gerar períodos
                PERFORM gerar_periodos_usuario(usuario.user_id, 200, 600); -- Teste com menos períodos primeiro
            END LOOP;

        RAISE NOTICE 'Todos os períodos foram gerados com sucesso!';
    END
$$;

-- Remover a função auxiliar se não for mais necessária
DROP FUNCTION gerar_periodos_usuario(INT, INT, INT);