using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPI_Report.Models
{
    public class Rig
    {
        [Key]
        public int RigId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Location { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

        public Rig()
        {
            Jobs = new HashSet<Job>();
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
    }
}
