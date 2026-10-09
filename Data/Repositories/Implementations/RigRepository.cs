using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MPI_Report.Data.Repositories.Interfaces;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Implementations
{
    public class RigRepository : IRigRepository
    {
        private readonly ApplicationDbContext _context;

        public RigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Rig>> ListAsync(string search, string status, int? customerId)
        {
            IQueryable<Rig> query = _context.Rigs
                .Include(r => r.Customer)
                .Include(r => r.Jobs)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(r =>
                    r.Name.Contains(search) ||
                    (r.Location != null && r.Location.Contains(search)) ||
                    (r.Customer != null && r.Customer.Name.Contains(search)));
            }

            if (customerId.HasValue)
            {
                int id = customerId.Value;
                query = query.Where(r => r.CustomerId == id);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                status = status.Trim().ToLowerInvariant();
                if (status == "active")
                {
                    query = query.Where(r => r.IsActive);
                }
                else if (status == "inactive")
                {
                    query = query.Where(r => !r.IsActive);
                }
            }

            return await query.OrderBy(r => r.Name).ToListAsync();
        }

        public Task<Rig> GetByIdAsync(int rigId)
        {
            return _context.Rigs
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.RigId == rigId);
        }

        public async Task<IList<Rig>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Rigs
                .AsNoTracking()
                .Where(r => r.CustomerId == customerId && r.IsActive)
                .OrderBy(r => r.Name)
                .ToListAsync();
        }

        public async Task<int> CreateAsync(Rig rig)
        {
            _context.Rigs.Add(rig);
            await _context.SaveChangesAsync();
            return rig.RigId;
        }

        public async Task<bool> UpdateAsync(Rig rig)
        {
            Rig existing = await _context.Rigs.FirstOrDefaultAsync(r => r.RigId == rig.RigId);
            if (existing == null)
            {
                return false;
            }

            existing.Name = rig.Name;
            existing.Location = rig.Location;
            existing.CustomerId = rig.CustomerId;
            existing.IsActive = rig.IsActive;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
