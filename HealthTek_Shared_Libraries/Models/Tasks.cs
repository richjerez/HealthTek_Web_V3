using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class Tasks
    {
        public Tasks()
        {
            Notes = new HashSet<TaskNotes>();
        }

        [NotMapped]
        public virtual Assignments? Assignment { get; set; }

        [NotMapped]
        public virtual Appointments? Appointment { get; set; }

        [DisplayName("ID")]
        public int TasksId { get; set; }

        [DisplayName("Due Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [DisplayName("Completed Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? CompletedDate { get; set; }

        [DisplayName("Assigned By")]
        public string? FkAssignedById { get; set; }

        [DisplayName("Assigned To")]
        public string? FkAssignedToId { get; set; }

        [DisplayName("Type")]
        [DataType(DataType.Text)]
        public string? TaskType { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Please enter Task Status.")]
        [DataType(DataType.Text)]
        public string? TaskStatus { get; set; }
        [DisplayName("Task Subject")]
        [Required(ErrorMessage = "Please enter Task Status.")]
        [DataType(DataType.Text)]
        public string? TaskSubject { get; set; }

        [DisplayName("Task Description")]
        [Required(ErrorMessage = "Please enter Task Status.")]
        [DataType(DataType.Text)]
        public string? TaskDescription { get; set; }

        [DisplayName("Note")]
        [DataType(DataType.Text)]
        public string? TaskNote { get; set; }

        [DisplayName("Task Identifier")]
        [DataType(DataType.Text)]
        public string? TaskIdentifier { get; set; }

        [DisplayName("Cleared?")]
        public bool IsCleared { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Employees? FkAssignedBy { get; set; }

        public virtual Employees? FkAssignedTo { get; set; }

        public ICollection<TaskNotes>? Notes { get; set; }
    }
}
