using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class ClientInsurancesCatalog
    {
        public ClientInsurancesCatalog()
        {

        }

        [DisplayName("ID")]
        public int ClientInsurancesCatalogId { get; set; }

        [DisplayName("Policy Name")]
        [Required(ErrorMessage = "Please enter Policy Name.")]
        [DataType(DataType.Text)]
        public string? PolicyName { get; set; }

        [DisplayName("Program")]
        [Required(ErrorMessage = "Please enter Policy Program.")]
        [DataType(DataType.Text)]
        public string? PolicyProgram { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }
#nullable enable
        public string? FullPolicy
        {
            get
            {
                if (!string.IsNullOrEmpty(PolicyProgram))
                    return PolicyName + ": (none)";
                else
                    return PolicyName + ": " + PolicyProgram;
            }
        }
#nullable disable
    }
}
