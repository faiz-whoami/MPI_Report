using System.ComponentModel.DataAnnotations;

namespace MPI_Report.ViewModels
{
    public class TestEvaluationViewModel
    {
        public int TestEvaluationId { get; set; }

        [StringLength(50)]
        [Display(Name = "Joint No.")]
        public string JointNo { get; set; }

        [StringLength(100)]
        [Display(Name = "Welder ID")]
        public string WelderId { get; set; }

        [StringLength(50)]
        [Display(Name = "Weld Length")]
        public string WeldLength { get; set; }

        [StringLength(250)]
        [Display(Name = "Discontinuity")]
        public string Discontinuity { get; set; }

        [StringLength(100)]
        [Display(Name = "Start Location")]
        public string StartLocation { get; set; }

        [StringLength(100)]
        [Display(Name = "End Location")]
        public string EndLocation { get; set; }

        [StringLength(50)]
        [Display(Name = "Defect Length")]
        public string DefectLength { get; set; }

        [StringLength(100)]
        [Display(Name = "Evaluation")]
        public string Evaluation { get; set; }
    }
}