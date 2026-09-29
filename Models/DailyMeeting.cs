using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPI_Report.Models
{
    public class DailyMeeting
    {
        [Key]
        public int DailyMeetingId { get; set; }

        [Required]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual Job Job { get; set; }

        public DateTime MeetingDate { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Signatures { get; set; }

        public DailyMeeting()
        {
            MeetingDate = DateTime.Now;
        }
    }
}
