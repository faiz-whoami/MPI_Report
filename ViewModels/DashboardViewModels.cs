using System;
using System.Collections.Generic;

namespace MPI_Report.ViewModels
{
    public class NamedCountViewModel
    {
        public string Name { get; set; }

        public int Count { get; set; }
    }

    public class DashboardJobOptionViewModel
    {
        public int JobId { get; set; }

        public string JobNo { get; set; }

        public string CustomerName { get; set; }

        public string RigName { get; set; }
    }

    public class DashboardActionItemViewModel
    {
        public int CorrectiveActionId { get; set; }

        public string Title { get; set; }

        public string Criticality { get; set; }

        public string Status { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsOverdue { get; set; }
    }

    public class DashboardMeetingItemViewModel
    {
        public int DailyMeetingId { get; set; }

        public DateTime MeetingDate { get; set; }

        public string Description { get; set; }
    }

    public class DashboardReportItemViewModel
    {
        public int InspectionReportId { get; set; }

        public string ReportNo { get; set; }

        public string WorkOrderNo { get; set; }

        public DateTime InspectionDate { get; set; }

        public string InspectionResult { get; set; }

        public string CustomerName { get; set; }
    }

    public class DashboardCalibrationItemViewModel
    {
        public string EquipmentName { get; set; }

        public string EquipmentSerialNo { get; set; }

        public DateTime? CalibrationDueDate { get; set; }

        public string ReportNo { get; set; }

        public int InspectionReportId { get; set; }
    }

    public class DashboardSummaryViewModel
    {
        public int? SelectedJobId { get; set; }

        public string SelectedJobNo { get; set; }

        public string SelectedCustomerName { get; set; }

        public string SelectedRigName { get; set; }

        public int InventoryCount { get; set; }

        public int OpenCorrectiveActionCount { get; set; }

        public int OverdueCorrectiveActionCount { get; set; }

        public int DailyMeetingCount { get; set; }

        public int ChecklistCount { get; set; }

        public int ChecklistItemCount { get; set; }

        public int ChecklistCompletedCount { get; set; }

        public int ChecklistCompletionPercent { get; set; }

        public int MpiReportCount { get; set; }

        public IList<DashboardJobOptionViewModel> Jobs { get; set; }

        public IList<NamedCountViewModel> InventoryByStatus { get; set; }

        public IList<NamedCountViewModel> ActionsByCriticality { get; set; }

        public IList<NamedCountViewModel> ActionsByStatus { get; set; }

        public IList<NamedCountViewModel> ReportsByResult { get; set; }

        public IList<DashboardActionItemViewModel> OverdueActions { get; set; }

        public IList<DashboardActionItemViewModel> OpenActions { get; set; }

        public IList<DashboardMeetingItemViewModel> RecentMeetings { get; set; }

        public IList<DashboardReportItemViewModel> RecentReports { get; set; }

        public IList<DashboardCalibrationItemViewModel> CalibrationDue { get; set; }

        public DashboardSummaryViewModel()
        {
            SelectedJobNo = string.Empty;
            SelectedCustomerName = string.Empty;
            SelectedRigName = string.Empty;
            Jobs = new List<DashboardJobOptionViewModel>();
            InventoryByStatus = new List<NamedCountViewModel>();
            ActionsByCriticality = new List<NamedCountViewModel>();
            ActionsByStatus = new List<NamedCountViewModel>();
            ReportsByResult = new List<NamedCountViewModel>();
            OverdueActions = new List<DashboardActionItemViewModel>();
            OpenActions = new List<DashboardActionItemViewModel>();
            RecentMeetings = new List<DashboardMeetingItemViewModel>();
            RecentReports = new List<DashboardReportItemViewModel>();
            CalibrationDue = new List<DashboardCalibrationItemViewModel>();
        }
    }
}
