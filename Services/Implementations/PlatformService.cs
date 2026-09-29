using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MPI_Report.Data;
using MPI_Report.Models;
using MPI_Report.Services.Interfaces;

namespace MPI_Report.Services.Implementations
{
    public class PlatformService : IPlatformService
    {
        private readonly ApplicationDbContext _context;

        public PlatformService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Job> GetActiveJobs()
        {
            return _context.Jobs
                .Include(j => j.Customer)
                .Include(j => j.Rig)
                .Where(j => j.IsActive)
                .OrderBy(j => j.JobNo)
                .ToList();
        }

        public Job GetJobById(int jobId)
        {
            return _context.Jobs
                .Include(j => j.Customer)
                .Include(j => j.Rig)
                .FirstOrDefault(j => j.JobId == jobId);
        }

        public DashboardCounts GetDashboardCounts(int? jobId)
        {
            DashboardCounts counts = new DashboardCounts();

            IQueryable<InventoryItem> inventory = _context.InventoryItems;
            IQueryable<CorrectiveAction> actions = _context.CorrectiveActions;
            IQueryable<DailyMeeting> meetings = _context.DailyMeetings;
            IQueryable<InspectionChecklist> checklists = _context.InspectionChecklists;
            IQueryable<InspectionReport> reports = _context.InspectionReports;

            if (jobId.HasValue)
            {
                int id = jobId.Value;
                inventory = inventory.Where(i => i.JobId == id);
                actions = actions.Where(a => a.JobId == id);
                meetings = meetings.Where(m => m.JobId == id);
                checklists = checklists.Where(c => c.JobId == id);
                reports = reports.Where(r => r.JobId == id);
            }

            counts.InventoryCount = inventory.Count();
            counts.OpenCorrectiveActionCount = actions.Count(a => a.Status == "Open");
            counts.DailyMeetingCount = meetings.Count();
            counts.ChecklistCount = checklists.Count();
            counts.MpiReportCount = reports.Count();

            return counts;
        }

        public IList<InventoryItem> GetInventory(int jobId)
        {
            return _context.InventoryItems
                .Where(i => i.JobId == jobId)
                .OrderBy(i => i.Description)
                .ToList();
        }

        public void AddInventory(InventoryItem item)
        {
            _context.InventoryItems.Add(item);
            _context.SaveChanges();
        }

        public IList<InspectionChecklist> GetChecklists(int jobId)
        {
            return _context.InspectionChecklists
                .Include(c => c.Items)
                .Where(c => c.JobId == jobId)
                .OrderByDescending(c => c.ChecklistDate)
                .ToList();
        }

        public void AddChecklist(InspectionChecklist checklist)
        {
            _context.InspectionChecklists.Add(checklist);
            _context.SaveChanges();
        }

        public IList<CorrectiveAction> GetCorrectiveActions(int jobId)
        {
            return _context.CorrectiveActions
                .Where(a => a.JobId == jobId)
                .OrderByDescending(a => a.CorrectiveActionId)
                .ToList();
        }

        public void AddCorrectiveAction(CorrectiveAction action)
        {
            _context.CorrectiveActions.Add(action);
            _context.SaveChanges();
        }

        public IList<DailyMeeting> GetDailyMeetings(int jobId)
        {
            return _context.DailyMeetings
                .Where(m => m.JobId == jobId)
                .OrderByDescending(m => m.MeetingDate)
                .ToList();
        }

        public void AddDailyMeeting(DailyMeeting meeting)
        {
            _context.DailyMeetings.Add(meeting);
            _context.SaveChanges();
        }
    }
}
