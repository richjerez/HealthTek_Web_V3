using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class ReplacementsCatalog
    {
        [DisplayName("ID")]
        public int ReplacementsCatalogId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Replacement.")]
        [DisplayName("Replacement")]
        public string? Replacement { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }
    }
}
