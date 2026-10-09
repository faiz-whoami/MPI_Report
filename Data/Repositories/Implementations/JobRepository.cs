using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MPI_Report.Data.Repositories.Interfaces;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Implementations
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Job>> ListAsync(string search, string status, int? customerId)
        {
            IQueryable<Job> query = _context.Jobs
                .Include(j => j.Customer)
                .Include(j => j.Rig)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(j =>
                    j.JobNo.Contains(search) ||
                    (j.Description != null && j.Description.Contains(search)) ||
                    (j.Customer != null && j.Customer.Name.Contains(search)) ||
                    (j.Rig != null && j.Rig.Name.Contains(search)));
            }

            if (customerId.HasValue)
            {
                int id = customerId.Value;
                query = query.Where(j => j.CustomerId == id);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                status = status.Trim().ToLowerInvariant();
                if (status == "active")
                {
                    query = query.Where(j => j.IsActive);
                }
                else if (status == "inactive")
                {
                    query = query.Where(j => !j.IsActive);
                }
            }

            return await query.OrderByDescending(j => j.IsActive).ThenBy(j => j.JobNo).ToListAsync();
        }

        public Task<Job> GetByIdAsync(int jobId)
        {
            return _context.Jobs
                .Include(j => j.Customer)
                .Include(j => j.Rig)
                .FirstOrDefaultAsync(j => j.JobId == jobId);
        }

        public async Task<IList<Job>> GetActiveAsync()
        {
            return await _context.Jobs
                .Include(j => j.Customer)
                .Include(j => j.Rig)
                .AsNoTracking()
                .Where(j => j.IsActive)
                .OrderBy(j => j.JobNo)
                .ToListAsync();
        }

        public async Task<int> CreateAsync(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return job.JobId;
        }

        public async Task<bool> UpdateAsync(Job job)
        {
            Job existing = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == job.JobId);
            if (existing == null)
            {
                return false;
            }

            existing.JobNo = job.JobNo;
            existing.Description = job.Description;
            existing.CustomerId = job.CustomerId;
            existing.RigId = job.RigId;
            existing.StartDate = job.StartDate;
            existing.IsActive = job.IsActive;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
