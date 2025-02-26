-- Таблица Clients
CREATE TABLE Clients (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Age INT NOT NULL,
    Workplace NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20) NOT NULL
);

-- Индексы для таблицы Clients
CREATE INDEX idx_Clients_FullName ON Clients (FullName);
CREATE INDEX idx_Clients_Phone ON Clients (Phone);

-- Таблица CreditProducts
CREATE TABLE CreditProducts (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    InterestRate DECIMAL(5,2) NOT NULL
);

-- Индексы для таблицы CreditProducts
CREATE INDEX idx_CreditProducts_ProductName ON CreditProducts (ProductName);

-- Таблица CreditApplications
CREATE TABLE CreditApplications (
    Id INT PRIMARY KEY IDENTITY(1,1),
    LoanPurpose NVARCHAR(200) NOT NULL,
    LoanAmount DECIMAL(18,2) NOT NULL,
    ClientIncome DECIMAL(18,2) NOT NULL,
    CreditProductId INT NOT NULL,
    FOREIGN KEY (CreditProductId) REFERENCES CreditProducts(Id)
);

-- Индексы для таблицы CreditApplications
CREATE INDEX idx_CreditApplications_CreditProductId ON CreditApplications (CreditProductId);
CREATE INDEX idx_CreditApplications_LoanAmount ON CreditApplications (LoanAmount);

-- Таблица Calls
CREATE TABLE Calls (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ScheduledDate DATETIME NOT NULL,
    CallResult NVARCHAR(200),
    IsProcessed BIT NOT NULL DEFAULT 0
);

-- Индексы для таблицы Calls
CREATE INDEX idx_Calls_ScheduledDate ON Calls (ScheduledDate);
CREATE INDEX idx_Calls_IsProcessed ON Calls (IsProcessed);

-- Таблица Operators
CREATE TABLE Operators (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OperatorName NVARCHAR(100) NOT NULL
);

-- Индексы для таблицы Operators
CREATE INDEX idx_Operators_OperatorName ON Operators (OperatorName);

-- Таблица OperatorCallResponsibilities
CREATE TABLE OperatorCallResponsibilities (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OperatorId INT NOT NULL,
    CallId INT NOT NULL,
    FOREIGN KEY (OperatorId) REFERENCES Operators(Id),
    FOREIGN KEY (CallId) REFERENCES Calls(Id)
);

-- Индексы для таблицы OperatorCallResponsibilities
CREATE INDEX idx_OperatorCallResponsibilities_OperatorId ON OperatorCallResponsibilities (OperatorId);
CREATE INDEX idx_OperatorCallResponsibilities_CallId ON OperatorCallResponsibilities (CallId);