using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class Preferences
    {
        [DisplayName("ID")]
        public int PreferencesId { get; set; }

        [DisplayName("Client")]
        public int FkClientsId { get; set; }

        [DisplayName("Reinforcer")]
        public int FkReinforcersCatalogId { get; set; }

        [DisplayName("Caregiver Training Goal")]
        public int FkCaregiverTrainingGoalsId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Preference.")]
        [DisplayName("Preference")]
        public string? Preference { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Source")]
        public string? Source { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual CaregiverTrainingGoals? FkCaregiverTrainingGoals { get; set; }

        public virtual Clients? FkClients { get; set; }

        public virtual ReinforcerCatalog? FkReinforcersCatalog { get; set; }
    }
}
