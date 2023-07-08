using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class Intakes
    {

        [DisplayName("ID")]
        public int IntakesId { get; set; }

        [DisplayName("Status")]
        public string IntakeStatus { get; set; }

        [DisplayName("Facility")]
        public int FkFacilitiesId { get; set; }

        [DisplayName("Client")]
        public int FkClientsId { get; set; }

        [Required(ErrorMessage = "The Effective Date is required.")]
        [DisplayName("Intake Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? IntakeEffectiveDate { get; set; }

        [Required(ErrorMessage = "The Expiration Date is required.")]
        [DisplayName("Intake Expiration Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? IntakeExpirationDate { get; set; }

#nullable enable
        [DataType(DataType.Text)]
        [DisplayName("Status Note")]
        public string? StatusNote { get; set; }
#nullable disable

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Facilities FkFacilities { get; set; }

        public virtual Clients FkClients { get; set; }

        [NotMapped]
        public virtual Documents Documents { get; set; }

        public virtual ICollection<Documents> IntakeDocumentation { get; set; }
    }
}
