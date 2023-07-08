using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class BaAssessmentsInterventions
    {
        [DisplayName("ID")]
        public int BaAssessmentsInterventionsId { get; set; }

        [DisplayName("Maladaptive")]
        public int FkBaAssessmentsId { get; set; }

        [DisplayName("Intervention")]
        public int FkInterventionsId { get; set; }

        [DisplayName("Caregiver Training Goal")]
        public int FkCaregiverTrainingGoalsId { get; set; }

        [DisplayName("Comment")]
        [DataType(DataType.Text)]
        public string? InterventionComment { get; set; }

        [DisplayName("Other Intervention Name (Optional)")]
        [DataType(DataType.Text)]
        public string? InterventionName { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("FkCaregiverTrainingGoalsId")]
        public CaregiverTrainingGoals? FkCaregiverTrainingGoals { get; set; }

        public virtual BaAssessments? FkBaAssessments { get; set; }

        public virtual Interventions? FkInterventions { get; set; }
    }
}
