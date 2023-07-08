using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Entity to Capture 1-to-Many Relationship between RoleNames and DocumentationProcess
/// </summary>
namespace HealthTek_Shared_Libraries
{
    public partial class RoleDocsCatalog
    {

        [NotMapped]
        public IFormFile? CustomFile { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please enter at least one Role.")]
        public List<int>? HrRoles { get; set; }

        public int RoleDocsCatalogId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Expiration window.")]
        [Display(Name = "Expiration")]
        public string? Expiration { get; set; }

        [Display(Name = "Never Expires?")]
        public bool NeverExpires { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Template Document")]
        [Url]
        public string? TemplateUrl { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string? Description { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Title.")]
        [DisplayName("Title")]
        public string? Title { get; set; }

        [DisplayName("Required?")]
        public bool IsRequired { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Roles?")]
        public string? Roles { get; set; }

        [Display(Name = "Created")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last Updated")]
        public DateTime LastUpdateDate { get; set; }

        public virtual DocumentationProcess? DocumentationProcess { get; set; }

    }
}
