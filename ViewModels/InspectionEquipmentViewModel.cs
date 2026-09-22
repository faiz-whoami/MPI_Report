using System;
using System.ComponentModel.DataAnnotations;

namespace MPI_Report.ViewModels
{
    public class InspectionEquipmentViewModel
    {
        public int InspectionEquipmentId { get; set; }

        [Required(ErrorMessage = "Equipment name is required.")]
        [StringLength(150)]
        [Display(Name = "Equipment Name")]
        public string EquipmentName { get; set; }

        [StringLength(100)]
        [Display(Name = "Equipment Serial No.")]
        public string EquipmentSerialNo { get; set; }

        [Display(Name = "Calibration Date")]
        public DateTime? CalibrationDate { get; set; }

        [Display(Name = "Calibration Due Date")]
        public DateTime? CalibrationDueDate { get; set; }
    }
}