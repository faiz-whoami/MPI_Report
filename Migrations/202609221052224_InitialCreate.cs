namespace MPI_Report.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(maxLength: 150),
                        Phone = c.String(maxLength: 30),
                        Address = c.String(maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.InspectionReports",
                c => new
                    {
                        InspectionReportId = c.Int(nullable: false, identity: true),
                        WorkOrderNo = c.String(nullable: false, maxLength: 100),
                        ReportNo = c.String(nullable: false, maxLength: 100),
                        InspectionDate = c.DateTime(nullable: false),
                        RecommendedDueDate = c.DateTime(),
                        TestLocation = c.String(maxLength: 150),
                        CustomerId = c.Int(nullable: false),
                        ItemDescription = c.String(maxLength: 250),
                        ItemSerialNo = c.String(maxLength: 100),
                        Manufacturer = c.String(maxLength: 150),
                        RatingSWL = c.String(maxLength: 150),
                        ModelNo = c.String(maxLength: 150),
                        ServiceType = c.String(maxLength: 150),
                        InspectionTypeCategory = c.String(maxLength: 150),
                        Material = c.String(maxLength: 100),
                        TestMethod = c.String(maxLength: 50),
                        LightingMethod = c.String(maxLength: 50),
                        MagnetismType = c.String(maxLength: 50),
                        CurrentType = c.String(maxLength: 50),
                        MagneticFieldDirection = c.String(maxLength: 50),
                        ProcedureNo = c.String(maxLength: 250),
                        SurfaceCondition = c.String(maxLength: 100),
                        LiftingCapacity = c.String(maxLength: 100),
                        SurfaceTemperature = c.Decimal(precision: 10, scale: 2),
                        Sensitivity = c.String(maxLength: 100),
                        AcceptanceCriteria = c.String(maxLength: 100),
                        LightingType = c.String(maxLength: 100),
                        InspectedItemImagePath = c.String(maxLength: 500),
                        Comments = c.String(),
                        AreaOfTesting = c.String(maxLength: 250),
                        RestrictedAccess = c.String(),
                        InspectionRemarks = c.String(),
                        InspectionResult = c.String(maxLength: 50),
                        InspectorName = c.String(maxLength: 150),
                        InspectorQualification = c.String(maxLength: 250),
                        InspectorSignaturePath = c.String(maxLength: 500),
                        InspectorSignedDate = c.DateTime(),
                        ReviewerName = c.String(maxLength: 150),
                        ReviewerDesignation = c.String(maxLength: 150),
                        ReviewerSignaturePath = c.String(maxLength: 500),
                        ReviewedDate = c.DateTime(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.InspectionReportId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.InspectionConsumables",
                c => new
                    {
                        InspectionConsumableId = c.Int(nullable: false, identity: true),
                        InspectionReportId = c.Int(nullable: false),
                        ConsumableName = c.String(nullable: false, maxLength: 150),
                        BrandType = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.InspectionConsumableId)
                .ForeignKey("dbo.InspectionReports", t => t.InspectionReportId, cascadeDelete: true)
                .Index(t => t.InspectionReportId);
            
            CreateTable(
                "dbo.InspectionEquipments",
                c => new
                    {
                        InspectionEquipmentId = c.Int(nullable: false, identity: true),
                        InspectionReportId = c.Int(nullable: false),
                        EquipmentName = c.String(nullable: false, maxLength: 150),
                        EquipmentSerialNo = c.String(maxLength: 100),
                        CalibrationDate = c.DateTime(),
                        CalibrationDueDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.InspectionEquipmentId)
                .ForeignKey("dbo.InspectionReports", t => t.InspectionReportId, cascadeDelete: true)
                .Index(t => t.InspectionReportId);
            
            CreateTable(
                "dbo.TestEvaluations",
                c => new
                    {
                        TestEvaluationId = c.Int(nullable: false, identity: true),
                        InspectionReportId = c.Int(nullable: false),
                        JointNo = c.String(maxLength: 50),
                        WelderId = c.String(maxLength: 100),
                        WeldLength = c.String(maxLength: 50),
                        Discontinuity = c.String(maxLength: 250),
                        StartLocation = c.String(maxLength: 100),
                        EndLocation = c.String(maxLength: 100),
                        DefectLength = c.String(maxLength: 50),
                        Evaluation = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.TestEvaluationId)
                .ForeignKey("dbo.InspectionReports", t => t.InspectionReportId, cascadeDelete: true)
                .Index(t => t.InspectionReportId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestEvaluations", "InspectionReportId", "dbo.InspectionReports");
            DropForeignKey("dbo.InspectionEquipments", "InspectionReportId", "dbo.InspectionReports");
            DropForeignKey("dbo.InspectionConsumables", "InspectionReportId", "dbo.InspectionReports");
            DropForeignKey("dbo.InspectionReports", "CustomerId", "dbo.Customers");
            DropIndex("dbo.TestEvaluations", new[] { "InspectionReportId" });
            DropIndex("dbo.InspectionEquipments", new[] { "InspectionReportId" });
            DropIndex("dbo.InspectionConsumables", new[] { "InspectionReportId" });
            DropIndex("dbo.InspectionReports", new[] { "CustomerId" });
            DropTable("dbo.TestEvaluations");
            DropTable("dbo.InspectionEquipments");
            DropTable("dbo.InspectionConsumables");
            DropTable("dbo.InspectionReports");
            DropTable("dbo.Customers");
        }
    }
}
