using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class BaCrisisPlan
    {
        [DisplayName("ID")]
        public int BaCrisisPlanId { get; set; }

        [DisplayName("BA Assessment")]
        public int FkBaAssessmentsId { get; set; }

        [DisplayName("Applies?")]
        public bool DoesApply { get; set; }

        [DisplayName("Assaultive?")]
        public bool IsAssaultive { get; set; }

        [DisplayName("Self-Injurious?")]
        public bool IsSelfInjurious { get; set; }

        [DisplayName("Fire-Setting?")]
        public bool IsFireSetting { get; set; }

        [DisplayName("Impulsive?")]
        public bool IsImpulsive { get; set; }

        [DisplayName("Self-Mutilating?")]
        public bool IsSelfMutilating { get; set; }

        [DisplayName("Family Violence?")]
        public bool IsFamilyViolence { get; set; }

        [DisplayName("Prior Psychiatric?")]
        public bool IsPriorPsychiatric { get; set; }

        [DisplayName("Elopement?")]
        public bool IsElopement { get; set; }

        [DisplayName("Sexually Offending?")]
        public bool IsSexuallyOffending { get; set; }

        [DisplayName("Substance Abuse?")]
        public bool IsSubstanceAbuse { get; set; }

        [DisplayName("Psychotic?")]
        public bool IsPsychotic { get; set; }

        [DisplayName("Caring For Ill Family?")]
        public bool IsCaringForIllFamily { get; set; }

        [DisplayName("Coping with Loss?")]
        public bool IsCopingWithLoss { get; set; }

        [DisplayName("Crisis Plan")]
        [Required(ErrorMessage = "Please enter Crisis Plan.")]
        [DataType(DataType.Text)]
        public string? CrisisPlan { get; set; }

#nullable enable
        [DisplayName("Other:")]
        [DataType(DataType.Text)]
        public string? OtherRiskFactor { get; set; }
#nullable disable

        [DisplayName("Other")]
        public bool OtherRisks { get; set; }

        [DisplayName("Not Present")]
        public bool SuicidalityNotPresent { get; set; }

        [DisplayName("Ideation")]
        public bool SuicidalityIdeation { get; set; }

        [DisplayName("Plan")]
        public bool SuicidalityPlan { get; set; }

        [DisplayName("Means")]
        public bool SuicidalityMeans { get; set; }

        [DisplayName("Prior Attempt")]
        public bool SuicidalityPriorAttempt { get; set; }

        [DisplayName("Not Present")]
        public bool HomicidalityNotPresent { get; set; }

        [DisplayName("Ideation")]
        public bool HomicidalityIdeation { get; set; }

        [DisplayName("Plan")]
        public bool HomicidalityPlan { get; set; }

        [DisplayName("Means")]
        public bool HomicidalityMeans { get; set; }

        [DisplayName("Prior Attempt")]
        public bool HomicidalityPriorAttempt { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual BaAssessments? FkBaAssessments { get; set; }

    }
}
