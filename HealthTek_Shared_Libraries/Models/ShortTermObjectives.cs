using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class ShortTermObjectives
    {
        [DisplayName("ID")]
        public int ShortTermObjectivesId { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Status")]
        public string? StoStatus { get; set; }

        [DisplayName("Maladaptive")]
        public int? FkMaladaptivesId { get; set; }

        [DisplayName("Replacement")]
        public int? FkReplacementsId { get; set; }

        [DisplayName("Training Goal")]
        public int? FkCaregiverTrainingGoalsId { get; set; }

        [DisplayName("Objective Number")]
        [Required(ErrorMessage = "Please enter Objective #.")]
        public int ObjectiveNumber { get; set; }

        [DisplayName("Reduction Number")]
        public int? ReductionNumber { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Description")]
        //[Required(ErrorMessage = "Please enter Description.")]
        public string? Description { get; set; }

        [DisplayName("STO Length")]
        public int Duration { get; set; }

        [DisplayName("Reduction Percentage?")]
        public bool IsReductionPercentage { get; set; }

        [DisplayName("Reduced Number")]
        public int ReducedNumber { get; set; }

        [DisplayName("2nd Reduced Number")]
        public int? SecondReducedNumber { get; set; }

        [DisplayName("Automatic?")]
        public bool IsAutomatic { get; set; }

        [DisplayName("Caregiver Competency?")]
        public bool IsCcc { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Timeframe")]
        public string? Timeframe { get; set; }

        [DisplayName("Current STO")]
        public bool IsCurrent { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Type")]
        public string? StoType { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Initiate Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime? InitiateDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Mastery Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime? MasteryDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Maladaptives? FkMaladaptives { get; set; }

        public virtual Replacements? FkReplacements { get; set; }

        public virtual CaregiverTrainingGoals? FkCaregiverTrainingGoals { get; set; }
    }
}
