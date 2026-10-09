using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MPI_Report.Data.Repositories.Interfaces;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Implementations
{
    public class CorrectiveActionRepository : ICorrectiveActionRepository
    {
        private readonly ApplicationDbContext _context;

        public CorrectiveActionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<CorrectiveAction>> ListAsync(int jobId, string status, string criticality)
        {
            IQueryable<CorrectiveAction> query = _context.CorrectiveActions
                .AsNoTracking()
                .Where(a => a.JobId == jobId);

            if (!string.IsNullOrWhiteSpace(status) && status != "all")
            {
                if (status == "overdue")
                {
                    System.DateTime today = System.DateTime.Today;
                    query = query.Where(a => a.Status != "Closed" && a.DueDate.HasValue && a.DueDate < today);
                }
                else
                {
                    query = query.Where(a => a.Status == status);
                }
            }

            if (!string.IsNullOrWhiteSpace(criticality))
            {
                query = query.Where(a => a.Criticality == criticality);
            }

            return await query.OrderByDescending(a => a.CorrectiveActionId).ToListAsync();
        }

        public Task<CorrectiveAction> GetByIdAsync(int id)
        {
            return _context.CorrectiveActions.FirstOrDefaultAsync(a => a.CorrectiveActionId == id);
        }

        public async Task<int> CreateAsync(CorrectiveAction action)
        {
            _context.CorrectiveActions.Add(action);
            await _context.SaveChangesAsync();
            return action.CorrectiveActionId;
        }

        public async Task<bool> UpdateAsync(CorrectiveAction action)
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
