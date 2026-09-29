USE [MPI_Report];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRANSACTION;


/* ============================================================
   1. CREATE THE ORIGINAL CUSTOMER
   ============================================================ */

IF NOT EXISTS
(
    SELECT 1
    FROM dbo.Customers
    WHERE [Name] = N'Sample Drilling Co.'
)
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
END;


/* ============================================================
   2. CREATE THE ORIGINAL SAMPLE DATASET
      Dataset #1
   ============================================================ */

DECLARE @CustomerId INT;
DECLARE @ReportId INT;

SELECT @CustomerId = CustomerId
FROM dbo.Customers
WHERE [Name] = N'Sample Drilling Co.';


IF NOT EXISTS
(
    SELECT 1
    FROM dbo.InspectionReports
    WHERE ReportNo = N'Sky-NDT-Inspect-01/7'
)
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

    SET @ReportId = SCOPE_IDENTITY();


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

END;


/* ============================================================
   3. CREATE 999 MORE DATASETS
      Dataset #2 -> Dataset #1000
   ============================================================ */

DECLARE @Counter INT = 2;

WHILE @Counter <= 1000
BEGIN

    DECLARE @DatasetCustomerName NVARCHAR(150);
    DECLARE @DatasetEmail NVARCHAR(150);
    DECLARE @DatasetPhone NVARCHAR(30);
    DECLARE @DatasetAddress NVARCHAR(250);

    DECLARE @WorkOrderNo NVARCHAR(100);
    DECLARE @ReportNo NVARCHAR(100);

    DECLARE @InspectionDate DATETIME;
    DECLARE @DueDate DATETIME;


    /* ========================================================
       CUSTOMER DATA
       ======================================================== */

    SET @DatasetCustomerName =
        CASE (@Counter % 12)

            WHEN 0 THEN N'Gulf Engineering'
            WHEN 1 THEN N'Desert Energy Services'
            WHEN 2 THEN N'Arabian Industrial Works'
            WHEN 3 THEN N'Falcon Marine Services'
            WHEN 4 THEN N'United Mechanical Systems'
            WHEN 5 THEN N'Prime Offshore Solutions'
            WHEN 6 THEN N'Atlas Heavy Equipment'
            WHEN 7 THEN N'Emirates Lifting Services'
            WHEN 8 THEN N'Global Rigging Industries'
            WHEN 9 THEN N'Northern Technical Services'
            WHEN 10 THEN N'International Crane Solutions'
            ELSE N'Advanced Inspection Services'

        END
        + N' '
        + RIGHT(N'0000' + CAST(@Counter AS NVARCHAR(10)), 4);


    SET @DatasetEmail =
        N'customer'
        + CAST(@Counter AS NVARCHAR(10))
        + N'@example.com';


    SET @DatasetPhone =
        N'+97150'
        + RIGHT(
            N'0000000' + CAST(@Counter AS NVARCHAR(10)),
            7
        );


    SET @DatasetAddress =
        CASE (@Counter % 8)

            WHEN 0 THEN N'Dubai'
            WHEN 1 THEN N'Abu Dhabi'
            WHEN 2 THEN N'Sharjah'
            WHEN 3 THEN N'Ajman'
            WHEN 4 THEN N'Doha'
            WHEN 5 THEN N'Riyadh'
            WHEN 6 THEN N'Jeddah'
            ELSE N'Muscat'

        END;


    /* ========================================================
       CREATE CUSTOMER
       ======================================================== */

    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.Customers
        WHERE [Name] = @DatasetCustomerName
    )
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
            @DatasetCustomerName,
            @DatasetEmail,
            @DatasetPhone,
            @DatasetAddress,
            1,
            DATEADD(DAY, -@Counter, GETDATE())
        );

    END;


    SELECT @CustomerId = CustomerId
    FROM dbo.Customers
    WHERE [Name] = @DatasetCustomerName;


    /* ========================================================
       REPORT IDENTIFIERS
       ======================================================== */

    SET @WorkOrderNo =
        N'Sky-NDT-WO-' +
        RIGHT(
            N'0000' + CAST(@Counter AS NVARCHAR(10)),
            4
        );


    SET @ReportNo =
        N'Sky-NDT-DATA-' +
        RIGHT(
            N'0000' + CAST(@Counter AS NVARCHAR(10)),
            4
        );


    SET @InspectionDate =
        DATEADD(
            DAY,
            -@Counter,
            CAST(GETDATE() AS DATE)
        );


    SET @DueDate =
        DATEADD(
            DAY,
            365,
            @InspectionDate
        );


    /* ========================================================
       CREATE INSPECTION REPORT
       ======================================================== */

    IF NOT EXISTS
    (
        SELECT 1
        FROM dbo.InspectionReports
        WHERE ReportNo = @ReportNo
    )
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
            @WorkOrderNo,
            @ReportNo,

            /* Inspection Date */
            @InspectionDate,

            /* Due Date */
            @DueDate,

            /* Location */
            @DatasetAddress,

            @CustomerId,


            /* Item Description */
            CASE (@Counter % 10)

                WHEN 0 THEN N'Air Hydraulic Winch'
                WHEN 1 THEN N'Overhead Crane'
                WHEN 2 THEN N'Mobile Crane'
                WHEN 3 THEN N'Chain Block'
                WHEN 4 THEN N'Wire Rope Sling'
                WHEN 5 THEN N'Hydraulic Winch'
                WHEN 6 THEN N'Air Winch'
                WHEN 7 THEN N'Lifting Beam'
                WHEN 8 THEN N'Personnel Basket'
                ELSE N'Utility Winch'

            END,


            /* Serial Number */
            N'Power-MPI-' +
            RIGHT(
                N'00000' + CAST(@Counter AS NVARCHAR(10)),
                5
            ),


            /* Manufacturer */
            CASE (@Counter % 10)

                WHEN 0 THEN N'Demag'
                WHEN 1 THEN N'Konecranes'
                WHEN 2 THEN N'Mammoet'
                WHEN 3 THEN N'Sarens'
                WHEN 4 THEN N'Columbus McKinnon'
                WHEN 5 THEN N'Gunnebo'
                WHEN 6 THEN N'Nitchi'
                WHEN 7 THEN N'Verling'
                WHEN 8 THEN N'Manitou'
                ELSE N'J.D. Neuhaus'

            END,


            /* Rating SWL */
            CASE (@Counter % 6)

                WHEN 0 THEN N'2 Ton'
                WHEN 1 THEN N'5 Ton'
                WHEN 2 THEN N'10 Ton'
                WHEN 3 THEN N'15 Ton'
                WHEN 4 THEN N'20 Ton'
                ELSE N'25 Ton'

            END,


            /* Model */
            N'MDL-' +
            RIGHT(
                N'0000' + CAST(@Counter AS NVARCHAR(10)),
                4
            ),


            /* Service Type */
            CASE (@Counter % 6)

                WHEN 0 THEN N'Utility Winches'
                WHEN 1 THEN N'Heavy Lifting'
                WHEN 2 THEN N'Routine Inspection'
                WHEN 3 THEN N'Maintenance Inspection'
                WHEN 4 THEN N'Periodic Inspection'
                ELSE N'Annual Inspection'

            END,


            /* Inspection Category */
            CASE (@Counter % 4)

                WHEN 0 THEN N'MPI'
                WHEN 1 THEN N'LPI'
                WHEN 2 THEN N'Visual'
                ELSE N'MPI'

            END,


            /* Material */
            CASE (@Counter % 6)

                WHEN 0 THEN N'Aluminium'
                WHEN 1 THEN N'Carbon Steel'
                WHEN 2 THEN N'Alloy Steel'
                WHEN 3 THEN N'Stainless Steel'
                WHEN 4 THEN N'Cast Steel'
                ELSE N'Ductile Iron'

            END,


            /* Test Method */
            CASE (@Counter % 4)

                WHEN 0 THEN N'Dry'
                WHEN 1 THEN N'Wet'
                WHEN 2 THEN N'Visible'
                ELSE N'Fluorescent'

            END,


            /* Lighting */
            CASE (@Counter % 4)

                WHEN 0 THEN N'Fluorescent'
                WHEN 1 THEN N'LED'
                WHEN 2 THEN N'Natural'
                ELSE N'UV-A'

            END,


            /* Magnetism */
            CASE (@Counter % 4)

                WHEN 0 THEN N'Residual'
                WHEN 1 THEN N'Continuous'
                WHEN 2 THEN N'Yoke'
                ELSE N'Prods'

            END,


            /* Current */
            CASE (@Counter % 3)

                WHEN 0 THEN N'AC'
                WHEN 1 THEN N'DC'
                ELSE N'AC'

            END,


            /* Magnetic Field Direction */
            CASE (@Counter % 4)

                WHEN 0 THEN N'Longitudinal'
                WHEN 1 THEN N'Transverse'
                WHEN 2 THEN N'Circular'
                ELSE N'Longitudinal'

            END,


            /* Procedure */
            CASE (@Counter % 4)

                WHEN 0 THEN N'SWIM-Section. No.39 / ASME-B30.7'
                WHEN 1 THEN N'ASTM E709'
                WHEN 2 THEN N'ASME Section V'
                ELSE N'ISO 9934'

            END,


            /* Surface Condition */
            CASE (@Counter % 5)

                WHEN 0 THEN N'Clean'
                WHEN 1 THEN N'Good'
                WHEN 2 THEN N'Painted'
                WHEN 3 THEN N'Acceptable'
                ELSE N'Clean and Dry'

            END,


            /* Lifting Capacity */
            CASE (@Counter % 5)

                WHEN 0 THEN N'2 Ton'
                WHEN 1 THEN N'5 Ton'
                WHEN 2 THEN N'10 Ton'
                WHEN 3 THEN N'15 Ton'
                ELSE N'20 Ton'

            END,


            /* Surface Temperature */
            CASE (@Counter % 5)

                WHEN 0 THEN 25
                WHEN 1 THEN 30
                WHEN 2 THEN 35
                WHEN 3 THEN 40
                ELSE 45

            END,


            /* Sensitivity */
            CASE (@Counter % 4)

                WHEN 0 THEN N'AC 08'
                WHEN 1 THEN N'High'
                WHEN 2 THEN N'Medium'
                ELSE N'Low'

            END,


            /* Acceptance Criteria */
            CASE (@Counter % 4)

                WHEN 0 THEN N'AC 08'
                WHEN 1 THEN N'ASME B30.7'
                WHEN 2 THEN N'ISO 9934'
                ELSE N'ASTM E709'

            END,


            /* Lighting Type */
            CASE (@Counter % 4)

                WHEN 0 THEN N'White Light'
                WHEN 1 THEN N'UV-A'
                WHEN 2 THEN N'LED'
                ELSE N'Fluorescent'

            END,


            /* Image */
            NULL,


            /* Comments */
            N'Inspection dataset '
            + CAST(@Counter AS NVARCHAR(10))
            + N' generated for testing.',


            /* Area Of Testing */
            CASE (@Counter % 5)

                WHEN 0 THEN N'Hook Assembly'
                WHEN 1 THEN N'Load Chain'
                WHEN 2 THEN N'Wire Rope'
                WHEN 3 THEN N'Body and Frame'
                ELSE N'Load Bearing Components'

            END,


            /* Restricted Access */
            CASE
                WHEN @Counter % 5 = 0
                    THEN N'Yes'
                ELSE N'No'
            END,


            /* Inspection Remarks */
            CASE (@Counter % 6)

                WHEN 0 THEN N'No relevant indications found.'
                WHEN 1 THEN N'Equipment condition satisfactory.'
                WHEN 2 THEN N'Inspection completed successfully.'
                WHEN 3 THEN N'Minor surface indications observed.'
                WHEN 4 THEN N'All inspected areas acceptable.'
                ELSE N'Equipment approved after inspection.'

            END,


            /* Inspection Result */
            CASE

                WHEN @Counter % 20 = 0
                    THEN N'Not Satisfactory'

                WHEN @Counter % 10 = 0
                    THEN N'Conditional'

                ELSE N'Satisfactory'

            END,


            /* Inspector */
            CASE (@Counter % 8)

                WHEN 0 THEN N'Muhammad Iftikhar'
                WHEN 1 THEN N'Ahmed Khan'
                WHEN 2 THEN N'Omar Hassan'
                WHEN 3 THEN N'Bilal Ahmed'
                WHEN 4 THEN N'Usman Tariq'
                WHEN 5 THEN N'Farhan Malik'
                WHEN 6 THEN N'Saad Ali'
                ELSE N'Imran Shah'

            END,


            /* Qualification */
            CASE (@Counter % 3)

                WHEN 0 THEN N'FSOGS-SNT-TC 1A level II'
                WHEN 1 THEN N'ASNT Level II'
                ELSE N'ISO 9712 Level II'

            END,


            /* Inspector Signature */
            NULL,


            /* Inspector Signed Date */
            @InspectionDate,


            /* Reviewer */
            CASE (@Counter % 5)

                WHEN 0 THEN N'Sky Manager'
                WHEN 1 THEN N'Ahmed Supervisor'
                WHEN 2 THEN N'QA Manager'
                WHEN 3 THEN N'Inspection Manager'
                ELSE N'Technical Manager'

            END,


            /* Reviewer Designation */
            CASE (@Counter % 3)

                WHEN 0 THEN N'QA/QC FSOGS'
                WHEN 1 THEN N'QA/QC Manager'
                ELSE N'Senior Inspector'

            END,


            /* Reviewer Signature */
            NULL,


            /* Reviewed Date */
            DATEADD(DAY, 1, @InspectionDate),


            /* Created Date */
            GETDATE()
        );


        SET @ReportId = SCOPE_IDENTITY();


        /* =====================================================
           4 EQUIPMENT RECORDS
           ===================================================== */

        INSERT INTO dbo.InspectionEquipments
        (
            InspectionReportId,
            EquipmentName,
            EquipmentSerialNo,
            CalibrationDate,
            CalibrationDueDate
        )
        VALUES
        (
            @ReportId,
            CASE (@Counter % 5)
                WHEN 0 THEN N'Vernier Calliper'
                WHEN 1 THEN N'MPI Yoke'
                WHEN 2 THEN N'Lux Meter'
                WHEN 3 THEN N'Gauss Meter'
                ELSE N'Digital Caliper'
            END,
            N'EQ-' + RIGHT(N'0000' + CAST(@Counter AS NVARCHAR(10)), 4) + N'-01',
            DATEADD(DAY, -30, @InspectionDate),
            DATEADD(DAY, 335, @InspectionDate)
        ),
        (
            @ReportId,
            CASE (@Counter % 5)
                WHEN 0 THEN N'Lux Meter'
                WHEN 1 THEN N'Measuring Ruler'
                WHEN 2 THEN N'UV Light Meter'
                WHEN 3 THEN N'Temperature Gauge'
                ELSE N'Digital Thermometer'
            END,
            N'EQ-' + RIGHT(N'0000' + CAST(@Counter AS NVARCHAR(10)), 4) + N'-02',
            DATEADD(DAY, -60, @InspectionDate),
            DATEADD(DAY, 305, @InspectionDate)
        ),
        (
            @ReportId,
            CASE (@Counter % 5)
                WHEN 0 THEN N'Measuring Ruler'
                WHEN 1 THEN N'Sheave Verification Gauge'
                WHEN 2 THEN N'Coating Thickness Gauge'
                WHEN 3 THEN N'Yoke Test Block'
                ELSE N'Depth Gauge'
            END,
            N'EQ-' + RIGHT(N'0000' + CAST(@Counter AS NVARCHAR(10)), 4) + N'-03',
            DATEADD(DAY, -90, @InspectionDate),
            DATEADD(DAY, 275, @InspectionDate)
        ),
        (
            @ReportId,
            CASE (@Counter % 5)
                WHEN 0 THEN N'Sheave Verification Gauge'
                WHEN 1 THEN N'Test Weight'
                WHEN 2 THEN N'Light Intensity Meter'
                WHEN 3 THEN N'Magnetic Field Meter'
                ELSE N'Surface Temperature Meter'
            END,
            N'EQ-' + RIGHT(N'0000' + CAST(@Counter AS NVARCHAR(10)), 4) + N'-04',
            DATEADD(DAY, -120, @InspectionDate),
            DATEADD(DAY, 245, @InspectionDate)
        );


        /* =====================================================
           4 CONSUMABLE RECORDS
           ===================================================== */

        INSERT INTO dbo.InspectionConsumables
        (
            InspectionReportId,
            ConsumableName,
            BrandType
        )
        VALUES
        (
            @ReportId,
            CASE (@Counter % 5)
                WHEN 0 THEN N'Cleaner'
                WHEN 1 THEN N'Penetrant'
                WHEN 2 THEN N'Contrast Paint'
                WHEN 3 THEN N'Cleaning Solvent'
                ELSE N'Inspection Cleaner'
            END,
            CASE (@Counter % 5)
                WHEN 0 THEN N'BM'
                WHEN 1 THEN N'SKF'
                WHEN 2 THEN N'Sherwin'
                WHEN 3 THEN N'3M'
                ELSE N'CRC'
            END
        ),
        (
            @ReportId,
            CASE (@Counter % 5)
                WHEN 0 THEN N'White Contrast'
                WHEN 1 THEN N'White Developer'
                WHEN 2 THEN N'Contrast Paint'
                WHEN 3 THEN N'White Powder'
                ELSE N'White Inspection Paint'
            END,
            CASE (@Counter % 5)
                WHEN 0 THEN N'Whitw con'
                WHEN 1 THEN N'Magnaflux'
                WHEN 2 THEN N'SKF'
                WHEN 3 THEN N'Castrol'
                ELSE N'CRC'
            END
        ),
        (
            @ReportId,
            CASE (@Counter % 5)
                WHEN 0 THEN N'Magnetic Ink'
                WHEN 1 THEN N'MPI Suspension'
                WHEN 2 THEN N'Magnetic Powder'
                WHEN 3 THEN N'Fluorescent Powder'
                ELSE N'Magnetic Particle Ink'
            END,
            CASE (@Counter % 5)
                WHEN 0 THEN N'CB-888'
                WHEN 1 THEN N'Magnaflux'
                WHEN 2 THEN N'Parker'
                WHEN 3 THEN N'Magnaflux'
                ELSE N'Chemetall'
            END
        ),
        (
            @ReportId,
            CASE (@Counter % 5)
                WHEN 0 THEN N'Light Intensity'
                WHEN 1 THEN N'UV Cleaner'
                WHEN 2 THEN N'Inspection Spray'
                WHEN 3 THEN N'Fluorescent Ink'
                ELSE N'Contrast Cleaner'
            END,
            CASE (@Counter % 5)
                WHEN 0 THEN N'BI-985'
                WHEN 1 THEN N'3M'
                WHEN 2 THEN N'CRC'
                WHEN 3 THEN N'Versa'
                ELSE N'SKF'
            END
        );

    END;


    SET @Counter = @Counter + 1;

