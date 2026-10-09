using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<IList<Job>> GetActiveJobsAsync();

        Task<Job> GetJobByIdAsync(int jobId);

        Task<int> CountInventoryAsync(int? jobId);

        Task<int> CountOpenActionsAsync(int? jobId);

        Task<int> CountOverdueActionsAsync(int? jobId, DateTime today);

        Task<int> CountMeetingsAsync(int? jobId);

        Task<int> CountChecklistsAsync(int? jobId);

        Task<int> CountChecklistItemsAsync(int? jobId);

        Task<int> CountCompletedChecklistItemsAsync(int? jobId);

        Task<int> CountReportsAsync(int? jobId);

        Task<IList<Tuple<string, int>>> InventoryByStatusAsync(int? jobId);

        Task<IList<Tuple<string, int>>> ActionsByCriticalityAsync(int? jobId);

        Task<IList<Tuple<string, int>>> ActionsByStatusAsync(int? jobId);

        Task<IList<Tuple<string, int>>> ReportsByResultAsync(int? jobId);

        Task<IList<CorrectiveAction>> OverdueActionsAsync(int? jobId, DateTime today, int take);

        Task<IList<CorrectiveAction>> OpenActionsAsync(int? jobId, int take);

        Task<IList<DailyMeeting>> RecentMeetingsAsync(int? jobId, int take);

        Task<IList<InspectionReport>> RecentReportsAsync(int? jobId, int take);

        Task<IList<InspectionEquipment>> CalibrationDueAsync(int? jobId, DateTime until, int take);
    }
}
