using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPI_Report.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        [StringLength(100)]
        public string JobNo { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [Required]
        public int RigId { get; set; }

        [ForeignKey("RigId")]
        public virtual Rig Rig { get; set; }

        public DateTime? StartDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<InventoryItem> InventoryItems { get; set; }

        public virtual ICollection<InspectionChecklist> Checklists { get; set; }

        public virtual ICollection<CorrectiveAction> CorrectiveActions { get; set; }

        public virtual ICollection<DailyMeeting> DailyMeetings { get; set; }

        public virtual ICollection<InspectionReport> InspectionReports { get; set; }

        public Job()
        {
            InventoryItems = new HashSet<InventoryItem>();
            Checklists = new HashSet<InspectionChecklist>();
            CorrectiveActions = new HashSet<CorrectiveAction>();
            DailyMeetings = new HashSet<DailyMeeting>();
            InspectionReports = new HashSet<InspectionReport>();
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
    }
}
