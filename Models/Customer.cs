using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MPI_Report.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(30)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        // Navigation Property
        public virtual ICollection<InspectionReport> InspectionReports { get; set; }

        public Customer()
        {
            InspectionReports = new HashSet<InspectionReport>();

            IsActive = true;
            CreatedDate = DateTime.Now;
        }
    }
}