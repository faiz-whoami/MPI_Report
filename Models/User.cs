using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPI_Report.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(80)]
        public string Username { get; set; }

        [Required]
        [StringLength(128)]
        public string PasswordHash { get; set; }

        [StringLength(150)]
        public string DisplayName { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public User()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
    }
}
