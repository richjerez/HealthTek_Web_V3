using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class CaregiverCompChecks
    {
        public CaregiverCompChecks()
        {
        }

        [DisplayName("ID")]
        public int CaregiverCompChecksId { get; set; }

        [DisplayName("Caregiver Competency")]
        public int FkCaregiverCompetenciesId { get; set; }

        public int FkCaregiverComptChecksCatalogId { get; set; }

        [DisplayName("Client")]
        public int FkClientsId { get; set; }

        [DisplayName("Baseline")]
        public int CompetencyBaseline { get; set; }

        [DisplayName("Level")]
        public int CompetencyLevel { get; set; }

        [DisplayName("Competency Summary")]
        [Required(ErrorMessage = "Please enter Summary.")]
        [DataType(DataType.Text)]
        public string? CompetencySummary { get; set; }

        [DisplayName("Maladaptive Check?")]
        public int FkMaladaptivesId { get; set; }

        [DisplayName("Intervention Check?")]
        public int FkInterventionsId { get; set; }

        [DisplayName("Reinforcer Check?")]
        public int FkReinforcersId { get; set; }

        [DisplayName("Replacement Check?")]
        public int FkReplacementsId { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual CaregiverCompChecksCatalog FkCaregiverComptChecksCatalog { get; set; }

        public virtual CaregiverCompetencies FkCaregiverCompetencies { get; set; }

        public virtual Clients FkClients { get; set; }
    }
}
