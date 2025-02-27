-- Удаление индексов и таблиц в обратном порядке

-- Удаление индексов для таблицы OperatorCallResponsibilities
DROP INDEX IF EXISTS idx_OperatorCallResponsibilities_OperatorId;
DROP INDEX IF EXISTS idx_OperatorCallResponsibilities_CallId;

-- Удаление таблицы OperatorCallResponsibilities
DROP TABLE IF EXISTS OperatorCallResponsibilities;

-- Удаление таблицы Operators
DROP TABLE IF EXISTS Operators;

-- Удаление индексов для таблицы Calls
DROP INDEX IF EXISTS idx_Calls_ScheduledDate;
DROP INDEX IF EXISTS idx_Calls_Status;

-- Удаление таблицы Calls
DROP TABLE IF EXISTS Calls;

-- Удаление индексов для таблицы CreditApplications
DROP INDEX IF EXISTS idx_CreditApplications_CreditProductId;

-- Удаление таблицы CreditApplications
DROP TABLE IF EXISTS CreditApplications;

-- Удаление таблицы CreditProducts
DROP TABLE IF EXISTS CreditProducts;

-- Удаление индексов для таблицы Clients
DROP INDEX IF EXISTS idx_Clients_Phone;

-- Удаление таблицы Clients
DROP TABLE IF EXISTS Clients;