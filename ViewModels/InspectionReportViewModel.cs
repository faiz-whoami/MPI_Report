using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPI_Report.Models;
using System.ComponentModel.DataAnnotations;

namespace MPI_Report.ViewModels
{
    public class InspectionReportViewModel
    {
        public string Search { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string InspectionResult { get; set; }

        public int? CustomerId { get; set; }

        public IList<InspectionReportListItemViewModel> Items { get; set; }

        public InspectionReportViewModel()
        {
            Search = string.Empty;
            InspectionResult = string.Empty;
            Items = new List<InspectionReportListItemViewModel>();
        }
    }
    public class InspectionReportListItemViewModel
    {
        public int InspectionReportId { get; set; }

        public string ReportNo { get; set; }

        public string WorkOrderNo { get; set; }

        public string CustomerName { get; set; }

        public string TestLocation { get; set; }

        public DateTime InspectionDate { get; set; }

        public DateTime? RecommendedDueDate { get; set; }

        public string InspectionResult { get; set; }
    }

    //Inspection Report Form View MOdel
    public class InspectionReportFormViewModel
        {
            public int InspectionReportId { get; set; }

            // Report Identification

            [Required(ErrorMessage = "Work order number is required.")]
            [StringLength(100)]
            [Display(Name = "Work Order No.")]
            public string WorkOrderNo { get; set; }

            [Required(ErrorMessage = "Report number is required.")]
            [StringLength(100)]
            [Display(Name = "Report No.")]
            public string ReportNo { get; set; }

            [Required(ErrorMessage = "Inspection date is required.")]
            [Display(Name = "Inspection Date")]
            public DateTime InspectionDate { get; set; }

            [Display(Name = "Recommended Due Date")]
            public DateTime? RecommendedDueDate { get; set; }

            [Required(ErrorMessage = "Customer is required.")]
            [Display(Name = "Customer")]
            public int CustomerId { get; set; }

            [Required(ErrorMessage = "Test location is required.")]
            [StringLength(150)]
            [Display(Name = "Test Location")]
            public string TestLocation { get; set; }


            // Inspected Material Particular

            [StringLength(250)]
            [Display(Name = "Item Description")]
            public string ItemDescription { get; set; }

            [StringLength(100)]
            [Display(Name = "Item Serial No.")]
            public string ItemSerialNo { get; set; }

            [StringLength(150)]
            [Display(Name = "Manufacturer")]
            public string Manufacturer { get; set; }

            [StringLength(150)]
            [Display(Name = "Rating / SWL")]
            public string RatingSWL { get; set; }

            [StringLength(150)]
            [Display(Name = "Model No.")]
            public string ModelNo { get; set; }

            [StringLength(150)]
            [Display(Name = "Service Type")]
            public string ServiceType { get; set; }

            [StringLength(150)]
            [Display(Name = "Inspection Type / Category")]
            public string InspectionTypeCategory { get; set; }

            [StringLength(100)]
            [Display(Name = "Material")]
            public string Material { get; set; }

            [StringLength(100)]
            [Display(Name = "Surface Condition")]
            public string SurfaceCondition { get; set; }

            [StringLength(100)]
            [Display(Name = "Lifting Capacity")]
            public string LiftingCapacity { get; set; }

            [Display(Name = "Surface Temperature (°C)")]
            public decimal? SurfaceTemperature { get; set; }

            [StringLength(100)]
            [Display(Name = "Sensitivity")]
            public string Sensitivity { get; set; }


            // Inspection Method Particular

            [StringLength(50)]
            [Display(Name = "Test Method")]
            public string TestMethod { get; set; }

            [StringLength(50)]
            [Display(Name = "Lighting Method")]
            public string LightingMethod { get; set; }

            [StringLength(50)]
            [Display(Name = "Magnetism Type")]
            public string MagnetismType { get; set; }

            [StringLength(50)]
            [Display(Name = "Current Type")]
            public string CurrentType { get; set; }

            [StringLength(50)]
            [Display(Name = "Magnetic Field Direction")]
            public string MagneticFieldDirection { get; set; }

            [StringLength(250)]
            [Display(Name = "Procedure No.")]
            public string ProcedureNo { get; set; }


            // Inspection Consumables / Acceptance

            [StringLength(100)]
            [Display(Name = "Acceptance Criteria")]
            public string AcceptanceCriteria { get; set; }

            [StringLength(100)]
            [Display(Name = "Lighting Type")]
            public string LightingType { get; set; }


            // Report / Inspection Details

            [StringLength(500)]
            [Display(Name = "Inspected Item Image")]
            public string InspectedItemImagePath { get; set; }

