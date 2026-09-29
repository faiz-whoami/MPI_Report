USE [MPI_Report];
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE Name = N'Admin')
BEGIN
    INSERT INTO dbo.Roles (Name) VALUES (N'Admin'), (N'Inspector'), (N'Reviewer'), (N'Client');
END
GO

DECLARE @AdminRoleId INT = (SELECT RoleId FROM dbo.Roles WHERE Name = N'Admin');

IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Username = N'admin')
BEGIN
    INSERT INTO dbo.Users (Username, PasswordHash, DisplayName, RoleId, IsActive, CreatedDate)
    VALUES
    (
        N'admin',
        LOWER(CONVERT(varchar(64), HASHBYTES('SHA2_256', CONVERT(varchar(100), 'MPI|Admin@123')), 2)),
        N'Administrator',
        @AdminRoleId,
        1,
        GETDATE()
    );
END
GO

DECLARE @CustomerId INT = (SELECT CustomerId FROM dbo.Customers WHERE [Name] = N'Sample Drilling Co.');

IF @CustomerId IS NULL
BEGIN
    RAISERROR('Run Scripts/SeedMpiCert1.sql first so Sample Drilling Co. exists.', 16, 1);
    RETURN;
END

IF NOT EXISTS (SELECT 1 FROM dbo.Rigs WHERE Name = N'Sample Rig')
BEGIN
    INSERT INTO dbo.Rigs (Name, Location, CustomerId, IsActive, CreatedDate)
    VALUES (N'Sample Rig', N'Dubai', @CustomerId, 1, GETDATE());
END

DECLARE @RigId INT = (SELECT RigId FROM dbo.Rigs WHERE Name = N'Sample Rig');

IF NOT EXISTS (SELECT 1 FROM dbo.Jobs WHERE JobNo = N'Sky-NDT-Inspect-01')
BEGIN
    INSERT INTO dbo.Jobs (JobNo, Description, CustomerId, RigId, StartDate, IsActive, CreatedDate)
    VALUES (N'Sky-NDT-Inspect-01', N'MPI sample job', @CustomerId, @RigId, '2024-03-29', 1, GETDATE());
END

DECLARE @JobId INT = (SELECT JobId FROM dbo.Jobs WHERE JobNo = N'Sky-NDT-Inspect-01');

UPDATE dbo.InspectionReports
SET JobId = @JobId
WHERE ReportNo = N'Sky-NDT-Inspect-01/7'
  AND JobId IS NULL;

IF NOT EXISTS (SELECT 1 FROM dbo.InventoryItems WHERE JobId = @JobId)
BEGIN
    INSERT INTO dbo.InventoryItems (JobId, ItemCode, Description, Position, ItemType, Status, SpecialType)
    VALUES (@JobId, N'WIN-001', N'Air / Hydraulic Winches Utility Winches / Tuggers', N'Deck', N'Winch', N'Satisfactory', N'MPI');
END

IF NOT EXISTS (SELECT 1 FROM dbo.CorrectiveActions WHERE JobId = @JobId)
BEGIN
    INSERT INTO dbo.CorrectiveActions (JobId, Title, Description, Criticality, Status, DueDate)
    VALUES (@JobId, N'Paint touch-up on winch frame', N'Sample open corrective action.', N'Minor', N'Open', '2024-04-15');
END

IF NOT EXISTS (SELECT 1 FROM dbo.DailyMeetings WHERE JobId = @JobId)
BEGIN
    INSERT INTO dbo.DailyMeetings (JobId, MeetingDate, Description, Signatures)
    VALUES (@JobId, '2024-03-29', N'Pre-inspection toolbox talk', N'Muhammad Iftikhar');
END
GO
