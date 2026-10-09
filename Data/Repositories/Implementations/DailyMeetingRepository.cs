using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MPI_Report.Data.Repositories.Interfaces;
using MPI_Report.Models;

namespace MPI_Report.Data.Repositories.Implementations
{
    public class DailyMeetingRepository : IDailyMeetingRepository
    {
        private readonly ApplicationDbContext _context;

        public DailyMeetingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<DailyMeeting>> ListAsync(int jobId)
        {
            return await _context.DailyMeetings
                .AsNoTracking()
                .Where(m => m.JobId == jobId)
                .OrderByDescending(m => m.MeetingDate)
                .ToListAsync();
        }

        public Task<DailyMeeting> GetByIdAsync(int id)
        {
            return _context.DailyMeetings.FirstOrDefaultAsync(m => m.DailyMeetingId == id);
        }

        public async Task<int> CreateAsync(DailyMeeting meeting)
        {
            _context.DailyMeetings.Add(meeting);
            await _context.SaveChangesAsync();
            return meeting.DailyMeetingId;
        }

        public async Task<bool> UpdateAsync(DailyMeeting meeting)
        {
            DailyMeeting existing = await _context.DailyMeetings
                .FirstOrDefaultAsync(m => m.DailyMeetingId == meeting.DailyMeetingId);
            if (existing == null)
            {
                return false;
            }

            existing.MeetingDate = meeting.MeetingDate;
            existing.Description = meeting.Description;
            existing.Signatures = meeting.Signatures;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
