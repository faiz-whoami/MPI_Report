using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MPI_Report.Data.Repositories.Interfaces;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Implementations
{
    public class ChecklistRepository : IChecklistRepository
    {
        private readonly ApplicationDbContext _context;

        public ChecklistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<InspectionChecklist>> ListAsync(int jobId)
        {
            return await _context.InspectionChecklists
                .Include(c => c.Items)
                .Where(c => c.JobId == jobId)
                .OrderByDescending(c => c.ChecklistDate)
                .ToListAsync();
        }

        public Task<InspectionChecklist> GetByIdAsync(int id)
        {
            return _context.InspectionChecklists
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.ChecklistId == id);
        }

        public Task<ChecklistItem> GetItemByIdAsync(int itemId)
        {
            return _context.ChecklistItems
                .Include(i => i.Checklist)
                .FirstOrDefaultAsync(i => i.ChecklistItemId == itemId);
        }

        public async Task<int> CreateAsync(InspectionChecklist checklist)
        {
            _context.InspectionChecklists.Add(checklist);
            await _context.SaveChangesAsync();
            return checklist.ChecklistId;
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
