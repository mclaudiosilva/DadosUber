ALTER TABLE dados_do_dia
ADD COLUMN horas_trabalhadas DECIMAL(5,2) NULL AFTER valorAbastecido;

-- Opcional (se decidir remover do banco):
-- ALTER TABLE lancamento DROP COLUMN horarioInicio;
-- ALTER TABLE lancamento DROP COLUMN horarioFim;
