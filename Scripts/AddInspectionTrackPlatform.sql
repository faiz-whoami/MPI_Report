USE [MPI_Report];
GO

IF OBJECT_ID(N'dbo.Roles', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Roles
    (
        RoleId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Name NVARCHAR(50) NOT NULL
    );

    CREATE TABLE dbo.Users
    (
        UserId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Username NVARCHAR(80) NOT NULL,
        PasswordHash NVARCHAR(128) NOT NULL,
        DisplayName NVARCHAR(150) NULL,
        RoleId INT NOT NULL,
        IsActive BIT NOT NULL,
        CreatedDate DATETIME NOT NULL,
        CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES dbo.Roles (RoleId)
    );

    CREATE UNIQUE INDEX IX_Users_Username ON dbo.Users (Username);

    CREATE TABLE dbo.Rigs
    (
        RigId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        Name NVARCHAR(150) NOT NULL,
        Location NVARCHAR(150) NULL,
        CustomerId INT NOT NULL,
        IsActive BIT NOT NULL,
        CreatedDate DATETIME NOT NULL,
        CONSTRAINT FK_Rigs_Customers FOREIGN KEY (CustomerId) REFERENCES dbo.Customers (CustomerId)
    );

    CREATE TABLE dbo.Jobs
    (
        JobId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        JobNo NVARCHAR(100) NOT NULL,
        Description NVARCHAR(250) NULL,
        CustomerId INT NOT NULL,
        RigId INT NOT NULL,
        StartDate DATETIME NULL,
        IsActive BIT NOT NULL,
        CreatedDate DATETIME NOT NULL,
        CONSTRAINT FK_Jobs_Customers FOREIGN KEY (CustomerId) REFERENCES dbo.Customers (CustomerId),
        CONSTRAINT FK_Jobs_Rigs FOREIGN KEY (RigId) REFERENCES dbo.Rigs (RigId)
    );

    CREATE TABLE dbo.InventoryItems
    (
        InventoryItemId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        JobId INT NOT NULL,
        ItemCode NVARCHAR(50) NULL,
        Description NVARCHAR(250) NOT NULL,
        Position NVARCHAR(100) NULL,
        ItemType NVARCHAR(100) NULL,
        Status NVARCHAR(50) NULL,
        SpecialType NVARCHAR(50) NULL,
        CONSTRAINT FK_InventoryItems_Jobs FOREIGN KEY (JobId) REFERENCES dbo.Jobs (JobId) ON DELETE CASCADE
    );

    CREATE TABLE dbo.InspectionChecklists
    (
        ChecklistId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        JobId INT NOT NULL,
        Title NVARCHAR(150) NOT NULL,
        Frequency NVARCHAR(50) NULL,
        ChecklistDate DATETIME NOT NULL,
        CONSTRAINT FK_InspectionChecklists_Jobs FOREIGN KEY (JobId) REFERENCES dbo.Jobs (JobId) ON DELETE CASCADE
    );

    CREATE TABLE dbo.ChecklistItems
    (
        ChecklistItemId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        ChecklistId INT NOT NULL,
        Description NVARCHAR(250) NOT NULL,
        IsCompleted BIT NOT NULL,
        Result NVARCHAR(50) NULL,
        CONSTRAINT FK_ChecklistItems_Checklists FOREIGN KEY (ChecklistId) REFERENCES dbo.InspectionChecklists (ChecklistId) ON DELETE CASCADE
    );

    CREATE TABLE dbo.CorrectiveActions
    (
        CorrectiveActionId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        JobId INT NOT NULL,
        Title NVARCHAR(200) NOT NULL,
        Description NVARCHAR(MAX) NULL,
        Criticality NVARCHAR(50) NULL,
        Status NVARCHAR(50) NULL,
        DueDate DATETIME NULL,
        ClosedDate DATETIME NULL,
        ClosedComments NVARCHAR(MAX) NULL,
        CONSTRAINT FK_CorrectiveActions_Jobs FOREIGN KEY (JobId) REFERENCES dbo.Jobs (JobId) ON DELETE CASCADE
    );

    CREATE TABLE dbo.DailyMeetings
    (
        DailyMeetingId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
        JobId INT NOT NULL,
        MeetingDate DATETIME NOT NULL,
        Description NVARCHAR(250) NULL,
        Signatures NVARCHAR(250) NULL,
        CONSTRAINT FK_DailyMeetings_Jobs FOREIGN KEY (JobId) REFERENCES dbo.Jobs (JobId) ON DELETE CASCADE
    );
END
GO

IF COL_LENGTH('dbo.InspectionReports', 'JobId') IS NULL
BEGIN
    ALTER TABLE dbo.InspectionReports ADD JobId INT NULL;
    ALTER TABLE dbo.InspectionReports
        ADD CONSTRAINT FK_InspectionReports_Jobs FOREIGN KEY (JobId) REFERENCES dbo.Jobs (JobId);
END
GO
