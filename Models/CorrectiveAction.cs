using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPI_Report.Models
{
    public class CorrectiveAction
    {
        [Key]
        public int CorrectiveActionId { get; set; }

        [Required]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Criticality { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public string ClosedComments { get; set; }

        public CorrectiveAction()
        {
            Status = "Open";
        }
    }
}
