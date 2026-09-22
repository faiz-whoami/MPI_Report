using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MPI_Report.Data;
using MPI_Report.Models;
using MPI_Report.Data.Repositories.Interfaces;

namespace MPI_Report.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get Customer By Id
        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }


        // Get All Customers
        public async Task<IList<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .ToListAsync();
        }


        // Search Customers
        public async Task<IList<Customer>> SearchAsync(
            string search,
            string status)
        {
            IQueryable<Customer> query =
                _context.Customers.AsNoTracking();

            // Search by name, email or phone
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(c =>
                    c.Name.Contains(search) ||
                    c.Email.Contains(search) ||
                    c.Phone.Contains(search));
            }

            // Filter by active/inactive status
            if (!string.IsNullOrWhiteSpace(status))
            {
                status = status.Trim().ToLower();

                if (status == "active")
                {
                    query = query.Where(c => c.IsActive);
                }
                else if (status == "inactive")
                {
                    query = query.Where(c => !c.IsActive);
                }
            }

            return await query.ToListAsync();
        }


        // Create Customer
        public async Task<int> CreateAsync(Customer customer)
        {
            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return customer.CustomerId;
        }


        // Update Customer
        public async Task<bool> UpdateAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers
                .FirstOrDefaultAsync(c =>
                    c.CustomerId == customer.CustomerId);

            if (existingCustomer == null)
            {
                return false;
            }

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Address = customer.Address;
            existingCustomer.IsActive = customer.IsActive;

            await _context.SaveChangesAsync();

            return true;
        }


        // Delete Customer
        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}