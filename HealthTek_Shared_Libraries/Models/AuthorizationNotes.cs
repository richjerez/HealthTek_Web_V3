using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class AuthorizationNotes
    {
        [NotMapped]
        public string? EmployeeName { get; set; }

        [Display(Name = "ID")]
        public int AuthorizationNotesId { get; set; }

        [Display(Name = "Employee")]
        public string? FkEmployeesId { get; set; }

        [Display(Name = "Authorization")]
        public int FkAuthorizationsId { get; set; }

        [Display(Name = "Authorization Note Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? NoteDate { get; set; }

        [Display(Name = "Authorization Note")]
        [Required(ErrorMessage = "Please enter Note.")]
        [DataType(DataType.Text)]
        public string? Notes { get; set; }

        [Display(Name = "Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Authorizations? FkAuthorizations { get; set; }

        public virtual Employees? FkEmployees { get; set; }
    }
}
