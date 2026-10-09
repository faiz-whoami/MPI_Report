using System;
using System.ComponentModel.DataAnnotations;

namespace MPI_Report.ViewModels
{
    public class RoleOptionViewModel
    {
        public int RoleId { get; set; }

        public string Name { get; set; }
    }

    public class UserListItemViewModel
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string DisplayName { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class UserFormViewModel
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(80)]
        public string Username { get; set; }

        [StringLength(150)]
        public string DisplayName { get; set; }

        [Required]
        public int RoleId { get; set; }

        public bool IsActive { get; set; }

        [StringLength(128)]
        public string Password { get; set; }
    }
}
