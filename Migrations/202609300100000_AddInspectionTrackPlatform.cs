namespace MPI_Report.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddInspectionTrackPlatform : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleId);

            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 80),
                        PasswordHash = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(maxLength: 150),
                        RoleId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.Username, unique: true)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.Rigs",
                c => new
                    {
                        RigId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Location = c.String(maxLength: 150),
                        CustomerId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RigId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);

            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        JobNo = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 250),
                        CustomerId = c.Int(nullable: false),
                        RigId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Rigs", t => t.RigId)
                .Index(t => t.CustomerId)
                .Index(t => t.RigId);

            CreateTable(
                "dbo.InventoryItems",
                c => new
                    {
                        InventoryItemId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        ItemCode = c.String(maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 250),
                        Position = c.String(maxLength: 100),
                        ItemType = c.String(maxLength: 100),
                        Status = c.String(maxLength: 50),
                        SpecialType = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.InventoryItemId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);

            CreateTable(
                "dbo.InspectionChecklists",
                c => new
                    {
                        ChecklistId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 150),
                        Frequency = c.String(maxLength: 50),
                        ChecklistDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChecklistId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);

            CreateTable(
                "dbo.ChecklistItems",
                c => new
                    {
                        ChecklistItemId = c.Int(nullable: false, identity: true),
                        ChecklistId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 250),
                        IsCompleted = c.Boolean(nullable: false),
                        Result = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ChecklistItemId)
                .ForeignKey("dbo.InspectionChecklists", t => t.ChecklistId, cascadeDelete: true)
                .Index(t => t.ChecklistId);

            CreateTable(
                "dbo.CorrectiveActions",
                c => new
                    {
                        CorrectiveActionId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(),
                        Criticality = c.String(maxLength: 50),
                        Status = c.String(maxLength: 50),
                        DueDate = c.DateTime(),
                        ClosedDate = c.DateTime(),
                        ClosedComments = c.String(),
                    })
                .PrimaryKey(t => t.CorrectiveActionId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);

            CreateTable(
                "dbo.DailyMeetings",
                c => new
                    {
                        DailyMeetingId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        MeetingDate = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 250),
                        Signatures = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.DailyMeetingId)
                .ForeignKey("dbo.Jobs", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);

            AddColumn("dbo.InspectionReports", "JobId", c => c.Int());
            CreateIndex("dbo.InspectionReports", "JobId");
            AddForeignKey("dbo.InspectionReports", "JobId", "dbo.Jobs", "JobId");
        }

        public override void Down()
        {
            DropForeignKey("dbo.InspectionReports", "JobId", "dbo.Jobs");
            DropIndex("dbo.InspectionReports", new[] { "JobId" });
            DropColumn("dbo.InspectionReports", "JobId");

            DropForeignKey("dbo.DailyMeetings", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.CorrectiveActions", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.ChecklistItems", "ChecklistId", "dbo.InspectionChecklists");
            DropForeignKey("dbo.InspectionChecklists", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.InventoryItems", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "RigId", "dbo.Rigs");
            DropForeignKey("dbo.Jobs", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Rigs", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");

            DropIndex("dbo.DailyMeetings", new[] { "JobId" });
            DropIndex("dbo.CorrectiveActions", new[] { "JobId" });
            DropIndex("dbo.ChecklistItems", new[] { "ChecklistId" });
            DropIndex("dbo.InspectionChecklists", new[] { "JobId" });
            DropIndex("dbo.InventoryItems", new[] { "JobId" });
            DropIndex("dbo.Jobs", new[] { "RigId" });
            DropIndex("dbo.Jobs", new[] { "CustomerId" });
            DropIndex("dbo.Rigs", new[] { "CustomerId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "Username" });

            DropTable("dbo.DailyMeetings");
            DropTable("dbo.CorrectiveActions");
            DropTable("dbo.ChecklistItems");
            DropTable("dbo.InspectionChecklists");
            DropTable("dbo.InventoryItems");
            DropTable("dbo.Jobs");
            DropTable("dbo.Rigs");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
