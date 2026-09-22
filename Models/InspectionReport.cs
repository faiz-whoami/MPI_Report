using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPI_Report.Models
{
    public class InspectionReport
    {
        [Key]
        public int InspectionReportId { get; set; }
        
        //Report Identification
        [Required]
        [StringLength(100)]
        public string WorkOrderNo { get; set; }

        [Required]
        [StringLength(100)]
        public string ReportNo { get; set; }

        [Required]
        public DateTime InspectionDate { get; set; }

        public DateTime? RecommendedDueDate { get; set; }

        [StringLength(150)]
        public string TestLocation { get; set; }

        //Customer

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }


        //INSPECTED MATERIAL PARTICULAR
        [StringLength(250)]
        public string ItemDescription { get; set; }

        [StringLength(100)]
        public string ItemSerialNo { get; set; }

        [StringLength(150)]
        public string Manufacturer { get; set; }

        [StringLength(150)]
        public string RatingSWL { get; set; }

        [StringLength(150)]
        public string ModelNo { get; set; }

        [StringLength(150)]
        public string ServiceType { get; set; }

        [StringLength(150)]
        public string InspectionTypeCategory { get; set; }

        [StringLength(100)]
        public string Material { get; set; }

        //INSPECTION METHOD PARTICULAR

        [StringLength(50)]
        public string TestMethod { get; set; }

        [StringLength(50)]
        public string LightingMethod { get; set; }

        [StringLength(50)]
        public string MagnetismType { get; set; }

        [StringLength(50)]
        public string CurrentType { get; set; }

        [StringLength(50)]
        public string MagneticFieldDirection { get; set; }

        [StringLength(250)]
        public string ProcedureNo { get; set; }


        //TEST CONDITIONS
        [StringLength(100)]
        public string SurfaceCondition { get; set; }

        [StringLength(100)]
        public string LiftingCapacity { get; set; }

        public decimal? SurfaceTemperature { get; set; }

        [StringLength(100)]
        public string Sensitivity { get; set; }

        [StringLength(100)]
        public string AcceptanceCriteria { get; set; }

        [StringLength(100)]
        public string LightingType { get; set; }


        //INSPECTED ITEM IMAGE

        [StringLength(500)]
        public string InspectedItemImagePath { get; set; }


        //COMMENTS / RESULTS
        public string Comments { get; set; }

        [StringLength(250)]
        public string AreaOfTesting { get; set; }

        public string RestrictedAccess { get; set; }

        public string InspectionRemarks { get; set; }

        [StringLength(50)]
        public string InspectionResult { get; set; }


        // =====================================================
        // INSPECTOR
        // =====================================================

        [StringLength(150)]
        public string InspectorName { get; set; }

        [StringLength(250)]
        public string InspectorQualification { get; set; }

        [StringLength(500)]
        public string InspectorSignaturePath { get; set; }

        public DateTime? InspectorSignedDate { get; set; }


        //REVIEWER

        [StringLength(150)]
        public string ReviewerName { get; set; }

        [StringLength(150)]
        public string ReviewerDesignation { get; set; }

        [StringLength(500)]
        public string ReviewerSignaturePath { get; set; }

        public DateTime? ReviewedDate { get; set; }


        // =====================================================
        // AUDIT
        // =====================================================

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }


        // =====================================================
        // NAVIGATION PROPERTIES
        // =====================================================

        public virtual ICollection<InspectionEquipment> InspectionEquipments { get; set; }

        public virtual ICollection<InspectionConsumable> InspectionConsumables { get; set; }

        public virtual ICollection<TestEvaluation> TestEvaluations { get; set; }


        public InspectionReport()
        {
            InspectionEquipments = new HashSet<InspectionEquipment>();
            InspectionConsumables = new HashSet<InspectionConsumable>();
            TestEvaluations = new HashSet<TestEvaluation>();

            CreatedDate = DateTime.Now;
        }
    }
}