            [Display(Name = "Comments")]
            public string Comments { get; set; }

            [StringLength(250)]
            [Display(Name = "Area of Testing")]
            public string AreaOfTesting { get; set; }

            [Display(Name = "Restricted Access")]
            public string RestrictedAccess { get; set; }

            [Display(Name = "Inspection Remarks")]
            public string InspectionRemarks { get; set; }

            [Required(ErrorMessage = "Inspection result is required.")]
            [StringLength(50)]
            [Display(Name = "Inspection Result")]
            public string InspectionResult { get; set; }


            // Inspector

            [Required(ErrorMessage = "Inspector name is required.")]
            [StringLength(150)]
            [Display(Name = "Inspector Name")]
            public string InspectorName { get; set; }

            [StringLength(250)]
            [Display(Name = "Inspector Qualification")]
            public string InspectorQualification { get; set; }

            [StringLength(500)]
            [Display(Name = "Inspector Signature")]
            public string InspectorSignaturePath { get; set; }

            [Display(Name = "Inspector Signed Date")]
            public DateTime? InspectorSignedDate { get; set; }


            // Reviewer

            [StringLength(150)]
            [Display(Name = "Reviewer Name")]
            public string ReviewerName { get; set; }

            [StringLength(150)]
            [Display(Name = "Reviewer Designation")]
            public string ReviewerDesignation { get; set; }

            [StringLength(500)]
            [Display(Name = "Reviewer Signature")]
            public string ReviewerSignaturePath { get; set; }

            [Display(Name = "Reviewed Date")]
            public DateTime? ReviewedDate { get; set; }


            // Repeating Sections

            public IList<InspectionEquipmentViewModel> Equipment { get; set; }

            public IList<InspectionConsumableViewModel> Consumables { get; set; }

            public IList<TestEvaluationViewModel> TestEvaluations { get; set; }


            public InspectionReportFormViewModel()
            {
                Equipment = new List<InspectionEquipmentViewModel>();

                Consumables = new List<InspectionConsumableViewModel>();

                TestEvaluations = new List<TestEvaluationViewModel>();

                InspectionDate = DateTime.Now;

                InspectionResult = "Satisfactory";
            }
    }

    //Inspection Report Detail View Model
    public class InspectionReportDetailsViewModel
        {
            public int InspectionReportId { get; set; }

            public string WorkOrderNo { get; set; }

            public string ReportNo { get; set; }

            public DateTime InspectionDate { get; set; }

            public DateTime? RecommendedDueDate { get; set; }

            public int CustomerId { get; set; }

            public string CustomerName { get; set; }

            public string TestLocation { get; set; }

            public string ItemDescription { get; set; }

            public string ItemSerialNo { get; set; }

            public string Manufacturer { get; set; }

            public string RatingSWL { get; set; }

            public string ModelNo { get; set; }

            public string ServiceType { get; set; }

            public string InspectionTypeCategory { get; set; }

            public string Material { get; set; }

            public string SurfaceCondition { get; set; }

            public string LiftingCapacity { get; set; }

            public decimal? SurfaceTemperature { get; set; }

            public string Sensitivity { get; set; }

            public string TestMethod { get; set; }

            public string LightingMethod { get; set; }

            public string MagnetismType { get; set; }

            public string CurrentType { get; set; }

            public string MagneticFieldDirection { get; set; }

            public string ProcedureNo { get; set; }

            public string AcceptanceCriteria { get; set; }

            public string LightingType { get; set; }

            public string InspectedItemImagePath { get; set; }

            public string Comments { get; set; }

            public string AreaOfTesting { get; set; }

            public string RestrictedAccess { get; set; }

            public string InspectionRemarks { get; set; }

            public string InspectionResult { get; set; }

            public string InspectorName { get; set; }

            public string InspectorQualification { get; set; }

            public string InspectorSignaturePath { get; set; }

            public DateTime? InspectorSignedDate { get; set; }

            public string ReviewerName { get; set; }

            public string ReviewerDesignation { get; set; }

            public string ReviewerSignaturePath { get; set; }

            public DateTime? ReviewedDate { get; set; }

            public IList<InspectionEquipmentViewModel> Equipment { get; set; }

            public IList<InspectionConsumableViewModel> Consumables { get; set; }

            public IList<TestEvaluationViewModel> TestEvaluations { get; set; }

            public InspectionReportDetailsViewModel()
            {
                Equipment = new List<InspectionEquipmentViewModel>();

                Consumables = new List<InspectionConsumableViewModel>();

                TestEvaluations = new List<TestEvaluationViewModel>();
            }
        }

}