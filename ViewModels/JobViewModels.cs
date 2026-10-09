using System;
using System.ComponentModel.DataAnnotations;

namespace MPI_Report.ViewModels
{
    public class JobListItemViewModel
    {
        public int JobId { get; set; }

        public string JobNo { get; set; }

        public string Description { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int RigId { get; set; }

        public string RigName { get; set; }

        public DateTime? StartDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsCurrent { get; set; }
    }

    public class JobFormViewModel
    {
        public int JobId { get; set; }

        [Required]
        [StringLength(100)]
        public string JobNo { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int RigId { get; set; }

        public DateTime? StartDate { get; set; }

        public bool IsActive { get; set; }
    }
}
