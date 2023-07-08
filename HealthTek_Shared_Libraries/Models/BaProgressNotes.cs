using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class BaProgressNotes
    {
        public BaProgressNotes()
        {
            BaProgressNotesInterventions = new HashSet<BaProgressNotesInterventions>();
            CaregiverFeedbackNotesCheck = new HashSet<CaregiverFeedbackNotesCheck>();
            EnvironmentalChanges = new HashSet<EnvironmentalChanges>();
        }
        [NotMapped]
        public List<int>? Reinforcers { get; set; }

        [NotMapped]
        [DisplayName("Feedback Provided to Caregiver")]
        public List<int>? CaregiverFeedback { get; set; }

        [DisplayName("ID")]
        public int? BaProgressNotesId { get; set; }

        [DisplayName("Appt ID")]
        public int? FkAppointmentsId { get; set; }

        [DisplayName("Employee E-Sign ID")]
        public int? FkEmployeeSignatureId { get; set; }

        [DisplayName("Supervisor E-Sign ID")]
        public int? FkSupervisorSignatureId { get; set; }

        [DisplayName("CCC ID")]
        public bool HasCcc { get; set; }

        [DisplayName("Was the Analyst present? (If not Leave blank)")]
        public bool AnalystPresent { get; set; }

        [DisplayName("Crisis Involved")]
        public bool IsRiskCrisisInvolved { get; set; }

        [DisplayName("Monitored")]
        public bool IsRiskMonitored { get; set; }

        [DisplayName("Addressed")]
        public bool IsRiskAddressed { get; set; }

        [DisplayName("Risk Behavior?")]
        public bool IsRiskBehavior { get; set; }

        [DisplayName("Feedback Provided to Caregiver")]
        public bool IsProvidedToCaregiver { get; set; }

        [DisplayName("Client Participation")]
        //[Required(ErrorMessage = "Please enter Client Participation.")]
        [DataType(DataType.Text)]
        public string? ClientParticipation { get; set; }

        [DisplayName("Risk Behavior")]
        //[Required(ErrorMessage = "Please enter Risk Behavior Monitored.")]
        [DataType(DataType.Text)]
        public string? RiskBehaviorMonitored { get; set; }

        [DisplayName("Incident Report")]
        //[Required(ErrorMessage = "Please enter Incident Report.")]
        [DataType(DataType.Text)]
        public string? IncidentReport { get; set; }

        [DataType(DataType.Text)]
        public string? ReinforcerListIds { get; set; }

        [DisplayName("Reinforcer Comments")]
        [DataType(DataType.Text)]
        public string? ReinforcerComments { get; set; }

        [DisplayName("Summmary")]
        //[Required(ErrorMessage = "Please enter Summary.")]
        [DataType(DataType.Text)]
        public string? ProgressNoteSummary { get; set; }

        [DisplayName("PN Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? ProgressNoteDate { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime? CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime? LastUpdateDate { get; set; }

        public CaregiverCompetencies? CaregiverCompetencies { get; set; }

        public virtual Appointments? FkAppointments { get; set; }

        public virtual ESignatures? FkEmployeeSignature { get; set; }

        public virtual ESignatures? FkSupervisorSignature { get; set; }

        public virtual ICollection<BaProgressNotesInterventions>? BaProgressNotesInterventions { get; set; }

        public virtual ICollection<CaregiverFeedbackNotesCheck>? CaregiverFeedbackNotesCheck { get; set; }
        public virtual ICollection<EnvironmentalChanges>? EnvironmentalChanges { get; set; }
    }
}
