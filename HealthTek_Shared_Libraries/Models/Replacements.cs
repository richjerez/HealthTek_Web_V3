using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class Replacements
    {
        public Replacements()
        {
            LongTermObjectives = new List<LongTermObjectives>();
            ShortTermObjectives = new List<ShortTermObjectives>();
        }

        [DisplayName("ID")]
        public int ReplacementsId { get; set; }

        [DisplayName("Maladaptive")]
        public int FkMaladaptivesId { get; set; }

        [DisplayName("BA Assessment")]
        public int FkBaAssessmentsId { get; set; }

        [DisplayName("Caregiver Training Goal")]
        public int? FkCaregiverTrainingGoalsId { get; set; }

        [DisplayName("Baseline Average")]
        public int? BaselineAverage { get; set; }

        [DisplayName("Baseline 1")]
        public int? Baseline1 { get; set; }

        [DisplayName("Baseline 2")]
        public int? Baseline2 { get; set; }

        [DisplayName("Baseline 3")]
        public int? Baseline3 { get; set; }

        [DisplayName("Duration")]
        public int? BaselineDuration { get; set; }

        [DisplayName("Baseline Duration Unit")]
        [DataType(DataType.Text)]
        public string? BaselineDurationUnit { get; set; }

        [Required(ErrorMessage = "Please enter Replacement.")]
        [DisplayName("Replacement")]
        [DataType(DataType.Text)]
        public string? ReplacementName { get; set; }

        [DisplayName("Barrier")]
        [DataType(DataType.Text)]
        public string? Barrier { get; set; }

        [DisplayName("Prompt Level")]
        [DataType(DataType.Text)]
        public string? PromptLevel { get; set; }

        [DisplayName("Collection Method")]
        [DataType(DataType.Text)]
        public string? CollectionMethod { get; set; }

        [DisplayName("Comment")]
        [DataType(DataType.Text)]
        public string? ReplacementComment { get; set; }

        [DisplayName("Baseline Start Date 1")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline1StartDate { get; set; }

        [DisplayName("Baseline Start Date 2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline2StartDate { get; set; }

        [DisplayName("Baseline Start Date 3")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline3StartDate { get; set; }

        [DisplayName("Baseline End Date 1")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline1EndDate { get; set; }

        [DisplayName("Baseline End Date 2")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline2EndDate { get; set; }

        [DisplayName("Baseline End Date 3")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline3EndDate { get; set; }

        [DisplayName("Archived Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? ArchivedDate { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual BaAssessments? FkBaAssessments { get; set; }

        public CaregiverTrainingGoals? FkCaregiverTrainingGoals { get; set; }

        public Maladaptives? FkMaladaptives { get; set; }

        public virtual List<LongTermObjectives>? LongTermObjectives { get; set; }

        public virtual ReplacementMeasurements? ReplacementMeasurements { get; set; }

        public virtual List<ShortTermObjectives>? ShortTermObjectives { get; set; }
    }
}
