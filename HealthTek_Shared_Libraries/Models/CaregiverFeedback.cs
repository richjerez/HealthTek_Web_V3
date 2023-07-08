using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class CaregiverFeedback
    {
        public CaregiverFeedback()
        {
            CaregiverFeedbackNotesCheck = new HashSet<CaregiverFeedbackNotesCheck>();
        }

        [DisplayName("ID")]
        public int CaregiverFeedbackId { get; set; }

        [Required(ErrorMessage = "Please enter Feedback.")]
        [DisplayName("Feedback")]
        [DataType(DataType.Text)]
        public string? Feedback { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual ICollection<CaregiverFeedbackNotesCheck>? CaregiverFeedbackNotesCheck { get; set; }
    }
}
