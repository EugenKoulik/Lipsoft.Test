-- Таблица Clients
CREATE TABLE Clients (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Workplace VARCHAR(100) NOT NULL,
    Phone VARCHAR(20) NOT NULL
);

-- Индексы для таблицы Clients
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_Clients_Phone' AND object_id = OBJECT_ID('Clients'))
BEGIN
CREATE INDEX idx_Clients_Phone ON Clients (Phone);
END;

-- Таблица CreditProducts
CREATE TABLE CreditProducts (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    InterestRate DECIMAL(5,2) NOT NULL
);

-- Таблица CreditApplications
CREATE TABLE CreditApplications (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    LoanPurpose VARCHAR(200) NOT NULL,
    LoanAmount DECIMAL(18,2) NOT NULL,
    ClientIncome DECIMAL(18,2) NOT NULL,
    CreditProductId BIGINT NOT NULL, 
    FOREIGN KEY (CreditProductId) REFERENCES CreditProducts(Id)
);

-- Индексы для таблицы CreditApplications
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_CreditApplications_CreditProductId' AND object_id = OBJECT_ID('CreditApplications'))
BEGIN
CREATE INDEX idx_CreditApplications_CreditProductId ON CreditApplications (CreditProductId);
END;

-- Таблица Calls
CREATE TABLE Calls (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    ScheduledDate DATETIME NOT NULL,
    CallResult VARCHAR(200),
    Status VARCHAR(50)
);

-- Индексы для таблицы Calls
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_Calls_ScheduledDate' AND object_id = OBJECT_ID('Calls'))
BEGIN
CREATE INDEX idx_Calls_ScheduledDate ON Calls (ScheduledDate);
END;

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_Calls_Status' AND object_id = OBJECT_ID('Calls'))
BEGIN
CREATE INDEX idx_Calls_Status ON Calls (Status);
END;

-- Таблица Operators
CREATE TABLE Operators (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    OperatorName VARCHAR(100) NOT NULL
);

-- Таблица OperatorCallResponsibilities
CREATE TABLE OperatorCallResponsibilities (
    OperatorId BIGINT NOT NULL, 
    CallId BIGINT NOT NULL,     
    FOREIGN KEY (OperatorId) REFERENCES Operators(Id),
    FOREIGN KEY (CallId) REFERENCES Calls(Id)
);

-- Индексы для таблицы OperatorCallResponsibilities
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_OperatorCallResponsibilities_OperatorId' AND object_id = OBJECT_ID('OperatorCallResponsibilities'))
BEGIN
CREATE INDEX idx_OperatorCallResponsibilities_OperatorId ON OperatorCallResponsibilities (OperatorId);
END;

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'idx_OperatorCallResponsibilities_CallId' AND object_id = OBJECT_ID('OperatorCallResponsibilities'))
BEGIN
CREATE INDEX idx_OperatorCallResponsibilities_CallId ON OperatorCallResponsibilities (CallId);
END;