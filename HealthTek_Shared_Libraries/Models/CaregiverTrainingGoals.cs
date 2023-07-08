using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class CaregiverTrainingGoals
    {
        public CaregiverTrainingGoals()
        {
            LongTermObjectives = new List<LongTermObjectives>();
            ShortTermObjectives = new List<ShortTermObjectives>();
        }
        [NotMapped]
        public string? GoalType { get; set; }

        [DisplayName("ID")]
        public int CaregiverTrainingGoalsId { get; set; }

        [DisplayName("Maladaptive")]
        public int? FkMaladaptivesId { get; set; }

        [DisplayName("Replacement")]
        public int? FkReplacementsId { get; set; }

        [DisplayName("Preference")]
        public int? FkPreferencesId { get; set; }

        [DisplayName("Intervention")]
        public int? FkBaAssessmentsInterventionsId { get; set; }

        [DisplayName("Baseline")]
        public int Baseline { get; set; }

        [DisplayName("Collected Start")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime BaselineCollectedStart { get; set; }

        [DisplayName("Collected End")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime BaselineCollectedEnd { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("FkMaladaptivesId")]
        public Maladaptives? FkMaladaptives { get; set; }

        [ForeignKey("FkReplacementsId")]
        public Replacements? FkReplacements { get; set; }

        [ForeignKey("FkPreferencesId")]
        public Preferences? FkPreferences { get; set; }

        public BaAssessmentsInterventions? FkBaAssessmentsInterventions { get; set; }

        public virtual List<LongTermObjectives>? LongTermObjectives { get; set; }

        public virtual List<ShortTermObjectives>? ShortTermObjectives { get; set; }
    }
}
