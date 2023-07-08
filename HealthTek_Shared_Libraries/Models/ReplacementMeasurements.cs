using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class ReplacementMeasurements
    {
        [DisplayName("ID")]
        public int ReplacementMeasurementsId { get; set; }

        [DisplayName("Replacement")]
        public int FkReplacementsId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Date Measured")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime DateMeasured { get; set; }

#nullable enable
        [DisplayName("Successful Trials")]
        public int? SuccessfulTrials { get; set; }

        [DisplayName("Total Trials")]
        public int? TotalTrials { get; set; }

        [DisplayName("Frequency")]
        public int? Frequency { get; set; }

        [DisplayName("Duration")]
        public int? Duration { get; set; }

        [DataType(DataType.Text)]
        //[Required(ErrorMessage = "Please enter Duration Unit.")]
        [DisplayName("Duration Unit")]
        public string? DurationUnit { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Comment")]
        public string? ReplacementMeasureComment { get; set; }
#nullable disable

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Replacements FkReplacements { get; set; }
    }
}
