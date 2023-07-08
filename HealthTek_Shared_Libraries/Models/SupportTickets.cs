using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class SupportTickets
    {
        public SupportTickets()
        {
        }

        public int SupportTicketsId { get; set; }

        [NotMapped]
        public string TicketId
        {
            get
            {
                Guid guid = Guid.NewGuid();
                return guid.ToString().Substring(0, 6);
            }
        }

        [DisplayName("Completed Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? CompletedDate { get; set; }

        [DisplayName("Assigned By")]
        public string? FkAssignedById { get; set; }

        [DisplayName("Ticket Number")]
        [Required(ErrorMessage = "Please enter Ticket #.")]
        [DataType(DataType.Text)]
        public string? TicketNumber { get; set; }

        [DisplayName("Error Occuring Views (Optional)")]
        [DataType(DataType.Text)]
        public string? ViewsInError { get; set; }

        [DisplayName("Description")]
        [DataType(DataType.Text)]
        public string? Description { get; set; }

        [DisplayName("Cleared?")]
        public bool IsCleared { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Employees? FkAssignedBy { get; set; }

    }
}
