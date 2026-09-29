namespace MPI_Report.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MPI_Report.Infrastructure;
    using MPI_Report.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MPI_Report.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MPI_Report.Data.ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(
                r => r.Name,
                new Role { Name = "Admin" },
                new Role { Name = "Inspector" },
                new Role { Name = "Reviewer" },
                new Role { Name = "Client" });

            context.SaveChanges();

            if (!context.Users.Any(u => u.Username == "admin"))
            {
                Role adminRole = context.Roles.First(r => r.Name == "Admin");
                User admin = new User();
                admin.Username = "admin";
                admin.PasswordHash = PasswordHasher.Hash("Admin@123");
                admin.DisplayName = "Administrator";
                admin.RoleId = adminRole.RoleId;
                admin.IsActive = true;
                admin.CreatedDate = DateTime.Now;
                context.Users.Add(admin);
                context.SaveChanges();
            }

            Customer customer = context.Customers.FirstOrDefault(c => c.Name == "Sample Drilling Co.");
            if (customer == null)
            {
                return;
            }

            if (!context.Rigs.Any(r => r.Name == "Sample Rig"))
            {
                Rig rig = new Rig();
                rig.Name = "Sample Rig";
                rig.Location = "Dubai";
                rig.CustomerId = customer.CustomerId;
                rig.IsActive = true;
                rig.CreatedDate = DateTime.Now;
                context.Rigs.Add(rig);
                context.SaveChanges();
            }

            Rig savedRig = context.Rigs.First(r => r.Name == "Sample Rig");

            if (!context.Jobs.Any(j => j.JobNo == "Sky-NDT-Inspect-01"))
            {
                Job job = new Job();
                job.JobNo = "Sky-NDT-Inspect-01";
                job.Description = "MPI sample job";
                job.CustomerId = customer.CustomerId;
                job.RigId = savedRig.RigId;
                job.StartDate = new DateTime(2024, 3, 29);
                job.IsActive = true;
                job.CreatedDate = DateTime.Now;
                context.Jobs.Add(job);
                context.SaveChanges();
            }

            Job savedJob = context.Jobs.First(j => j.JobNo == "Sky-NDT-Inspect-01");

            InspectionReport report = context.InspectionReports
                .FirstOrDefault(r => r.ReportNo == "Sky-NDT-Inspect-01/7");
            if (report != null && !report.JobId.HasValue)
            {
                report.JobId = savedJob.JobId;
            }

            if (!context.InventoryItems.Any(i => i.JobId == savedJob.JobId))
            {
                InventoryItem item = new InventoryItem();
                item.JobId = savedJob.JobId;
                item.ItemCode = "WIN-001";
                item.Description = "Air / Hydraulic Winches Utility Winches / Tuggers";
                item.Position = "Deck";
                item.ItemType = "Winch";
                item.Status = "Satisfactory";
                item.SpecialType = "MPI";
                context.InventoryItems.Add(item);
            }

            if (!context.CorrectiveActions.Any(a => a.JobId == savedJob.JobId))
            {
                CorrectiveAction action = new CorrectiveAction();
                action.JobId = savedJob.JobId;
                action.Title = "Paint touch-up on winch frame";
                action.Description = "Sample open corrective action.";
                action.Criticality = "Minor";
                action.Status = "Open";
                action.DueDate = new DateTime(2024, 4, 15);
                context.CorrectiveActions.Add(action);
            }

            if (!context.DailyMeetings.Any(m => m.JobId == savedJob.JobId))
            {
                DailyMeeting meeting = new DailyMeeting();
                meeting.JobId = savedJob.JobId;
                meeting.MeetingDate = new DateTime(2024, 3, 29);
                meeting.Description = "Pre-inspection toolbox talk";
                meeting.Signatures = "Muhammad Iftikhar";
                context.DailyMeetings.Add(meeting);
            }

            context.SaveChanges();
        }
    }
}
