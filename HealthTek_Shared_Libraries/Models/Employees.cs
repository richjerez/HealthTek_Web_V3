using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class Employees
    {
        public Employees()
        {
            Appointments = new HashSet<Appointments>();
            Assignments = new HashSet<Assignments>();
            AuthorizationNotes = new HashSet<AuthorizationNotes>();
            Batches = new HashSet<Batches>();
            DocumentationProcess = new HashSet<DocumentationProcess>();
            DocumentsUploadedBy = new HashSet<Documents>();
            EmployeesOperatingCounties = new HashSet<EmployeesOperatingCounties>();
            EmployeesFacilities = new HashSet<EmployeesFacilities>();
            EmployeesRoleNames = new HashSet<EmployeesRoleNames>();
            Comments = new HashSet<Comments>();
            TasksFkAssignedBy = new HashSet<Tasks>();
            TasksFkAssignedTo = new HashSet<Tasks>();
        }
        [NotMapped]
        public IFormFile? CustomFile { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        public string? EmployeesId { get; set; }

        #region VariablesNotMapped
        [NotMapped]
        public List<int>? OpCounties { get; set; }

        [NotMapped]
        public List<int>? Roles { get; set; }

        [NotMapped]
        public List<int>? Facilities { get; set; }

        [NotMapped]
        public string? Avatar { get; set; }

        [NotMapped]
        [DisplayName("Supervisor Number")]
        public string? SupervisorNumber { get; set; }

        [NotMapped]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SupervisorRate { get; set; }

        [NotMapped]
        [DisplayName("Languages")]
        public List<string>? ListLanguages { get; set; }
        #endregion #region VariablesNotMapped

        #region Identity Details
        [Display(Name = "Employee First Name")]
        [Required(ErrorMessage = "Please enter First Name.")]
        [DataType(DataType.Text)]
        public string? FirstName { get; set; }

        [Display(Name = "Employee Middle Name")]
        [DataType(DataType.Text)]
        public string? MiddleName { get; set; }

        [Display(Name = "Employee Last Name")]
        [Required(ErrorMessage = "Please enter Last Name.")]
        [DataType(DataType.Text)]
        public string? LastName { get; set; }

        [Display(Name = "Main Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "###-###-####")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Alternate Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "###-###-####")]
        public string? AlternateNumber { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter a valid e-mail address")]
        public string? Email { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "DOB")]
        public DateTime? Dob { get; set; }

        [Display(Name = "SSN")]
        [RegularExpression(@"^\d{3}-\d{2}-\d{4}$", ErrorMessage = "###-##-####")]
        public string? Ssn { get; set; }

        [DisplayName("Profile Image")]
        public string? AvatarUrl { get; set; }

        [Display(Name = "Gender")]
        public string? Gender { get; set; }

        [Display(Name = "Ethnicity")]
        public string? Ethnicity { get; set; }

        [Display(Name = "Religion")]
        public string? Religion { get; set; }

        [DisplayName("Languages")]
        public string? Languages { get; set; }
        #endregion Identity Details

        #region Employee Details
        [Display(Name = "Company Name")]
        [DataType(DataType.Text)]
        public string? CompanyName { get; set; }

        [Display(Name = "Provider Number")]
        [DataType(DataType.Text)]
        public string? ProviderNumber { get; set; }

        [Display(Name = "NPI #")]
        [DataType(DataType.Text)]
        public string? EmployeeIdentifier { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [Display(Name = "NPI Effective Date")]
        public DateTime? IdentifierEffectiveDate { get; set; }

        [Display(Name = "License #")]
        [DataType(DataType.Text)]
        public string? LicenseNumber { get; set; }

        [Display(Name = "Highest Education")]
        [DataType(DataType.Text)]
        public string? HighestEducation { get; set; }

        [Display(Name = "Referral Source")]
        [DataType(DataType.Text)]
        public string? ReferralSource { get; set; }

        [Display(Name = "Supervisor?")]
        public bool IsSupervisor { get; set; }

        [Display(Name = "HR Ready")]
        public bool IsHrReady { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [Display(Name = "HR Ready Since")]
        public DateTime? HrReadySince { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [Display(Name = "Hire Date")]
        public DateTime? HireDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [Display(Name = "Interviewed On")]
        public DateTime? InterviewDate { get; set; }

        [Display(Name = "Pay Rate")]
        [Column(TypeName = "decimal(18, 2)")]
        [DefaultValue(0.0)]
        public decimal? EmployeeRate { get; set; }

        [Display(Name = "Employee Status")]
        [DataType(DataType.Text)]
        public string? EmployeeStatus { get; set; }
        #endregion Employee Details

        #region Object Creation
        [Display(Name = "Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        // Add who modified it fkuserid
        #endregion Object Creation

        #region Class Methods
        public string EmployeeLabel
        {
            get
            {
                var firstInitial = FirstName.Substring(0, 1) + ". ";
                var Middle = " ";
                if (MiddleName != null && MiddleName != string.Empty)
                {
                    Middle = " " + MiddleName.Substring(0, 1) + ": ";
                }
                return LastName + ", " + firstInitial + EmployeeIdentifier;
            }
        }

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

        #endregion Class Methods

        #region Foreing Keys Id's
        [Display(Name = "E-Signature")]
        public int? FkEsignaturesId { get; set; }

        [Display(Name = "Location")]
        public int? FkLocationsId { get; set; }

        #endregion Foreing Keys Id's

        #region Foreing Tables
        [JsonIgnore]
        public ESignatures? FkESignatures { get; set; }

        public Locations? Locations { get; set; }

        [JsonIgnore]
        public virtual ICollection<Assignments>? Assignments { get; set; }
        public virtual ICollection<Appointments>? Appointments { get; set; }
        public virtual ICollection<AuthorizationNotes>? AuthorizationNotes { get; set; }
        public virtual ICollection<Batches>? Batches { get; set; }
        public virtual ICollection<DocumentationProcess>? DocumentationProcess { get; set; }
        public virtual ICollection<Documents>? DocumentsUploadedBy { get; set; }
        public virtual ICollection<EmployeesFacilities>? EmployeesFacilities { get; set; }
        public virtual ICollection<EmployeesOperatingCounties>? EmployeesOperatingCounties { get; set; }
        public virtual ICollection<EmployeesRoleNames>? EmployeesRoleNames { get; set; }
        public virtual ICollection<Comments>? Comments { get; set; }
        public virtual ICollection<Tasks>? TasksFkAssignedBy { get; set; }
        public virtual ICollection<Tasks>? TasksFkAssignedTo { get; set; }
        #endregion 

        // These dates should be the hiredate this fields are redundant
        [NotMapped]
        [DisplayName("Supervised From")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? SupervisionStartDate { get; set; }

        [NotMapped]
        [DisplayName("Supervised Until")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? SupervisionEndDate { get; set; }
    }
}
