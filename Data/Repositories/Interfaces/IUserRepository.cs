using System.Collections.Generic;
using System.Threading.Tasks;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IList<User>> ListAsync(string search, string status);

        Task<User> GetByIdAsync(int id);

        Task<bool> UsernameExistsAsync(string username, int? excludeUserId);

        Task<IList<Role>> GetRolesAsync();

        Task<int> CreateAsync(User user);

        Task<bool> UpdateAsync(User user);
    }
}
