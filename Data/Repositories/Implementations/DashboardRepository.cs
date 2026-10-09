using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MPI_Report.Data.Repositories.Interfaces;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Implementations
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Job>> GetActiveJobsAsync()
        {
            return await _context.Jobs
                .Include(j => j.Customer)
                .Include(j => j.Rig)
                .AsNoTracking()
                .Where(j => j.IsActive)
                .OrderBy(j => j.JobNo)
                .ToListAsync();
        }

        public Task<Job> GetJobByIdAsync(int jobId)
        {
            return _context.Jobs
                .Include(j => j.Customer)
                .Include(j => j.Rig)
                .AsNoTracking()
                .FirstOrDefaultAsync(j => j.JobId == jobId);
        }

        public Task<int> CountInventoryAsync(int? jobId)
        {
            return Filter(_context.InventoryItems, jobId, i => i.JobId).CountAsync();
        }

        public Task<int> CountOpenActionsAsync(int? jobId)
        {
            return Filter(_context.CorrectiveActions, jobId, a => a.JobId)
                .CountAsync(a => a.Status == "Open");
        }

        public Task<int> CountOverdueActionsAsync(int? jobId, DateTime today)
        {
            return Filter(_context.CorrectiveActions, jobId, a => a.JobId)
                .CountAsync(a => a.Status != "Closed" && a.DueDate.HasValue && a.DueDate < today);
        }

        public Task<int> CountMeetingsAsync(int? jobId)
        {
            return Filter(_context.DailyMeetings, jobId, m => m.JobId).CountAsync();
        }

        public Task<int> CountChecklistsAsync(int? jobId)
        {
            return Filter(_context.InspectionChecklists, jobId, c => c.JobId).CountAsync();
        }

        public Task<int> CountChecklistItemsAsync(int? jobId)
        {
            IQueryable<ChecklistItem> query = _context.ChecklistItems.AsNoTracking();
            if (jobId.HasValue)
            {
                int id = jobId.Value;
                query = query.Where(i => i.Checklist.JobId == id);
            }

            return query.CountAsync();
        }

        public Task<int> CountCompletedChecklistItemsAsync(int? jobId)
        {
            IQueryable<ChecklistItem> query = _context.ChecklistItems.AsNoTracking();
            if (jobId.HasValue)
            {
                int id = jobId.Value;
                query = query.Where(i => i.Checklist.JobId == id);
            }

            return query.CountAsync(i => i.IsCompleted);
        }

        public Task<int> CountReportsAsync(int? jobId)
        {
            IQueryable<InspectionReport> query = _context.InspectionReports.AsNoTracking();
            if (jobId.HasValue)
            {
                int id = jobId.Value;
                query = query.Where(r => r.JobId == id);
            }

            return query.CountAsync();
        }

        public Task<IList<Tuple<string, int>>> InventoryByStatusAsync(int? jobId)
        {
            return GroupCounts(Filter(_context.InventoryItems, jobId, i => i.JobId), i => i.Status);
        }

        public Task<IList<Tuple<string, int>>> ActionsByCriticalityAsync(int? jobId)
        {
            return GroupCounts(Filter(_context.CorrectiveActions, jobId, a => a.JobId), a => a.Criticality);
        }

        public Task<IList<Tuple<string, int>>> ActionsByStatusAsync(int? jobId)
        {
            return GroupCounts(Filter(_context.CorrectiveActions, jobId, a => a.JobId), a => a.Status);
        }

        public Task<IList<Tuple<string, int>>> ReportsByResultAsync(int? jobId)
        {
            IQueryable<InspectionReport> query = _context.InspectionReports.AsNoTracking();
            if (jobId.HasValue)
            {
                int id = jobId.Value;
                query = query.Where(r => r.JobId == id);
            }

            return GroupCounts(query, r => r.InspectionResult);
        }

        public async Task<IList<CorrectiveAction>> OverdueActionsAsync(int? jobId, DateTime today, int take)
        {
            return await Filter(_context.CorrectiveActions, jobId, a => a.JobId)
                .Where(a => a.Status != "Closed" && a.DueDate.HasValue && a.DueDate < today)
                .OrderBy(a => a.DueDate)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IList<CorrectiveAction>> OpenActionsAsync(int? jobId, int take)
        {
            return await Filter(_context.CorrectiveActions, jobId, a => a.JobId)
                .Where(a => a.Status == "Open")
                .OrderBy(a => a.DueDate)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IList<DailyMeeting>> RecentMeetingsAsync(int? jobId, int take)
        {
            return await Filter(_context.DailyMeetings, jobId, m => m.JobId)
                .OrderByDescending(m => m.MeetingDate)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IList<InspectionReport>> RecentReportsAsync(int? jobId, int take)
        {
            IQueryable<InspectionReport> query = _context.InspectionReports
                .Include(r => r.Customer)
                .AsNoTracking();
            if (jobId.HasValue)
            {
                int id = jobId.Value;
                query = query.Where(r => r.JobId == id);
            }

            return await query.OrderByDescending(r => r.InspectionDate).Take(take).ToListAsync();
        }

        public async Task<IList<InspectionEquipment>> CalibrationDueAsync(int? jobId, DateTime until, int take)
        {
            IQueryable<InspectionEquipment> query = _context.InspectionEquipments
                .Include(e => e.InspectionReport)
                .AsNoTracking()
                .Where(e => e.CalibrationDueDate.HasValue && e.CalibrationDueDate <= until);

            if (jobId.HasValue)
            {
                int id = jobId.Value;
                query = query.Where(e => e.InspectionReport.JobId == id);
            }

            return await query.OrderBy(e => e.CalibrationDueDate).Take(take).ToListAsync();
        }

        private static IQueryable<T> Filter<T>(IQueryable<T> source, int? jobId, Func<T, int> jobSelector)
        {
            IQueryable<T> query = source.AsNoTracking();
            if (!jobId.HasValue)
            {
                return query;
            }

            int id = jobId.Value;
            return query.Where(item => jobSelector(item) == id);
        }

        private static async Task<IList<Tuple<string, int>>> GroupCounts<T>(
            IQueryable<T> query,
            System.Linq.Expressions.Expression<Func<T, string>> selector)
        {
            var rows = await query
                .GroupBy(selector)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .ToListAsync();

            return rows
                .Select(row => Tuple.Create(string.IsNullOrWhiteSpace(row.Name) ? "Unspecified" : row.Name, row.Count))
                .ToList();
        }
    }
}
