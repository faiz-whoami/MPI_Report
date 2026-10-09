using System;
using System.ComponentModel.DataAnnotations;

namespace MPI_Report.ViewModels
{
    public class DailyMeetingListItemViewModel
    {
        public int DailyMeetingId { get; set; }

        public DateTime MeetingDate { get; set; }

        public string Description { get; set; }

        public string Signatures { get; set; }
    }

    public class DailyMeetingFormViewModel
    {
        public int DailyMeetingId { get; set; }

        [Required]
        public DateTime MeetingDate { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Signatures { get; set; }
    }
}
