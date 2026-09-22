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

        public DbSet<InspectionReport> InspectionReports { get; set; }

        public DbSet<InspectionEquipment> InspectionEquipments { get; set; }

        public DbSet<InspectionConsumable> InspectionConsumables { get; set; }

        public DbSet<TestEvaluation> TestEvaluations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customer -> InspectionReport
            modelBuilder.Entity<InspectionReport>()
                .HasRequired(r => r.Customer)
                .WithMany(c => c.InspectionReports)
                .HasForeignKey(r => r.CustomerId)
                .WillCascadeOnDelete(false);

            // InspectionReport -> InspectionEquipment
            modelBuilder.Entity<InspectionEquipment>()
                .HasRequired(e => e.InspectionReport)
                .WithMany(r => r.InspectionEquipments)
                .HasForeignKey(e => e.InspectionReportId)
                .WillCascadeOnDelete(true);

            // InspectionReport -> InspectionConsumable
            modelBuilder.Entity<InspectionConsumable>()
                .HasRequired(c => c.InspectionReport)
                .WithMany(r => r.InspectionConsumables)
                .HasForeignKey(c => c.InspectionReportId)
                .WillCascadeOnDelete(true);

            // InspectionReport -> TestEvaluation
            modelBuilder.Entity<TestEvaluation>()
                .HasRequired(t => t.InspectionReport)
                .WithMany(r => r.TestEvaluations)
                .HasForeignKey(t => t.InspectionReportId)
                .WillCascadeOnDelete(true);

            // Surface temperature
            modelBuilder.Entity<InspectionReport>()
                .Property(r => r.SurfaceTemperature)
                .HasPrecision(10, 2);
        }
    }
}