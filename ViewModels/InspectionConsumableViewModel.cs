using System.ComponentModel.DataAnnotations;

namespace MPI_Report.ViewModels
{
    public class InspectionConsumableViewModel
    {
        public int InspectionConsumableId { get; set; }

        [Required(ErrorMessage = "Consumable name is required.")]
        [StringLength(150)]
        [Display(Name = "Consumable")]
        public string ConsumableName { get; set; }

        [StringLength(150)]
        [Display(Name = "Brand / Type")]
        public string BrandType { get; set; }
    }
}