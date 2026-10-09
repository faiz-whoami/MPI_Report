using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MPI_Report.Data.Repositories.Interfaces;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<User>> ListAsync(string search, string status)
        {
            IQueryable<User> query = _context.Users.Include(u => u.Role).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(u =>
                    u.Username.Contains(search) ||
                    (u.DisplayName != null && u.DisplayName.Contains(search)));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                status = status.Trim().ToLowerInvariant();
                if (status == "active")
                {
                    query = query.Where(u => u.IsActive);
                }
                else if (status == "inactive")
                {
                    query = query.Where(u => !u.IsActive);
                }
            }

            return await query.OrderBy(u => u.Username).ToListAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == id);
        }

        public Task<bool> UsernameExistsAsync(string username, int? excludeUserId)
        {
            IQueryable<User> query = _context.Users.Where(u => u.Username == username);
            if (excludeUserId.HasValue)
            {
                int id = excludeUserId.Value;
                query = query.Where(u => u.UserId != id);
            }

            return query.AnyAsync();
        }

        public async Task<IList<Role>> GetRolesAsync()
        {
            return await _context.Roles.AsNoTracking().OrderBy(r => r.Name).ToListAsync();
        }

        public async Task<int> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
