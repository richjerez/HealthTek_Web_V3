using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class Caregivers
    {
        public Caregivers()
        {
        }

        [Display(Name = "ID")]
        public int CaregiversId { get; set; }

        [Display(Name = "E-Signature")]
        public int? FkEsignaturesId { get; set; }

        [Display(Name = "Client")]
        public int FkClientsId { get; set; }

        [Display(Name = "Location")]
        public int? FkLocationsId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter Name.")]
        [DataType(DataType.Text)]
        public string? FullName { get; set; }

        [Display(Name = "Relationship")]
        [Required(ErrorMessage = "Please enter Relationship.")]
        [DataType(DataType.Text)]
        public string? Relationship { get; set; }

#nullable enable
        [Display(Name = "Gender")]
        [DataType(DataType.Text)]
        public string? Gender { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please enter a valid e-mail address")]
        public string? Email { get; set; }

        [Display(Name = "Baseline Collected On")]
        [DataType(DataType.Date)]
        public DateTime? BaselineCollectionDate { get; set; }

        [Display(Name = "Baseline")]
        public int? Baseline { get; set; }

        [Display(Name = "Comments")]
        [DataType(DataType.Text)]
        public string? Comments { get; set; }
#nullable disable

        [Display(Name = "Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        [Display(Name = "E-Signature")]
        public virtual ESignatures? FkEsignatures { get; set; }

        public virtual Clients? FkClients { get; set; }

        public Locations? Locations { get; set; }
    }
}
