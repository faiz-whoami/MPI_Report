using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MPI_Report.Data.Repositories.Interfaces;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Implementations
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<InventoryItem>> ListAsync(int jobId, string search, string status, string itemType)
        {
            IQueryable<InventoryItem> query = _context.InventoryItems
                .AsNoTracking()
                .Where(i => i.JobId == jobId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(i =>
                    (i.ItemCode != null && i.ItemCode.Contains(search)) ||
                    i.Description.Contains(search) ||
                    (i.Position != null && i.Position.Contains(search)));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(i => i.Status == status);
            }

            if (!string.IsNullOrWhiteSpace(itemType))
            {
                query = query.Where(i => i.ItemType == itemType);
            }

            return await query.OrderBy(i => i.Description).ToListAsync();
        }

        public Task<InventoryItem> GetByIdAsync(int id)
        {
            return _context.InventoryItems.FirstOrDefaultAsync(i => i.InventoryItemId == id);
        }

        public async Task<int> CreateAsync(InventoryItem item)
        {
            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();
            return item.InventoryItemId;
        }

        public async Task<bool> UpdateAsync(InventoryItem item)
        {
            InventoryItem existing = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.InventoryItemId == item.InventoryItemId);
            if (existing == null)
            {
                return false;
            }

            existing.ItemCode = item.ItemCode;
            existing.Description = item.Description;
            existing.Position = item.Position;
            existing.ItemType = item.ItemType;
            existing.Status = item.Status;
            existing.SpecialType = item.SpecialType;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id, int jobId)
        {
            InventoryItem existing = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.InventoryItemId == id && i.JobId == jobId);
            if (existing == null)
            {
                return false;
            }

            _context.InventoryItems.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
