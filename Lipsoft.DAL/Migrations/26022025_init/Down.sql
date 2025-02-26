-- Удаление индексов и таблиц в обратном порядке

-- Удаление индексов для таблицы OperatorCallResponsibilities
DROP INDEX idx_OperatorCallResponsibilities_OperatorId ON OperatorCallResponsibilities;
DROP INDEX idx_OperatorCallResponsibilities_CallId ON OperatorCallResponsibilities;

-- Удаление таблицы OperatorCallResponsibilities
DROP TABLE OperatorCallResponsibilities;

-- Удаление индексов для таблицы Operators
DROP INDEX idx_Operators_OperatorName ON Operators;

-- Удаление таблицы Operators
DROP TABLE Operators;

-- Удаление индексов для таблицы Calls
DROP INDEX idx_Calls_ScheduledDate ON Calls;
DROP INDEX idx_Calls_IsProcessed ON Calls;

-- Удаление таблицы Calls
DROP TABLE Calls;

-- Удаление индексов для таблицы CreditApplications
DROP INDEX idx_CreditApplications_CreditProductId ON CreditApplications;
DROP INDEX idx_CreditApplications_LoanAmount ON CreditApplications;

-- Удаление таблицы CreditApplications
DROP TABLE CreditApplications;

-- Удаление индексов для таблицы CreditProducts
DROP INDEX idx_CreditProducts_ProductName ON CreditProducts;

-- Удаление таблицы CreditProducts
DROP TABLE CreditProducts;

-- Удаление индексов для таблицы Clients
DROP INDEX idx_Clients_FullName ON Clients;
DROP INDEX idx_Clients_Phone ON Clients;

-- Удаление таблицы Clients
DROP TABLE Clients;