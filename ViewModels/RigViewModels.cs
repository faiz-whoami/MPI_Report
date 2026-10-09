using System.ComponentModel.DataAnnotations;

namespace MPI_Report.ViewModels
{
    public class RigListItemViewModel
    {
        public int RigId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public bool IsActive { get; set; }

        public int JobCount { get; set; }
    }

    public class RigFormViewModel
    {
        public int RigId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Location { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public bool IsActive { get; set; }
    }
}
