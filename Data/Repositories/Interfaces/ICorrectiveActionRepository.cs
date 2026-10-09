using System.Collections.Generic;
using System.Threading.Tasks;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Interfaces
{
    public interface ICorrectiveActionRepository
    {
        Task<IList<CorrectiveAction>> ListAsync(int jobId, string status, string criticality);

        Task<CorrectiveAction> GetByIdAsync(int id);

        Task<int> CreateAsync(CorrectiveAction action);

        Task<bool> UpdateAsync(CorrectiveAction action);
    }
}
