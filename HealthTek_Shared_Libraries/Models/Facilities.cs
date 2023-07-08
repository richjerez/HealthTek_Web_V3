using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class Facilities
    {
        public Facilities()
        {
            Appointments = new HashSet<Appointments>();
            Assignments = new HashSet<Assignments>();
            Authorizations = new HashSet<Authorizations>();
            Batches = new HashSet<Batches>();
            EmployeesFacilities = new HashSet<EmployeesFacilities>();
            FacilitiesOperatingCounties = new HashSet<FacilitiesOperatingCounties>();
            Intakes = new HashSet<Intakes>();
            ClientsFacilities = new HashSet<ClientsFacilities>();
        }

        [DisplayName("ID")]
        public int FacilitiesId { get; set; }

        #region Facility Details
        [DisplayName("Facility Name")]
        [Required(ErrorMessage = "Please enter Facility Name.")]
        [DataType(DataType.Text)]
        public string? FacilityName { get; set; }

        [DisplayName("Type")]
        [DataType(DataType.Text)]
        public string? FacilityType { get; set; }

        [DisplayName("Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [DisplayName("Alternate Number")]
        [DataType(DataType.PhoneNumber)]
        public string? AlternateNumber { get; set; }

        [DisplayName("Fax Number")]
        [DataType(DataType.PhoneNumber)]
        public string? FaxNumber { get; set; }

        [DisplayName("Date of Arrival")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfArrival { get; set; }

        [DisplayName("Supervisor")]
        [DataType(DataType.Text)]
        public string? Supervisor { get; set; }

        [DisplayName("URL")]
        [DataType(DataType.Url)]
        public string? FacilityUrl { get; set; }

#nullable enable
        [DisplayName("Abbreviation")]
        [DataType(DataType.Text)]
        public string? FacilityInitials { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HHMM}")]
        [DisplayName("Operating Start Time")]
        [DataType(DataType.Time)]
        public string? OperatingStartTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HHMM}")]
        [DisplayName("Operating End Time")]
        [DataType(DataType.Time)]
        public string? OperatingEndTime { get; set; }

        [DisplayName("Timezone")]
        [DataType(DataType.Text)]
        public string? Timezone { get; set; }

        [DisplayName("Client Daily Limit")]
        public int? ClientDailyLimit { get; set; }

        [DisplayName("Employee Daily Limit")]
        public int? EmployeeDailyLimit { get; set; }

        [DisplayName("# of Clients")]
        public int? NumberOfClients { get; set; }
#nullable disable
        #endregion

        #region Foreing Keys Id's

        [DisplayName("Location")]
        public int? FkLocationsId { get; set; }
        #endregion

        #region Object Creation
        [Display(Name = "Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        // Add who modified it fkuserid
        #endregion Object Creation

        #region Foreign Tables
        public Locations Locations { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
        public virtual ICollection<Assignments> Assignments { get; set; }
        public virtual ICollection<Authorizations> Authorizations { get; set; }
        public virtual ICollection<Batches> Batches { get; set; }
        public virtual ICollection<EmployeesFacilities> EmployeesFacilities { get; set; }
        public virtual ICollection<FacilitiesOperatingCounties> FacilitiesOperatingCounties { get; set; }
        public virtual ICollection<Intakes> Intakes { get; set; }
        public virtual ICollection<ClientsFacilities> ClientsFacilities { get; set; }

        #endregion
    }
}
