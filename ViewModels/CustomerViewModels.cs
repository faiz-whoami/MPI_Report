using MPI_Report.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace MPI_Report.ViewModels
{
    public class CustomerListViewModel
    {
        public string Search { get; set; }

        public string Status { get; set; }

        public IList<Customer> Items { get; set; }

        public CustomerListViewModel()
        {
            Search = string.Empty;
            Status = "active";
            Items = new List<Customer>();
        }
    }
    public class CustomerFormViewModel
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(150)]
        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(150)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(30)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [StringLength(250)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
    public class CustomerListItemViewModel
        {
            public int CustomerId { get; set; }

            public string Name { get; set; }

            public string Email { get; set; }

            public string Phone { get; set; }

            public string Address { get; set; }

            public bool IsActive { get; set; }
        }
    
}