using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class ClientInsurances
    {
        [DisplayName("ID")]
        public int ClientInsurancesId { get; set; }

        [DisplayName("Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTime EffectiveDate { get; set; }

        [DisplayName("Expiration Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTime ExpirationDate { get; set; }

        [DisplayName("Policy Name")]
        [Required(ErrorMessage = "Please enter Policy Name.")]
        [DataType(DataType.Text)]
        public string? PolicyName { get; set; }

        [DisplayName("Policy Program")]
        [Required(ErrorMessage = "Please enter Policy Program.")]
        [DataType(DataType.Text)]
        public string? PolicyProgram { get; set; }

        [DisplayName("Status")]
        [DataType(DataType.Text)]
        public string? PolicyStatus { get; set; }

        [DisplayName("Policy Identifier")]
        [DataType(DataType.Text)]
        public string? PolicyIdentifier { get; set; }

        [DisplayName("Client ID")]
        public int FkClientsId { get; set; }

        [Display(Name = "Is Archived")]
        public bool IsArchived { get; set; }

        [Display(Name = "Is Verified")]
        public bool IsVerified { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Clients? FkClients { get; set; }
    }
}
