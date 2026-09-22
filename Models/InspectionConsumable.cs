using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MPI_Report.Models;

namespace MPI_Report.Models
{
    public class InspectionConsumable
    {
        [Key]
        public int InspectionConsumableId { get; set; }

        [Required]
        public int InspectionReportId { get; set; }

        [ForeignKey("InspectionReportId")]
        public virtual InspectionReport InspectionReport { get; set; }

        [Required]
        [StringLength(150)]
        public string ConsumableName { get; set; }

        [StringLength(150)]
        public string BrandType { get; set; }
    }
}