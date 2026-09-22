using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPI_Report.Models;
using System.ComponentModel.DataAnnotations;


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
}