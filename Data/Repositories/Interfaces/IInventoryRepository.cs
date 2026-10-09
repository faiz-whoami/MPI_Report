using System.Collections.Generic;
using System.Threading.Tasks;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        Task<IList<InventoryItem>> ListAsync(int jobId, string search, string status, string itemType);

        Task<InventoryItem> GetByIdAsync(int id);

        Task<int> CreateAsync(InventoryItem item);

        Task<bool> UpdateAsync(InventoryItem item);

        Task<bool> DeleteAsync(int id, int jobId);
    }
}
