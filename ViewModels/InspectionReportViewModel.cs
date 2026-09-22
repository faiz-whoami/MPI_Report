using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPI_Report.Models;

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
}