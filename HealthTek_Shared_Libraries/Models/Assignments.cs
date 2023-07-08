using System;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class Assignments
    {
        public Assignments()
        {
        }

        [Display(Name = "Assignment")]
        public int AssignmentsId { get; set; }

        [Display(Name = "Status")]
        public string? AssignmentStatus { get; set; }

        [Display(Name = "Client")]
        public int FkClientsId { get; set; }

        [Display(Name = "Facility")]
        public int FkFacilitiesId { get; set; }

        [Display(Name = "Employees")]
        public string? FkEmployeesId { get; set; }

        [Display(Name = "Assignment Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime AssignmentEffectiveDate { get; set; }

        [Display(Name = "Assignment Expiration Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? AssignmentExpirationDate { get; set; }

        [Display(Name = "Assignment Position")]
        [DataType(DataType.Text)]
        public string? AssignmentPosition { get; set; }

        [Display(Name = "Needs Attention?")]
        public bool NeedsAttention { get; set; }

        [Display(Name = "Confirmed?")]
        public bool IsConfirmed { get; set; }

        [Display(Name = "Note")]
        [DataType(DataType.Text)]
        public string? AssignmentNote { get; set; }

        [Display(Name = "Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Employees? FkEmployees { get; set; }
        public virtual Facilities? FkFacilities { get; set; }
        public virtual Clients? FkClients { get; set; }
    }
}