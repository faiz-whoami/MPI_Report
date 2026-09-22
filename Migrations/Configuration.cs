namespace MPI_Report.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MPI_Report.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MPI_Report.Data.ApplicationDbContext context)
        {
            // Sample data is in Scripts/SeedMpiCert1.sql
        }
    }
}
