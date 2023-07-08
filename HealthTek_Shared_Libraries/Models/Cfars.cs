using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class Cfars
    {
        [DisplayName("ID")]
        public int? CfarsId { get; set; }

        [DisplayName("Appt ID")]
        public int? FkAppointmentsId { get; set; }

        [DisplayName("Employee E-Sign ID")]
        public int? FkEmployeeSignatureId { get; set; }

        [DisplayName("DcfOutcomesReport")]
        public string? DcfOutcomesReport { get; set; }

        [DisplayName("ProgramEvaluation")]
        public string? ProgramEvaluation { get; set; }

        [DisplayName("SubstanceAbuseHistory")]
        public string? SubstanceAbuseHistory { get; set; }

        [DisplayName("EducationalCategory")]
        public string? EducationalCategory { get; set; }

        [DisplayName("CfarsRatersNote")]
        public string? CfarsRatersNote { get; set; }

        /// <summary>
        /// The ratings are int's because will need to iterate on the form
        /// </summary>
        [DisplayName("DepressionRating")]
        public int? DepressionRating { get; set; }

        [DisplayName("HyperAffectRating")]
        public int? HyperAffectRating { get; set; }

        [DisplayName("CognitivePerformanceRating")]
        public int? CognitivePerformanceRating { get; set; }

        [DisplayName("TraumaticStressRating")]
        public int? TraumaticStressRating { get; set; }

        [DisplayName("InterpersonalRelationshipsRating")]
        public int? InterpersonalRelationshipsRating { get; set; }

        [DisplayName("FamilyEnvironmentRating")]
        public int? FamilyEnvironmentRating { get; set; }

        [DisplayName("WorkSchoolRating")]
        public int? WorkSchoolRating { get; set; }

        [DisplayName("AbilityToCareForSelfRating")]
        public int? AbilityToCareForSelfRating { get; set; }

        [DisplayName("DangerToOthersRating")]
        public int? DangerToOthersRating { get; set; }

        [DisplayName("AnxietyRating")]
        public int? AnxietyRating { get; set; }

        [DisplayName("ThoughtProcessRating")]
        public int? ThoughtProcessRating { get; set; }

        [DisplayName("MedicalPhysicalRating")]
        public int? MedicalPhysicalRating { get; set; }

        [DisplayName("SubstanceAbuseRating")]
        public int SubstanceAbuseRating { get; set; }

        [DisplayName("FamilyRelationshipsRating")]
        public int? FamilyRelationshipsRating { get; set; }

        [DisplayName("SocioLegalRating")]
        public int? SocioLegalRating { get; set; }

        [DisplayName("AdlFunctioningRating")]
        public int? AdlFunctioningRating { get; set; }

        [DisplayName("DangerToSelfRating")]
        public int? DangerToSelfRating { get; set; }

        [DisplayName("SecurityManagementNeedsRating")]
        public int? SecurityManagementNeedsRating { get; set; }
        /// <summary>
        /// End The ratings are int's
        /// </summary>

        [DisplayName("Depression")]
        public string? Depression { get; set; }

        [DisplayName("HyperAffect")]
        public string? HyperAffect { get; set; }

        [DisplayName("CognitivePerformance")]
        public string? CognitivePerformance { get; set; }

        [DisplayName("TraumaticStress")]
        public string? TraumaticStress { get; set; }

        [DisplayName("InterpersonalRelationships")]
        public string? InterpersonalRelationships { get; set; }

        [DisplayName("FamilyEnvironment")]
        public string? FamilyEnvironment { get; set; }

        [DisplayName("WorkSchool")]
        public string? WorkSchool { get; set; }

        [DisplayName("AbilityToCareForSelf")]
        public string? AbilityToCareForSelf { get; set; }

        [DisplayName("DangerToOthers")]
        public string? DangerToOthers { get; set; }

        [DisplayName("Anxiety")]
        public string? Anxiety { get; set; }

        [DisplayName("ThoughtProcess")]
        public string? ThoughtProcess { get; set; }

        [DisplayName("MedicalPhysical")]
        public string? MedicalPhysical { get; set; }

        [DisplayName("SubstanceAbuse")]
        public int SubstanceAbuse { get; set; }

        [DisplayName("FamilyRelationships")]
        public string? FamilyRelationships { get; set; }

        [DisplayName("SocioLegal")]
        public string? SocioLegal { get; set; }

        [DisplayName("AdlFunctioning")]
        public string? AdlFunctioning { get; set; }

        [DisplayName("DangerToSelf")]
        public string? DangerToSelf { get; set; }

        [DisplayName("SecurityManagementNeeds")]
        public string? SecurityManagementNeeds { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Appointments? FkAppointments { get; set; }

        public virtual ESignatures? FkEmployeeSignature { get; set; }

    }
}
