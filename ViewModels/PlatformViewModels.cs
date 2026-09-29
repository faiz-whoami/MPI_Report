using System.Collections.Generic;
using MPI_Report.Models;

namespace MPI_Report.ViewModels
{
    public class DashboardViewModel
    {
        public string SelectedJobNo { get; set; }

        public int InventoryCount { get; set; }

        public int OpenCorrectiveActionCount { get; set; }

        public int DailyMeetingCount { get; set; }

        public int ChecklistCount { get; set; }

        public int MpiReportCount { get; set; }

        public IList<Job> Jobs { get; set; }

        public int? SelectedJobId { get; set; }

        public DashboardViewModel()
        {
            Jobs = new List<Job>();
            SelectedJobNo = string.Empty;
        }
    }
}
