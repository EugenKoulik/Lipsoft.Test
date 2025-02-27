-- Удаление индексов для таблицы OperatorCallResponsibilities
IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_OperatorCallResponsibilities_OperatorId' AND object_id = OBJECT_ID('OperatorCallResponsibilities'))
BEGIN
DROP INDEX idx_OperatorCallResponsibilities_OperatorId ON OperatorCallResponsibilities;
END

IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_OperatorCallResponsibilities_CallId' AND object_id = OBJECT_ID('OperatorCallResponsibilities'))
BEGIN
DROP INDEX idx_OperatorCallResponsibilities_CallId ON OperatorCallResponsibilities;
END

-- Удаление таблицы OperatorCallResponsibilities
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'OperatorCallResponsibilities')
BEGIN
DROP TABLE OperatorCallResponsibilities;
END

-- Удаление таблицы Operators
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'Operators')
BEGIN
DROP TABLE Operators;
END

-- Удаление индексов для таблицы Calls
IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_Calls_ScheduledDate' AND object_id = OBJECT_ID('Calls'))
BEGIN
DROP INDEX idx_Calls_ScheduledDate ON Calls;
END

IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_Calls_Status' AND object_id = OBJECT_ID('Calls'))
BEGIN
DROP INDEX idx_Calls_Status ON Calls;
END

-- Удаление таблицы Calls
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'Calls')
BEGIN
DROP TABLE Calls;
END

-- Удаление индексов для таблицы CreditApplications
IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_CreditApplications_CreditProductId' AND object_id = OBJECT_ID('CreditApplications'))
BEGIN
DROP INDEX idx_CreditApplications_CreditProductId ON CreditApplications;
END

-- Удаление таблицы CreditApplications
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'CreditApplications')
BEGIN
DROP TABLE CreditApplications;
END

-- Удаление таблицы CreditProducts
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'CreditProducts')
BEGIN
DROP TABLE CreditProducts;
END

-- Удаление индексов для таблицы Clients
IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_Clients_Phone' AND object_id = OBJECT_ID('Clients'))
BEGIN
DROP INDEX idx_Clients_Phone ON Clients;
END

-- Удаление таблицы Clients
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'Clients')
BEGIN
DROP TABLE Clients;
END