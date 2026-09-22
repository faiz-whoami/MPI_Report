using System.Collections.Generic;
using System.Threading.Tasks;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetByIdAsync(int id);

        Task<IList<Customer>> GetAllAsync();

        Task<IList<Customer>> SearchAsync(string search, string status);

        Task<int> CreateAsync(Customer customer);

        Task<bool> UpdateAsync(Customer customer);

        Task<bool> DeleteAsync(int id);
    }
}