using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPI_Report.Models
{
    public class InventoryItem
    {
        [Key]
        public int InventoryItemId { get; set; }

        [Required]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }

        [StringLength(50)]
        public string ItemCode { get; set; }

        [Required]
        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Position { get; set; }

        [StringLength(100)]
        public string ItemType { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(50)]
        public string SpecialType { get; set; }
    }
}
