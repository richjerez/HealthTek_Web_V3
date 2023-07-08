using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class MaladaptiveMeasurements
    {
        [DisplayName("ID")]
        public int MaladaptiveMeasurementsId { get; set; }

        [DisplayName("Maladaptive")]
        public int FkMaladaptivesId { get; set; }

        public int FkCaregiverCompetenciesId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Date Measured")]
        //[Required(ErrorMessage = "Please enter Date Measured.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime? DateMeasured { get; set; }

        [DisplayName("Successful Trials")]
        public int? SuccessfulTrials { get; set; }

        [DisplayName("Total Trials")]
        public int? TotalTrials { get; set; }

        [DisplayName("Frequency")]
        public int? Frequency { get; set; }

        [DisplayName("Duration")]
        public int? Duration { get; set; }

        [DataType(DataType.Text)]
        //[Required(ErrorMessage = "Please enter Duration Measured.")]
        [DisplayName("Duration Unit")]
        public string? DurationUnit { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Comment")]
        public string? MaladaptiveMeasureComment { get; set; }

        [DataType(DataType.Text)]
        public string? InterventionsUsed { get; set; }

        [NotMapped]
        [DisplayName("Interventions")]
        public List<string>? InterventionsUsedList { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Maladaptives? FkMaladaptives { get; set; }
        public virtual CaregiverCompetencies? FkCaregiverCompetencies { get; set; }
    }
}
