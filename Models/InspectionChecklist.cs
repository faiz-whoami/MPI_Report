using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPI_Report.Models
{
    public class InspectionChecklist
    {
        [Key]
        public int ChecklistId { get; set; }

        [Required]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Frequency { get; set; }

        public DateTime ChecklistDate { get; set; }

        public virtual ICollection<ChecklistItem> Items { get; set; }

        public InspectionChecklist()
        {
            Items = new HashSet<ChecklistItem>();
            ChecklistDate = DateTime.Now;
        }
    }
}
