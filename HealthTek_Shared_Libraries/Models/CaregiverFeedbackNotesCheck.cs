using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class CaregiverFeedbackNotesCheck
    {
        public CaregiverFeedbackNotesCheck()
        {
        }

        [DisplayName("ID")]
        public int CaregiverFeedbackNotesCheckId { get; set; }

        [DisplayName("BA PN")]
        public int FkBaProgressNotesId { get; set; }

        [DisplayName("Caregiver Feedback")]
        public int FkCaregiverFeedbackId { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual BaProgressNotes? FkBaProgressNotes { get; set; }

        public virtual CaregiverFeedback? FkCaregiverFeedback { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }
    }
}
