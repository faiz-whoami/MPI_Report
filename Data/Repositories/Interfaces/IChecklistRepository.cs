using System.Collections.Generic;
using System.Threading.Tasks;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Interfaces
{
    public interface IChecklistRepository
    {
        Task<IList<InspectionChecklist>> ListAsync(int jobId);

        Task<InspectionChecklist> GetByIdAsync(int id);

        Task<ChecklistItem> GetItemByIdAsync(int itemId);

        Task<int> CreateAsync(InspectionChecklist checklist);

        Task SaveAsync();
    }
}