END;


COMMIT TRANSACTION;


/* ============================================================
   5. VERIFY 1,000 DATASETS
   ============================================================ */

SELECT
    COUNT(*) AS TotalInspectionReports
FROM dbo.InspectionReports
WHERE ReportNo LIKE N'Sky-NDT-DATA-%';


SELECT
    COUNT(*) AS TotalCustomers
FROM dbo.Customers
WHERE [Name] LIKE N'%[0-9][0-9][0-9][0-9]';


SELECT
    COUNT(*) AS TotalEquipment
FROM dbo.InspectionEquipments AS e
INNER JOIN dbo.InspectionReports AS r
    ON r.InspectionReportId = e.InspectionReportId
WHERE r.ReportNo LIKE N'Sky-NDT-DATA-%';


SELECT
    COUNT(*) AS TotalConsumables
FROM dbo.InspectionConsumables AS c
INNER JOIN dbo.InspectionReports AS r
    ON r.InspectionReportId = c.InspectionReportId
WHERE r.ReportNo LIKE N'Sky-NDT-DATA-%';


/* ============================================================
   6. SHOW SAMPLE OF GENERATED DATA
   ============================================================ */

SELECT TOP 50
    r.InspectionReportId,
    r.WorkOrderNo,
    r.ReportNo,
    r.InspectionDate,
    r.TestLocation,
    c.[Name] AS CustomerName,
    r.ItemDescription,
    r.ItemSerialNo,
    r.Manufacturer,
    r.InspectionTypeCategory,
    r.Material,
    r.InspectionResult,
    r.InspectorName,
    r.ReviewerName
FROM dbo.InspectionReports AS r
INNER JOIN dbo.Customers AS c
    ON c.CustomerId = r.CustomerId
WHERE r.ReportNo LIKE N'Sky-NDT-DATA-%'
ORDER BY r.InspectionReportId;

GO