-- Таблица Clients
CREATE TABLE IF NOT EXISTS Clients (
    Id BIGSERIAL PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Workplace VARCHAR(100) NOT NULL,
    Phone VARCHAR(20) NOT NULL
);

-- Индексы для таблицы Clients
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_Clients_Phone ON Clients (Phone);

-- Таблица CreditProducts
CREATE TABLE IF NOT EXISTS CreditProducts (
    Id BIGSERIAL PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    InterestRate DECIMAL(5,2) NOT NULL
);

-- Таблица CreditApplications
CREATE TABLE IF NOT EXISTS CreditApplications (
    Id BIGSERIAL PRIMARY KEY,
    LoanPurpose VARCHAR(200) NOT NULL,
    LoanAmount DECIMAL(18,2) NOT NULL,
    ClientIncome DECIMAL(18,2) NOT NULL,
    CreditProductId INT NOT NULL,
    FOREIGN KEY (CreditProductId) REFERENCES CreditProducts(Id)
);

-- Индексы для таблицы CreditApplications
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_CreditApplications_CreditProductId ON CreditApplications (CreditProductId);

-- Таблица Calls
CREATE TABLE IF NOT EXISTS Calls (
    Id BIGSERIAL PRIMARY KEY,
    ScheduledDate TIMESTAMP NOT NULL,
    CallResult VARCHAR(200),
    Status VARCHAR(50)
);

-- Индексы для таблицы Calls
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_Calls_ScheduledDate ON Calls (ScheduledDate);
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_Calls_Status ON Calls (Status);

-- Таблица Operators
CREATE TABLE IF NOT EXISTS Operators (
    Id BIGSERIAL PRIMARY KEY,
    OperatorName VARCHAR(100) NOT NULL
);

-- Таблица OperatorCallResponsibilities
CREATE TABLE IF NOT EXISTS OperatorCallResponsibilities (
    OperatorId INT NOT NULL,
    CallId INT NOT NULL,
    FOREIGN KEY (OperatorId) REFERENCES Operators(Id),
    FOREIGN KEY (CallId) REFERENCES Calls(Id)
);

-- Индексы для таблицы OperatorCallResponsibilities
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_OperatorCallResponsibilities_OperatorId ON OperatorCallResponsibilities (OperatorId);
CREATE INDEX CONCURRENTLY IF NOT EXISTS idx_OperatorCallResponsibilities_CallId ON OperatorCallResponsibilities (CallId);