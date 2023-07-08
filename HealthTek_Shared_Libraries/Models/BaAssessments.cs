using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class BaAssessments
    {
        public BaAssessments()
        {
            AbcReports = new HashSet<AbcReports>();
            BaAssessmentsInterventions = new HashSet<BaAssessmentsInterventions>();
            BaCrisisPlan = new HashSet<BaCrisisPlan>();
            BaMonthlyReports = new HashSet<BaMonthlyReports>();
            Maladaptives = new HashSet<Maladaptives>();
            Replacements = new HashSet<Replacements>();
        }


        [DisplayName("ID")]
        public int BaAssessmentsId { get; set; }

        [DisplayName("Appt")]
        public int FkAppointmentsId { get; set; }

        public int? FkReAssessmentId { get; set; }

        [DisplayName("Analyst Signature")]
        public int? FkAnalystSignatureId { get; set; }

        [DisplayName("Supervisor Signature")]
        public int? FkSupervisorSignatureId { get; set; }

        [DisplayName("Reviewed Documents")]
        [DataType(DataType.Text)]
        public string? ReviewedDocuments { get; set; }

        [DisplayName("Background Information")]
        [DataType(DataType.Text)]
        public string? BackgroundInformation { get; set; }

        [DisplayName("Relevant Family History")]
        [DataType(DataType.Text)]
        public string? RelevantFamilyHistory { get; set; }

        [DisplayName("Major Areas of Concern")]
        [DataType(DataType.Text)]
        public string? MajorAreasOfConcern { get; set; }

        [DisplayName("Previous Treatments")]
        [DataType(DataType.Text)]
        public string? PreviousTreatments { get; set; }

        [DisplayName("Current Treatments")]
        [DataType(DataType.Text)]
        public string? CurrentTreatments { get; set; }

        [DisplayName("Education Status")]
        [DataType(DataType.Text)]
        public string? EducationStatus { get; set; }

        [DisplayName("Pysical/Medical Status")]
        [DataType(DataType.Text)]
        public string? PysicalMedicalStatus { get; set; }

        [DisplayName("Medications")]
        [DataType(DataType.Text)]
        public string? Medications { get; set; }

        [DisplayName("Strengths and Weaknesses")]
        [DataType(DataType.Text)]
        public string? StrengthsWeaknesses { get; set; }

        [DisplayName("Assessments Conducted")]
        [DataType(DataType.Text)]
        public string? AssessmentsConducted { get; set; }

        [DisplayName("Indirect Assessment Results and Description of Observations")]
        [DataType(DataType.Text)]
        public string? IndirectResultsAndObservations { get; set; }

        [DisplayName("Generalization & Maintenance")]
        [DataType(DataType.Text)]
        public string? GeneralizationMaintenance { get; set; }

        [DisplayName("Risk Assessment")]
        [DataType(DataType.Text)]
        public string? RiskAssessment { get; set; }

        [DisplayName("MedicalNecessity")]
        [DataType(DataType.Text)]
        public string? MedicalNecessity { get; set; }

        [DisplayName("Consent to Treatment")]
        [DataType(DataType.Text)]
        public string? ConsentToTreatment { get; set; }

#nullable enable
        [DisplayName("All Short-Term Objectives")]
        [DataType(DataType.Text)]
        public string? DischargeAllSto { get; set; }

        [DisplayName("Other Provider Drug Prescriber")]
        [DataType(DataType.Text)]
        public string? OtherProviderDrugPrescriber { get; set; }

        [DisplayName("Other Provider Drug PCP")]
        [DataType(DataType.Text)]
        public string? OtherProviderPcp { get; set; }

        [DisplayName("Other Provider Drug Documented Communication")]
        [DataType(DataType.Text)]
        public string? OtherProviderDocumentedComm { get; set; }

        [DisplayName("Other Provider BH Communication")]
        [DataType(DataType.Text)]
        public string? OtherProviderBhComm { get; set; }

        [DisplayName("Other Provider BH Type")]
        [DataType(DataType.Text)]
        public string? OtherProviderBhCommType { get; set; }

        [DisplayName("Start Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime EmployeeScheduledStartTime { get; set; }

        [DisplayName("End Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime EmployeeScheduledEndTime { get; set; }

        [DisplayName("Summary & Recommendations")]
        [DataType(DataType.Text)]
        public string? SummaryRecommendations { get; set; }


        [DisplayName("Assessment Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime AssessmentDate { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Appointments? FkAppointments { get; set; }

        public virtual BaReassessments? FkReAssessment { get; set; }

        public virtual ESignatures? FkAnalystSignature { get; set; }

        public virtual ESignatures? FkSupervisorSignature { get; set; }

        public virtual ICollection<AbcReports>? AbcReports { get; set; }

        public virtual ICollection<Authorizations>? Authorizations { get; set; }

        public virtual ICollection<BaCrisisPlan>? BaCrisisPlan { get; set; }

        public virtual ICollection<BaAssessmentsInterventions>? BaAssessmentsInterventions { get; set; }

        public virtual ICollection<BaMonthlyReports>? BaMonthlyReports { get; set; }

        public virtual ICollection<Maladaptives>? Maladaptives { get; set; }

        public virtual ICollection<Replacements>? Replacements { get; set; }
#nullable disable
    }
}
