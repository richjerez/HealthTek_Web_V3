using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Entity to Capture a Catalog of Intake Docs
/// </summary>
namespace HealthTek_Shared_Libraries
{
    public partial class IntakeDocsCatalog
    {
        [DisplayName("ID")]
        public int IntakeDocsCatalogId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Doc Name.")]
        [DisplayName("Title")]
        public string? IntakeDocName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string? IntakeDocDescription { get; set; }

        [DisplayName("Created On")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }
    }
}
