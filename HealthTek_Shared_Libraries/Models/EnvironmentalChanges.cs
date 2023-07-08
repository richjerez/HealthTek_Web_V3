using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class EnvironmentalChanges
    {

        [DisplayName("ID")]
        public int EnvironmentalChangesId { get; set; }

        [DisplayName("BA Monthly Report")]
        public int? FkBaMonthlyReportsId { get; set; }

        [DisplayName("BA PN")]
        public int? FkBaProgressNotesId { get; set; }

        [DisplayName("Description")]
        [DataType(DataType.Text)]
        public string? Description { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Please enter Category.")]
        [DataType(DataType.Text)]
        public string? Category { get; set; }

        [DisplayName("Add to Graph")]
        public bool AddToGraph { get; set; }

        [DisplayName("Occurred Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateOfOccurrence { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("FkBaProgressNotesId")]
        public virtual BaProgressNotes? FkBaProgressNotes { get; set; }

        [ForeignKey("FkBaMonthlyReportsId")]
        public virtual BaMonthlyReports? FkBaMonthlyReports { get; set; }
    }
}
