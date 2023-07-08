using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class TaskNotes
    {
        [DisplayName("ID")]
        public int TaskNotesId { get; set; }

        [DisplayName("Task")]
        public int FkTasksId { get; set; }

        public string FkEmployeesId { get; set; }

        [DisplayName("Assignment")]
        public int? FkAssignmentsId { get; set; }

        [DisplayName("Task Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTime TaskDate { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Note.")]
        [DisplayName("Note")]
        public string? Notes { get; set; }

        [Display(Name = "Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Employees FkEmployees { get; set; }
        public virtual Assignments FkAssignments { get; set; }

        public virtual Tasks FkTasks { get; set; }
    }
}
