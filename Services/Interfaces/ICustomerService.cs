using System.Collections.Generic;
using System.Threading.Tasks;
using MPI_Report.ViewModels;

namespace MPI_Report.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerFormViewModel> GetByIdAsync(int id);

        Task<IList<CustomerListItemViewModel>> GetAllAsync();

        Task<IList<CustomerListItemViewModel>> SearchAsync(
            string search,
            string status);

        Task<int> CreateAsync(CustomerFormViewModel model);

        Task<bool> UpdateAsync(CustomerFormViewModel model);

        Task<bool> DeleteAsync(int id);
    }
}