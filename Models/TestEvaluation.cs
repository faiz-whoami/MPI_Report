using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPI_Report.Models
{
    public class TestEvaluation
    {
        [Key]
        public int TestEvaluationId { get; set; }

        [Required]
        public int InspectionReportId { get; set; }

        [ForeignKey("InspectionReportId")]
        public virtual InspectionReport InspectionReport { get; set; }


        [StringLength(50)]
        public string JointNo { get; set; }

        [StringLength(100)]
        public string WelderId { get; set; }

        [StringLength(50)]
        public string WeldLength { get; set; }

        [StringLength(250)]
        public string Discontinuity { get; set; }

        [StringLength(100)]
        public string StartLocation { get; set; }

        [StringLength(100)]
        public string EndLocation { get; set; }

        [StringLength(50)]
        public string DefectLength { get; set; }

        [StringLength(100)]
        public string Evaluation { get; set; }
    }
}