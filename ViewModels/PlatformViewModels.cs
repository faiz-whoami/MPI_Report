using System.Collections.Generic;
using MPI_Report.Models;

namespace MPI_Report.ViewModels
{
    public class DashboardJobViewModel
    {
        public int JobId { get; set; }

        public string JobNo { get; set; }

        public string CustomerName { get; set; }

        public string RigName { get; set; }
    }

    public class DashboardViewModel
    {
        public string SelectedJobNo { get; set; }

        public int InventoryCount { get; set; }

        public int OpenCorrectiveActionCount { get; set; }

        public int DailyMeetingCount { get; set; }

        public int ChecklistCount { get; set; }

        public int MpiReportCount { get; set; }

        public IList<DashboardJobViewModel> Jobs { get; set; }

        public int? SelectedJobId { get; set; }

        public DashboardViewModel()
        {
            Jobs = new List<DashboardJobViewModel>();
            SelectedJobNo = string.Empty;
        }
    }
}
