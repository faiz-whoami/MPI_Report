using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPI_Report.Models
{
    public class InspectionEquipment
    {
        [Key]
        public int InspectionEquipmentId { get; set; }

        [Required]
        public int InspectionReportId { get; set; }

        [ForeignKey("InspectionReportId")]
        public virtual InspectionReport InspectionReport { get; set; }

        [Required]
        [StringLength(150)]
        public string EquipmentName { get; set; }

        [StringLength(100)]
        public string EquipmentSerialNo { get; set; }

        public DateTime? CalibrationDate { get; set; }

        public DateTime? CalibrationDueDate { get; set; }
    }
}