using System.ComponentModel.DataAnnotations;

namespace MPI_Report.ViewModels
{
    public class InventoryListItemViewModel
    {
        public int InventoryItemId { get; set; }

        public string ItemCode { get; set; }

        public string Description { get; set; }

        public string Position { get; set; }

        public string ItemType { get; set; }

        public string Status { get; set; }

        public string SpecialType { get; set; }
    }

    public class InventoryFormViewModel
    {
        public int InventoryItemId { get; set; }

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
