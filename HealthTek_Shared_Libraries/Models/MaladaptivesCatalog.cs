using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Entity to Capture a Catalog of Maladaptives
/// </summary>
namespace HealthTek_Shared_Libraries
{
    public partial class MaladaptivesCatalog
    {
        public MaladaptivesCatalog()
        {
        }

        [DisplayName("ID")]
        public int MaladaptivesCatalogId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Maladaptive.")]
        [DisplayName("Maladaptive")]
        public string? MaladaptiveName { get; set; }

        [DisplayName("Created")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        public DateTime LastUpdateDate { get; set; }

    }
}
