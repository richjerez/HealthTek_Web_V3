using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class EnvironmentalsCatalog
    {
        [DisplayName("ID")]
        public int EnvironmentalsCatalogId { get; set; }

        [DisplayName("Description")]
        //[Required(ErrorMessage = "Please enter Description.")]
        [DataType(DataType.Text)]
        public string? Description { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Please enter Category.")]
        [DataType(DataType.Text)]
        public string? Category { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }
    }
}
