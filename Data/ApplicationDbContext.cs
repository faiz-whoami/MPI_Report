using System.Data.Entity;
using MPI_Report.Models;

namespace MPI_Report.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=MPI_Report")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Rig> Rigs { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<InventoryItem> InventoryItems { get; set; }

        public DbSet<InspectionChecklist> InspectionChecklists { get; set; }

        public DbSet<ChecklistItem> ChecklistItems { get; set; }

        public DbSet<CorrectiveAction> CorrectiveActions { get; set; }

        public DbSet<DailyMeeting> DailyMeetings { get; set; }

        public DbSet<InspectionReport> InspectionReports { get; set; }

        public DbSet<InspectionEquipment> InspectionEquipments { get; set; }

        public DbSet<InspectionConsumable> InspectionConsumables { get; set; }

        public DbSet<TestEvaluation> TestEvaluations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasRequired(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rig>()
                .HasRequired(r => r.Customer)
                .WithMany(c => c.Rigs)
                .HasForeignKey(r => r.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Job>()
                .HasRequired(j => j.Customer)
                .WithMany(c => c.Jobs)
                .HasForeignKey(j => j.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Job>()
                .HasRequired(j => j.Rig)
                .WithMany(r => r.Jobs)
                .HasForeignKey(j => j.RigId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InventoryItem>()
                .HasRequired(i => i.Job)
                .WithMany(j => j.InventoryItems)
                .HasForeignKey(i => i.JobId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<InspectionChecklist>()
                .HasRequired(c => c.Job)
                .WithMany(j => j.Checklists)
                .HasForeignKey(c => c.JobId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<ChecklistItem>()
                .HasRequired(i => i.Checklist)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.ChecklistId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CorrectiveAction>()
                .HasRequired(a => a.Job)
                .WithMany(j => j.CorrectiveActions)
                .HasForeignKey(a => a.JobId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<DailyMeeting>()
                .HasRequired(m => m.Job)
                .WithMany(j => j.DailyMeetings)
                .HasForeignKey(m => m.JobId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<InspectionReport>()
                .HasRequired(r => r.Customer)
                .WithMany(c => c.InspectionReports)
                .HasForeignKey(r => r.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InspectionReport>()
                .HasOptional(r => r.Job)
                .WithMany(j => j.InspectionReports)
                .HasForeignKey(r => r.JobId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InspectionEquipment>()
                .HasRequired(e => e.InspectionReport)
                .WithMany(r => r.InspectionEquipments)
                .HasForeignKey(e => e.InspectionReportId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<InspectionConsumable>()
                .HasRequired(c => c.InspectionReport)
                .WithMany(r => r.InspectionConsumables)
                .HasForeignKey(c => c.InspectionReportId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<TestEvaluation>()
                .HasRequired(t => t.InspectionReport)
                .WithMany(r => r.TestEvaluations)
                .HasForeignKey(t => t.InspectionReportId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<InspectionReport>()
                .Property(r => r.SurfaceTemperature)
                .HasPrecision(10, 2);
        }
    }
}
