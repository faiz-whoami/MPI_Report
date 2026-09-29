using System.Data.Entity;
using System.Linq;
using MPI_Report.Data;
using MPI_Report.Infrastructure;
using MPI_Report.Models;
using MPI_Report.Services.Interfaces;

namespace MPI_Report.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public User Validate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            User user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username && u.IsActive);

            if (user == null)
            {
                return null;
            }

            if (!PasswordHasher.Verify(password, user.PasswordHash))
            {
                return null;
            }

            return user;
        }
    }
}
