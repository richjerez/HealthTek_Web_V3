using System;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class ClientContacts
    {
        [Display(Name = "ID")]
        public int ClientContactsId { get; set; }

        [Display(Name = "Client")]
        public int FkClientsId { get; set; }

        [Display(Name = "Location")]
        public int? FkLocationsId { get; set; }

        [Required(ErrorMessage = "Please enter Name.")]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string? ContactName { get; set; }

#nullable enable
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Alternate Number")]
        [DataType(DataType.PhoneNumber)]
        public string? AlternateNumber { get; set; }

        [Required(ErrorMessage = "Please enter Relationship.")]
        [Display(Name = "Relationship")]
        [DataType(DataType.Text)]
        public string? Relationship { get; set; }
#nullable disable

        [Display(Name = "Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Clients FkClients { get; set; }

        public Locations FkLocations { get; set; }
    }
}
