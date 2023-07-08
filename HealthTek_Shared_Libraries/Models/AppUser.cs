using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public class AppUser : IdentityUser
    {
        public string? Avatar { get; set; }

        [Display(Name = "Employee")]
        public string? FkEmployeesId { get; set; }
        public int FkLoginId { get; set; }
        [NotMapped]
        public string? StatusMessage { get; set; }

    }
}
