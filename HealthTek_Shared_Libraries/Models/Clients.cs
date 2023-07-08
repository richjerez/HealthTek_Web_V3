using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class Clients
    {
        public Clients()
        {
            Appointments = new HashSet<Appointments>();
            Assignments = new HashSet<Assignments>();
            Authorizations = new HashSet<Authorizations>();
            Maladaptives = new HashSet<Maladaptives>();
            Medications = new HashSet<Medications>();
            CaregiverCompChecks = new HashSet<CaregiverCompChecks>();
            ClientContacts = new HashSet<ClientContacts>();
            Diagnosis = new HashSet<Diagnosis>();
            Documents = new HashSet<Documents>();
            Intakes = new HashSet<Intakes>();
            Locations = new HashSet<Locations>();
            Comments = new HashSet<Comments>();
            ClientsFacilities = new HashSet<ClientsFacilities>();
            ClientInsurances = new HashSet<ClientInsurances>();
            Preferences = new HashSet<Preferences>();
        }

        [NotMapped]
        [DisplayName("Languages")]
        public List<string>? ListLanguages { get; set; }

        [NotMapped]
        public IFormFile? customFile { get; set; }

        [DisplayName("Client ID")]
        public int ClientsId { get; set; }

        [Display(Name = "E-Signature")]
        public int? FkEsignaturesId { get; set; }

        [DisplayName("Status")]
        [DataType(DataType.Text)]
        public string? ClientStatus { get; set; }

        #region Identity Details
        [Required(ErrorMessage = "Please enter First Name.")]
        [DisplayName("First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        [DataType(DataType.Text)]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name.")]
        [DisplayName("Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [DisplayName("Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }

        [DisplayName("SSN")]
        [RegularExpression(@"^\d{3}-\d{2}-\d{4}$", ErrorMessage = "###-##-####")]
        [DataType(DataType.Text)]
        public string? Ssn { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter a valid e-mail address")]
        public string? Email { get; set; }

        [DisplayName("Main Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "###-###-####")]
        public string? MainPhoneNumber { get; set; }

        [DisplayName("Alt Contact Info")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "###-###-####")]
        public string? AlternateContactInfo { get; set; }

        [DisplayName("Profile Image")]
        public string? AvatarUrl { get; set; }

        [DisplayName("School Level")]
        [DataType(DataType.Text)]
        public string? SchoolLevel { get; set; }

        [DisplayName("School Name")]
        [DataType(DataType.Text)]
        public string? SchoolName { get; set; }

        [DisplayName("School Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "###-###-####")]
        public string? SchoolPhone { get; set; }

        [DisplayName("Gender")]
        [DataType(DataType.Text)]
        public string? Gender { get; set; }

        [DisplayName("Religion")]
        [DataType(DataType.Text)]
        public string? Religion { get; set; }

        [DisplayName("Ethnicity")]
        [DataType(DataType.Text)]
        public string? Ethnicity { get; set; }

        [DisplayName("Languages")]
        [DataType(DataType.Text)]
        public string? Languages { get; set; }

        #endregion

        #region Refferal Info 
        [DisplayName("Originator")]
        [DataType(DataType.Text)]
        public string? Originator { get; set; }

        [DisplayName("Originator Contact Info")]
        [DataType(DataType.Text)]
        public string? OriginatorContactInfo { get; set; }

        [DisplayName("Referring Physician")]
        [DataType(DataType.Text)]
        public string? ReferringPhysician { get; set; }

        [DisplayName("Referring NPI #")]
        [DataType(DataType.Text)]
        public string? ReferringNpi { get; set; }

        [DisplayName("Referral Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? ReferralDate { get; set; }

        [DisplayName("ABA Services?")]
        public bool NeedAbaServices { get; set; }

        [DisplayName("Clinical Services?")]
        public bool NeedClinicalServices { get; set; }

        [DisplayName("Admit Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? AdmitDate { get; set; }

        #endregion

        #region Object Creation
        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }
        // Add who updated it 
        #endregion

        #region Class Methods
        [DisplayName("Full Name")]
        public string FullName
        {
            get
            {
                var Middle = " ";
                if (MiddleName != null && MiddleName != string.Empty)
                {
                    Middle = " " + MiddleName.Substring(0, 1) + ". ";
                }
                return (FirstName + Middle + LastName);
            }
        }
        public string DocumentIdentifier
        {
            get
            {
                return (ClientsId + FirstName + LastName);
            }
        }

        public int Age
        {
            get
            {
                int age = 0;
                if (Dob != null)
                {
                    var a = Math.Floor(DateTime.Now.Date.Subtract(Dob.Value).TotalDays / 365);
                    age = Convert.ToInt32(a);
                    return (age);
                }
                return age;
            }
        }

        #endregion

        #region Foreign Tables
        public virtual ESignatures? FkEsignatures { get; set; }
        public virtual ICollection<Appointments> Appointments { get; set; }
        public virtual ICollection<Assignments> Assignments { get; set; }
        public virtual ICollection<Authorizations> Authorizations { get; set; }
        public virtual ICollection<Maladaptives> Maladaptives { get; set; }
        public virtual ICollection<Medications> Medications { get; set; }
        public virtual ICollection<CaregiverCompChecks> CaregiverCompChecks { get; set; }
        public Caregivers? Caregivers { get; set; }
        public virtual ICollection<ClientContacts> ClientContacts { get; set; }
        public virtual ICollection<Diagnosis> Diagnosis { get; set; }
        public virtual ICollection<Documents> Documents { get; set; }
        public virtual ICollection<Intakes> Intakes { get; set; }
        [JsonIgnore]
        public virtual ICollection<Locations> Locations { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<ClientsFacilities> ClientsFacilities { get; set; }
        public virtual ICollection<ClientInsurances> ClientInsurances { get; set; }
        public virtual ICollection<Preferences> Preferences { get; set; }

        #endregion
    }
}
