using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class ClientEvents
    {
        [DisplayName("ID")]
        public int ClientEventsId { get; set; }

        [Display(Name = "Maladaptive")]
        public int FkMaladaptivesId { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime? EventStartDate { get; set; }

        [DisplayName("End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime? EventEndDate { get; set; }

        [DisplayName("Type")]
        [Required(ErrorMessage = "Please enter Event Type.")]
        [DataType(DataType.Text)]
        public string? EventType { get; set; }

        [DisplayName("Description")]
        [DataType(DataType.Text)]
        public string? EventDescription { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Maladaptives? FkMaladaptives { get; set; }
    }
}
