using System;
using System.ComponentModel.DataAnnotations;

namespace MPI_Report.ViewModels
{
    public class CorrectiveActionListItemViewModel
    {
        public int CorrectiveActionId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Criticality { get; set; }

        public string Status { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public string ClosedComments { get; set; }

        public bool IsOverdue { get; set; }
    }

    public class CorrectiveActionFormViewModel
    {
        public int CorrectiveActionId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        [StringLength(50)]
        public string Criticality { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public DateTime? DueDate { get; set; }

        public string ClosedComments { get; set; }
    }

    public class CorrectiveActionCloseViewModel
    {
        [Required]
        public int CorrectiveActionId { get; set; }

        public string ClosedComments { get; set; }
    }
}
