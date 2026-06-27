-- Usuário 1 (admin) - 8 categorias
INSERT INTO categories (name, user_id)
VALUES ('Trabalho Pessoal', 1),
       ('Estudos Avançados', 1),
       ('Exercício Físico', 1),
       ('Leitura Diária', 1),
       ('Descanso Programado', 1),
       ('Reuniões Importantes', 1),
       ('Projetos Pessoais', 1),
       ('Metas Mensais', 1)
ON CONFLICT (user_id, name) DO NOTHING;

-- Usuário 2 (João) - 6 categorias
INSERT INTO categories (name, user_id)
VALUES ('Trabalho Principal', 2),
       ('Estudos Técnicos', 2),
       ('Treino Academia', 2),
       ('Leitura Profissional', 2),
       ('Família', 2),
       ('Finanças Pessoais', 2)
ON CONFLICT (user_id, name) DO NOTHING;

-- Usuário 3 (Maria) - 7 categorias
INSERT INTO categories (name, user_id)
VALUES ('Trabalho Home Office', 3),
       ('Cursos Online', 3),
       ('Yoga e Meditação', 3),
       ('Leitura Literatura', 3),
       ('Projetos Criativos', 3),
       ('Saúde e Bem-estar', 3),
       ('Planejamento Semanal', 3)
ON CONFLICT (user_id, name) DO NOTHING;

-- Usuário 4 (Pedro) - 9 categorias
INSERT INTO categories (name, user_id)
VALUES ('Trabalho Corporativo', 4),
       ('Pós-graduação', 4),
       ('Corrida e Caminhada', 4),
       ('Leitura Técnica', 4),
       ('Projetos Freelance', 4),
       ('Investimentos', 4),
       ('Viagens', 4),
       ('Hobbies', 4),
       ('Voluntariado', 4)
ON CONFLICT (user_id, name) DO NOTHING;

-- Usuário 5 (Ana) - 5 categorias
INSERT INTO categories (name, user_id)
VALUES ('Trabalho Remoto', 5),
       ('Idiomas', 5),
       ('Pilates', 5),
       ('Leitura Desenvolvimento', 5),
       ('Projetos Sociais', 5)
ON CONFLICT (user_id, name) DO NOTHING;

-- Adicionando categorias padrão que todos devem ter
INSERT INTO categories (name, user_id)
SELECT 'Geral', user_id
FROM users
WHERE is_active = true
ON CONFLICT (user_id, name) DO NOTHING;

INSERT INTO categories (name, user_id)
SELECT 'Sem Categoria', user_id
FROM users
WHERE is_active = true
ON CONFLICT (user_id, name) DO NOTHING;