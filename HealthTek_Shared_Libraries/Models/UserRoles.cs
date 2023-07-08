using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public class UserRoles : IdentityRole
    {
        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}