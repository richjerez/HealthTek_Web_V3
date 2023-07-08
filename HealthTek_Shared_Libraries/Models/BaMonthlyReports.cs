using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class BaMonthlyReports
    {
        public BaMonthlyReports()
        {
            EnvironmentalChanges = new HashSet<EnvironmentalChanges>();
        }

        [DisplayName("ID")]
        public int BaMonthlyReportsId { get; set; }

        [DisplayName("BA Assessment")]
        public int FkBaAssessmentsId { get; set; }

        [DisplayName("Appointment")]
        public int FkAppointmentsId { get; set; }

#nullable enable
        [DisplayName("Monthly Summary")]
        //[Required(ErrorMessage = "Please enter Monthly Summary.")]
        [DataType(DataType.Text)]
        public string? MonthlySummary { get; set; }

        [DisplayName("Caregiver Training Statement")]
        //[Required(ErrorMessage = "Please enter Statement.")]
        [DataType(DataType.Text)]
        public string? CaregiverTrainingStatement { get; set; }
#nullable disable

        //[Required(ErrorMessage = "Please enter Date.")]
        [DisplayName("Monthly Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime MonthlyDate { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("FkAppointmentsId")]
        public virtual Appointments? FkAppointments { get; set; }

        [ForeignKey("FkBaAssessmentsId")]
        public virtual BaAssessments? FkBaAssessments { get; set; }

        public virtual ICollection<EnvironmentalChanges>? EnvironmentalChanges { get; set; }
    }
}
