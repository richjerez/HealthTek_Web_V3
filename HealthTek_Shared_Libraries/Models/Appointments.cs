using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class Appointments
    {

        [Display(Name = "ID")]
        public int AppointmentsId { get; set; }

        [Display(Name = "Service Code")]
        public int? FkServiceCodesId { get; set; }

        [Display(Name = "Batch")]
        public int? FkBatchesId { get; set; }

        [Display(Name = "Ba Assessments")]
        public int? FkBaAssessmentsId { get; set; }

        [Display(Name = "Ba ReAssessments")]
        public int? FkBaReAssessmentsId { get; set; }

        [Display(Name = "Ba Monthly Reports")]
        public int? FkBaMonthlyReportsId { get; set; }

        [Display(Name = "Ba Progress Notes")]
        public int? FkBaProgressNotesId { get; set; }

        [Display(Name = "Ba Progress Notes")]
        public int? FkCfarsId { get; set; }

        [Display(Name = "Billing Status")]
        [DataType(DataType.Text)]
        public string? BillingStatus { get; set; }

        [Display(Name = "QA Status")]
        [DataType(DataType.Text)]
        public string? QaStatus { get; set; }

        [Display(Name = "Client")]
        public int? FkClientsId { get; set; }

        [Display(Name = "Employees")]
        public string? FkEmployeesId { get; set; }

        [Required]
        [Display(Name = "Facility")]
        public int FkFacilitiesId { get; set; }

        [Display(Name = "Start Location")]
        public int? FkStartLocationId { get; set; }

        [Display(Name = "End Location")]
        public int? FkEndLocationId { get; set; }

        [Required]
        [Display(Name = "Type")]
        [DataType(DataType.Text)]
        public string? AppointmentType { get; set; }

        [Display(Name = "Billed Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime BilledDate { get; set; }

        [Display(Name = "Paid by Insurance Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime PaidByInsuranceDate { get; set; }

        [Display(Name = "Paid to Employee")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime PaidToEmployeeDate { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Display(Name = "End Time")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime? EndTime { get; set; }

        [Display(Name = "Last Follow Up")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        [DataType(DataType.DateTime)]
        public DateTime? LastFollowUp { get; set; }

        [Display(Name = "Confirmed?")]
        public bool IsConfirmed { get; set; }

        [Display(Name = "Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm tt}")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm tt}")]
        public DateTime LastUpdateDate { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string? Description { get; set; }

        public string? ClassName { get; set; }

        public int? Units
        {
            get
            {
                if (EndTime != null && StartTime != null)
                {
                    TimeSpan t = (DateTime)EndTime - (DateTime)StartTime;
                    var min = t.TotalMinutes;
                    var reminder = min % 15;
                    var total = (int)min / 15;
                    if (reminder >= 8)
                    {
                        total += 1;
                    }
                    return total;
                }
                return null;
            }
        }

        public int? BillingUnits
        {
            get
            {
                if (EndTime != null && StartTime != null)
                {
                    TimeSpan t = (DateTime)EndTime - (DateTime)StartTime;
                    var min = t.TotalMinutes;
                    var reminder = min % 5.5;
                    var total = (int)min / 5;
                    if (reminder >= 8)
                    {
                        total += 1;
                    }
                    return total;
                }
                return null;
            }
        }

        public string? TimeSlot
        {
            get
            {
                var timeSlot = "";
                if (EndTime != null && StartTime != null)
                {
                    var start = StartTime.ToString();
                    var end = EndTime.ToString();

                    timeSlot = start + " - " + end;

                    return timeSlot;
                }
                return timeSlot;
            }
        }

        public string[] Styles = new[] { "primary", "info", "warning", "danger", "success" };

        public virtual ServiceCodes? FkServiceCodes { get; set; }

        public virtual Batches? FkBatches { get; set; }

        public virtual Employees? FkEmployees { get; set; }

        public virtual Facilities? FkFacilities { get; set; }

        public virtual Locations? FkStartLocation { get; set; }

        public virtual Locations? FkEndLocation { get; set; }

        public virtual Clients? FkClients { get; set; }

        [ForeignKey("FkBaReAssessmentsId")]
        public virtual BaReassessments? BaReAssessments { get; set; }

        public virtual BaAssessments? BaAssessments { get; set; }

        public virtual BaMonthlyReports? BaMonthlyReports { get; set; }

        public virtual BaProgressNotes? BaProgressNotes { get; set; }

        public virtual Cfars? Cfars { get; set; }

        public virtual Supervisions? Supervisions { get; set; }
    }
    public partial class CalendarModel
    {
        public int id { get; set; }
        public string? title { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public bool allDay { get; set; }
        public string? url { get; set; }
        public string? className { get; set; }

    }

}
