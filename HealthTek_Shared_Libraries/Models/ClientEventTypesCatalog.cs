using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class ClientEventTypesCatalog
    {
        [DisplayName("ID")]
        public int ClientEventTypesCatalogId { get; set; }

        [DisplayName("Event Type")]
        [Required(ErrorMessage = "Please enter Event Type.")]
        [DataType(DataType.Text)]
        public string? EventType { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }
    }
}
