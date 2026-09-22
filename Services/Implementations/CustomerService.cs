
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPI_Report.Models;
using MPI_Report.Data.Repositories.Interfaces;
using MPI_Report.Services.Interfaces;
using MPI_Report.ViewModels;

namespace MPI_Report.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly Mapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, Mapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        // Get a single customer
        public async Task<CustomerFormViewModel> GetByIdAsync(int id)
        {
            Customer customer =
                await _customerRepository.GetByIdAsync(id);

            if (customer == null)
            {
                return null;
            }

            return _mapper.Map<CustomerFormViewModel>(customer);
        }

        // Get all customers
        public async Task<IList<CustomerListItemViewModel>> GetAllAsync()
        {
            IList<Customer> customers =
                await _customerRepository.GetAllAsync();

            return _mapper.Map<IList<CustomerListItemViewModel>>(customers);
        }

        // Search/filter customers
        public async Task<IList<CustomerListItemViewModel>> SearchAsync(
            string search,
            string status)
        {
            IList<Customer> customers =
                await _customerRepository.SearchAsync(search, status);

            return _mapper.Map<IList<CustomerListItemViewModel>>(customers);
        }

        // Create customer
        public async Task<int> CreateAsync(CustomerFormViewModel model)
        {
            if (model == null)
            {
                return 0;
            }

            Customer customer =
                _mapper.Map<Customer>(model);

            int customerId =
                await _customerRepository.CreateAsync(customer);

            return customerId;
        }

        // Update customer
        public async Task<bool> UpdateAsync(CustomerFormViewModel model)
        {
            if (model == null)
            {
                return false;
            }

            Customer customer =
                _mapper.Map<Customer>(model);

            return await _customerRepository.UpdateAsync(customer);
        }

        // Delete customer
        public async Task<bool> DeleteAsync(int id)
        {
            return await _customerRepository.DeleteAsync(id);
        }
    }
}
