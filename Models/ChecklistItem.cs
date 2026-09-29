using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPI_Report.Models
{
    public class ChecklistItem
    {
        [Key]
        public int ChecklistItemId { get; set; }

        [Required]
        public int ChecklistId { get; set; }

        [ForeignKey("ChecklistId")]
        public virtual InspectionChecklist Checklist { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        [StringLength(50)]
        public string Result { get; set; }
    }
}
