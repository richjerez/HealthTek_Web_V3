using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class CaregiverCompChecksCatalog
    {
        [DisplayName("ID")]
        public int CaregiverCompChecksCatalogId { get; set; }

        [DisplayName("Training Item")]
        [Required(ErrorMessage = "Please enter Training Item.")]
        [DataType(DataType.Text)]
        public string? TrainingItem { get; set; }

        [DisplayName("Description")]
        [DataType(DataType.Text)]
        public string? TrainingDescription { get; set; }

        [DisplayName("Instructions")]
        [DataType(DataType.Text)]
        public string? TrainingInstructions { get; set; }

        [DisplayName("Usage Count")]
        public int UsageCount { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }
    }
}
