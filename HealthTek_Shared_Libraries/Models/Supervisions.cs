using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class Supervisions
    {
        public Supervisions()
        {

        }

        [DisplayName("ID")]
        public int SupervisionsId { get; set; }

        [DisplayName("RCCC ID")]
        public int? FkRbtCompetenciesId { get; set; }

        [DisplayName("BCaBA Supv Meeting")]
        public int? FkBcabaSupvMeetingsId { get; set; }

        [DisplayName("Appointment")]
        public int FkAppointmentsId { get; set; }

        [DisplayName("User's Signature ID")]
        public int? FkUserSignaturesId { get; set; }

        [DisplayName("Supervisor Signature ID")]
        public int? FkSupervisorSignaturesId { get; set; }

        [DisplayName("Duration")]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal SupervisionDuration { get; set; }

        [Display(Name = "Observed with Client")]
        public bool ObservedWithClient { get; set; }

        [Display(Name = "Group Session")]
        public bool IsGroup { get; set; }

        [DisplayName("Performance Rating")]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal PerformanceRating { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Supervisor Name.")]
        [DisplayName("Supervisor Name")]
        public string? SupervisorName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Supervisor Number.")]
        [DisplayName("Supervisor Number")]
        public string? SupervisorNumber { get; set; }

        [DisplayName("Create RBT Competency Checks Form")]
        public bool HasRcc { get; set; }

        [DisplayName("Create Bcaba Supervision Meeting Form")]
        public bool HasBcabaSupvMeeting { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Supervision Mode.")]
        [DisplayName("Supervision Mode")]
        public string? SupervisionMode { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Supervision Status")]
        public string? SupervisionStatus { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Supervision Characteristic.")]
        [DisplayName("Supervision Characteristics")]
        public string? SupervisionCharacteristics { get; set; }

#nullable enable

        [DisplayName("Time In")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Please enter Start Time.")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:HH:mm}")]
        public DateTime? StartTime { get; set; }

        [DisplayName("Time Out")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Please enter End Time.")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:HH:mm}")]
        public DateTime? EndTime { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Supervision Comment")]
        public string? SupervisionComment { get; set; }

#nullable disable

        public BcabaSupvMeetings? FkBcabaSupvMeetings { get; set; }

        public RbtCompetencies? FkRbtCompetencies { get; set; }

        public virtual Appointments? FkAppointments { get; set; }

        public virtual ESignatures? FkUserSignature { get; set; }

        public virtual ESignatures? FkSupervisorSignature { get; set; }
    }
}
