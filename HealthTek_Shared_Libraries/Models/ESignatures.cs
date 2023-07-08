using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class ESignatures
    {
        public ESignatures()
        {
            BaAssessmentFkAnalystSignature = new HashSet<BaAssessments>();
            BaAssessmentFkSupervisorSignature = new HashSet<BaAssessments>();
            BaMonthlyEmployeeSignature = new HashSet<BaMonthlyReports>();
            BaPnFkEmployeeSignature = new HashSet<BaProgressNotes>();
            BaPnFkSupervisorSignature = new HashSet<BaProgressNotes>();
            CaregiverCompetencies = new HashSet<CaregiverCompetencies>();
            Caregivers = new HashSet<Caregivers>();
            CfarsFkEmployeeSignature = new HashSet<Cfars>();
            Clients = new HashSet<Clients>();
            RbtCompetencies = new HashSet<RbtCompetencies>();
            SupvUserSignatures = new HashSet<Supervisions>();
            SupvSupervisorSignatures = new HashSet<Supervisions>();
        }

        [DisplayName("ID")]
        public int ESignaturesId { get; set; }

        [DisplayName("Employee ID")]
        public string? FkEmployeesId { get; set; }

        [DisplayName("E-Signature IP")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")]
        [DataType(DataType.Text)]
        public string? ESignsIp { get; set; }

        [DisplayName("E-Signature URL")]
        [DataType(DataType.Url)]
        public string? ESignatureUrl { get; set; }

        [DisplayName("Authorized?")]
        public bool IsAuthorized { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public Employees? FkEmployees { get; set; }

        public virtual ICollection<Clients>? Clients { get; set; }
        public virtual ICollection<Caregivers>? Caregivers { get; set; }
        public virtual ICollection<CaregiverCompetencies>? CaregiverCompetencies { get; set; }
        public virtual ICollection<RbtCompetencies>? RbtCompetencies { get; set; }
        public virtual ICollection<BaAssessments>? BaAssessmentFkAnalystSignature { get; set; }
        public virtual ICollection<BaAssessments>? BaAssessmentFkSupervisorSignature { get; set; }
        public virtual ICollection<BaMonthlyReports>? BaMonthlyEmployeeSignature { get; set; }
        public virtual ICollection<BaProgressNotes>? BaPnFkEmployeeSignature { get; set; }
        public virtual ICollection<BaProgressNotes>? BaPnFkSupervisorSignature { get; set; }
        public virtual ICollection<Cfars>? CfarsFkEmployeeSignature { get; set; }
        public virtual ICollection<Supervisions>? SupvUserSignatures { get; set; }
        public virtual ICollection<Supervisions>? SupvSupervisorSignatures { get; set; }
    }
}
