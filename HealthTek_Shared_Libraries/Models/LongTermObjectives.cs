using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class LongTermObjectives
    {
        [DisplayName("ID")]
        public int LongTermObjectivesId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Status.")]
        [DisplayName("Status")]
        public string? LtoStatus { get; set; }

        [DisplayName("Behavior")]
        public int? FkMaladaptivesId { get; set; }

        [DisplayName("Replacement")]
        public int? FkReplacementsId { get; set; }

        [DisplayName("Training Goal")]
        public int? FkCaregiverTrainingGoalsId { get; set; }

        [Required(ErrorMessage = "Please enter Description.")]
        [DisplayName("Objective Number")]
        public int ObjectiveNumber { get; set; }

        [DataType(DataType.Text)]
        //[Required(ErrorMessage = "Please enter Description.")]
        [DisplayName("Description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please enter LTO #.")]
        [DisplayName("LTO Goal")]
        public int? LtoGoal { get; set; }

        [DisplayName("Timeframe")]
        public string? Timeframe { get; set; }

        [DisplayName("Duration")]
        public int? Duration { get; set; }

        [DisplayName("Caregiver Competency?")]
        public bool IsCcc { get; set; }

        [DisplayName("Current LTO")]
        public bool IsCurrent { get; set; }

        [DisplayName("Type")]
        public string? LtoType { get; set; }

        [DisplayName("Initiate Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime? InitiateDate { get; set; }

        [DisplayName("Mastery Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime? MasteryDate { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Maladaptives? FkMaladaptives { get; set; }

        public virtual Replacements? FkReplacements { get; set; }

        public virtual CaregiverTrainingGoals? FkCaregiverTrainingGoals { get; set; }
    }
}
