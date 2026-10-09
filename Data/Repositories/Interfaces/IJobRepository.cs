using System.Collections.Generic;
using System.Threading.Tasks;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Interfaces
{
    public interface IJobRepository
    {
        Task<IList<Job>> ListAsync(string search, string status, int? customerId);

        Task<Job> GetByIdAsync(int jobId);

        Task<IList<Job>> GetActiveAsync();

        Task<int> CreateAsync(Job job);

        Task<bool> UpdateAsync(Job job);
    }
}
