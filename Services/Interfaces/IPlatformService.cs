using System.Collections.Generic;
using MPI_Report.Models;

namespace MPI_Report.Services.Interfaces
{
    public interface IPlatformService
    {
        IList<Job> GetActiveJobs();

        Job GetJobById(int jobId);

        DashboardCounts GetDashboardCounts(int? jobId);

        IList<InventoryItem> GetInventory(int jobId);

        void AddInventory(InventoryItem item);

        IList<InspectionChecklist> GetChecklists(int jobId);

        void AddChecklist(InspectionChecklist checklist);

        IList<CorrectiveAction> GetCorrectiveActions(int jobId);

        void AddCorrectiveAction(CorrectiveAction action);

        IList<DailyMeeting> GetDailyMeetings(int jobId);

        void AddDailyMeeting(DailyMeeting meeting);
    }

    public class DashboardCounts
    {
        public int InventoryCount { get; set; }

        public int OpenCorrectiveActionCount { get; set; }

        public int DailyMeetingCount { get; set; }

        public int ChecklistCount { get; set; }

        public int MpiReportCount { get; set; }
    }
}
