USE MPI_Report_Cpy;
GO

CREATE OR ALTER PROCEDURE dbo.GetInspectionReport
    @ReportId INT
AS
BEGIN
    SET NOCOUNT ON;

    -------------------------------------------------------
    -- 1. REPORT HEADER + CUSTOMER
    -------------------------------------------------------
    SELECT 
        r.WorkOrderNo,
        r.ReportNo,
        r.InspectionDate,
        r.RecommendedDueDate,
        r.TestLocation,
        r.ItemDescription,
        r.ItemSerialNo,
        r.Manufacturer,
        r.RatingSWL,
        r.ModelNo,
        r.ServiceType,
        r.InspectionTypeCategory,
        r.Material,
        r.TestMethod,
        r.LightingMethod,
        r.MagnetismType,
        r.CurrentType,
        r.MagneticFieldDirection,
        r.ProcedureNo,
        r.SurfaceCondition,
        r.LiftingCapacity,
        r.SurfaceTemperature,
        r.Sensitivity,
        r.AcceptanceCriteria,
        r.LightingType,
        r.Comments,
        r.AreaOfTesting,
        r.RestrictedAccess,
        r.InspectionRemarks,
        r.InspectionResult,
        r.InspectorName,
        r.InspectorQualification,
        r.InspectorSignedDate,
        r.ReviewerName,
        r.ReviewerDesignation,
        r.ReviewedDate,

       
        c.Name AS CustomerName

    FROM InspectionReports r
    LEFT JOIN Customers c
        ON r.CustomerId = c.CustomerId
    WHERE r.InspectionReportId = @ReportId;


    SELECT
        c.ConsumableName,
        c.BrandType
    FROM InspectionConsumables c
    WHERE c.InspectionReportId = @ReportId;

    SELECT
        e.EquipmentName,
        e.EquipmentSerialNo,
        e.CalibrationDate,
        e.CalibrationDueDate
    FROM InspectionEquipments e
    WHERE e.InspectionReportId = @ReportId;

END
GO


EXEC dbo.GetInspectionReport 10;