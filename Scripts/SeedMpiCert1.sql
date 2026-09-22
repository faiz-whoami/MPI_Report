USE [MPI_Report];
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Customers WHERE [Name] = N'Sample Drilling Co.')
BEGIN
    INSERT INTO dbo.Customers
    (
        [Name],
        Email,
        Phone,
        Address,
        IsActive,
        CreatedDate
    )
    VALUES
    (
        N'Sample Drilling Co.',
        N'sample@drilling.example',
        N'00000000',
        N'Dubai',
        1,
        GETDATE()
    );
END
GO

DECLARE @CustomerId INT =
(
    SELECT CustomerId
    FROM dbo.Customers
    WHERE [Name] = N'Sample Drilling Co.'
);

IF NOT EXISTS (SELECT 1 FROM dbo.InspectionReports WHERE ReportNo = N'Sky-NDT-Inspect-01/7')
BEGIN
    INSERT INTO dbo.InspectionReports
    (
        WorkOrderNo,
        ReportNo,
        InspectionDate,
        RecommendedDueDate,
        TestLocation,
        CustomerId,
        ItemDescription,
        ItemSerialNo,
        Manufacturer,
        RatingSWL,
        ModelNo,
        ServiceType,
        InspectionTypeCategory,
        Material,
        TestMethod,
        LightingMethod,
        MagnetismType,
        CurrentType,
        MagneticFieldDirection,
        ProcedureNo,
        SurfaceCondition,
        LiftingCapacity,
        SurfaceTemperature,
        Sensitivity,
        AcceptanceCriteria,
        LightingType,
        InspectedItemImagePath,
        Comments,
        AreaOfTesting,
        RestrictedAccess,
        InspectionRemarks,
        InspectionResult,
        InspectorName,
        InspectorQualification,
        InspectorSignaturePath,
        InspectorSignedDate,
        ReviewerName,
        ReviewerDesignation,
        ReviewerSignaturePath,
        ReviewedDate,
        CreatedDate
    )
    VALUES
    (
        N'Sky-NDT-Inspect-01',
        N'Sky-NDT-Inspect-01/7',
        '2024-03-29',
        '2026-12-02',
        N'Dubai',
        @CustomerId,
        N'Air / Hydraulic Winches Utility Winches / Tuggers',
        N'Power-MPI-008',
        N'N/A',
        N'N/A',
        N'N/A',
        N'Utility Winches / Tuggers',
        N'MPI',
        N'Aluminium',
        N'Dry',
        N'Fluorescent',
        N'Residual',
        N'',
        N'',
        N'SWIM-Section. No.39 / ASME-B30.7',
        N'',
        N'',
        NULL,
        N'',
        N'AC 08',
        N'',
        NULL,
        N'',
        N'',
        N'',
        N'',
        N'Satisfactory',
        N'Muhammad Iftikhar',
        N'FSOGS-SNT-TC 1A level II',
        NULL,
        '2024-03-29',
        N'Sky Manager',
        N'QA/QC FSOGS',
        NULL,
        '2024-03-29',
        GETDATE()
    );

    DECLARE @ReportId INT = SCOPE_IDENTITY();

    INSERT INTO dbo.InspectionEquipments
    (
        InspectionReportId,
        EquipmentName,
        EquipmentSerialNo,
        CalibrationDate,
        CalibrationDueDate
    )
    VALUES
    (@ReportId, N'Vernier Calliper', N'150740053', '2025-07-12', '2026-07-11'),
    (@ReportId, N'Lux Meter', N'L240630', '2025-04-30', '2026-04-29'),
    (@ReportId, N'Measuring Ruler', N'DMI-12', '2025-04-29', '2026-04-28'),
    (@ReportId, N'Sheave Verification Gauge', N'UT-15', '2025-04-29', '2026-04-28');

    INSERT INTO dbo.InspectionConsumables
    (
        InspectionReportId,
        ConsumableName,
        BrandType
    )
    VALUES
    (@ReportId, N'Cleaner', N'BM'),
    (@ReportId, N'White Contrast', N'Whitw con'),
    (@ReportId, N'Magnetic Ink', N'CB-888'),
    (@ReportId, N'Light Intensity', N'BI-985');
END
GO

SELECT
    r.InspectionReportId,
    r.ReportNo,
    c.[Name] AS CustomerName
FROM dbo.InspectionReports AS r
INNER JOIN dbo.Customers AS c
    ON c.CustomerId = r.CustomerId
WHERE r.ReportNo = N'Sky-NDT-Inspect-01/7';
GO
