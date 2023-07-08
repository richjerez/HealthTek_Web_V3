using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class BcabaSupvMeetings
    {
        [DisplayName("ID")]
        public int BcabaSupvMeetingsId { get; set; }

        [Display(Name = "Supervisor")]
        public string? FkSupervisorId { get; set; }

        [DisplayName("Supervision")]
        public int FkSupervisionsId { get; set; }

        [DisplayName("BCaBA Signature")]
        public int FkBcabaSignatureId { get; set; }

        [DisplayName("Supervisor Signature")]
        public int FkSupervisorSignatureId { get; set; }

        [Display(Name = "Is BCBA?")]
        public bool IsBcbaCredential { get; set; }

        [Display(Name = "First Thousand?")]
        public bool IsFirstThousand { get; set; }

        [Display(Name = "Was Observed")]
        public bool WasObserved { get; set; }

        [DisplayName("BCaBA Signed")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime BcabaSignDate { get; set; }

        [DisplayName("Supervisor Signed")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime SupvSignDate { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public Supervisions? Supervisions { get; set; }

        public virtual Employees? FkSupervisor { get; set; }

        public virtual ESignatures? FkBcabaSignature { get; set; }

        public virtual ESignatures? FkSupervisorSignature { get; set; }
    }
}
