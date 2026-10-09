using System.Collections.Generic;
using System.Threading.Tasks;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Interfaces
{
    public interface IRigRepository
    {
        Task<IList<Rig>> ListAsync(string search, string status, int? customerId);

        Task<Rig> GetByIdAsync(int rigId);

        Task<IList<Rig>> GetByCustomerIdAsync(int customerId);

        Task<int> CreateAsync(Rig rig);

        Task<bool> UpdateAsync(Rig rig);
    }
}
