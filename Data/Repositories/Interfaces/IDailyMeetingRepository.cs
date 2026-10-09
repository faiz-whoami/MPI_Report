using System.Collections.Generic;
using System.Threading.Tasks;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Interfaces
{
    public interface IDailyMeetingRepository
    {
        Task<IList<DailyMeeting>> ListAsync(int jobId);

        Task<DailyMeeting> GetByIdAsync(int id);

        Task<int> CreateAsync(DailyMeeting meeting);

        Task<bool> UpdateAsync(DailyMeeting meeting);
    }
}
