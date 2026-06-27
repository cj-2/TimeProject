-- Records para o Administrador (usuário 1)
INSERT INTO records (name, description, external_link, user_id, category_id)
SELECT name,
       description,
       external_link,
       1,
       (SELECT category_id
        FROM categories
        WHERE user_id = 1
          AND name LIKE '%' || category || '%'
        LIMIT 1) as category_id
FROM (VALUES ('Gestão Sistema', 'Monitoramento e manutenção do sistema principal', 'https://dashboard.sistema.com',
              'Trabalho'),
             ('Backup Diário', 'Verificação e execução de backup automático', NULL, 'Projetos'),
             ('Revisão Segurança', 'Análise de logs e vulnerabilidades', 'https://security.sistema.com', 'Metas'),
             ('Planejamento Capacidade', 'Previsão de recursos para próximo trimestre', NULL, 'Organização'),
             ('Relatório Mensal', 'Geração de relatório de performance', 'https://reports.sistema.com', 'Financeiro'),
             ('Atualização Documentação', 'Revisão e atualização da documentação técnica', 'https://wiki.sistema.com',
              'Estudo')) AS t(name, description, external_link, category);

-- Records para João Silva (usuário 2)
INSERT INTO records (name, description, external_link, user_id, category_id)
SELECT name,
       description,
       external_link,
       2,
       (SELECT category_id FROM categories WHERE user_id = 2 ORDER BY random() LIMIT 1) as category_id
FROM (VALUES ('Desenvolvimento Frontend', 'Criação de componentes React para nova feature',
              'https://github.com/projeto/frontend', NULL),
             ('Code Review', 'Revisão de PRs do time', 'https://github.com/projeto/pulls', NULL),
             ('Daily Meeting', 'Reunião diária com o time', 'https://meet.google.com/daily', NULL),
             ('Testes Unitários', 'Escrita de testes para módulo de autenticação', NULL, NULL),
             ('Estudo TypeScript', 'Aprofundamento em tipos avançados', 'https://typescriptlang.org/docs',
              NULL)) AS t(name, description, external_link, category);

-- Records para Maria Santos (usuário 3)
INSERT INTO records (name, description, external_link, user_id, category_id)
SELECT name,
       description,
       external_link,
       3,
       (SELECT category_id FROM categories WHERE user_id = 3 ORDER BY random() LIMIT 1) as category_id
FROM (VALUES ('Design UI/UX', 'Criação de protótipo para nova funcionalidade', 'https://figma.com/projeto', NULL),
             ('Pesquisa Usuários', 'Análise de feedback e entrevistas', NULL, NULL),
             ('Workshop Acessibilidade', 'Preparação de material sobre WCAG', 'https://drive.google.com/workshop',
              NULL),
             ('Leitura Design Systems', 'Capítulo sobre design tokens', 'https://designsystemsbook.com', NULL),
             ('Yoga Matinal', 'Sessão de 20 minutos para começar o dia', NULL, NULL),
             ('Planejimento Sprint', 'Definição de tarefas para próxima sprint', NULL,
              NULL)) AS t(name, description, external_link, category);

-- Records para Pedro Oliveira (usuário 4)
INSERT INTO records (name, description, external_link, user_id, category_id)
SELECT name,
       description,
       external_link,
       4,
       (SELECT category_id FROM categories WHERE user_id = 4 ORDER BY random() LIMIT 1) as category_id
FROM (VALUES ('Análise Dados', 'Processamento de dataset para relatório', 'https://powerbi.com/relatorio', NULL),
             ('Modelagem Banco', 'Criação de novo schema para feature', NULL, NULL),
             ('ETL Pipeline', 'Otimização de pipeline de dados', 'https://airflow.dados.com', NULL),
             ('Aula Pós-graduação', 'Preparação para aula de Big Data', 'https://moodle.uni.edu.br', NULL),
             ('Corrida Parque', 'Treino de 5km no parque da cidade', NULL, NULL),
             ('Estudo Machine Learning', 'Revisão de algoritmos de classificação', 'https://coursera.org/ml', NULL),
             ('Revisão Investimentos', 'Análise de carteira de ações', 'https://corretora.com.br',
              NULL)) AS t(name, description, external_link, category);

-- Records para Ana Costa (usuário 5)
INSERT INTO records (name, description, external_link, user_id, category_id)
SELECT name,
       description,
       external_link,
       5,
       (SELECT category_id FROM categories WHERE user_id = 5 ORDER BY random() LIMIT 1) as category_id
FROM (VALUES ('Tradução Documentação', 'Tradução de docs para português', 'https://crowdin.com/projeto', NULL),
             ('Reunião Clientes', 'Apresentação de novas funcionalidades', 'https://zoom.us/reuniao', NULL),
             ('Prática Espanhol', 'Sessão de conversação com nativo', 'https://italki.com/session', NULL),
             ('Redação Artigo', 'Escrita de artigo sobre comunicação técnica', 'https://medium.com/draft', NULL),
             ('Pilates Studio', 'Aula de pilates com instrutor', NULL, NULL),
             ('Organização Home Office', 'Arrumação do espaço de trabalho remoto', NULL,
              NULL)) AS t(name, description, external_link, category);