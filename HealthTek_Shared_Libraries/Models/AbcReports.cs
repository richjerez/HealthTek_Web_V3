using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class AbcReports
    {
        public AbcReports()
        {
        }
        [NotMapped]
        public string? referer { get; set; }

        [DisplayName("ID")]
        public int AbcReportsId { get; set; }

        [DisplayName("BA Assessment")]
        public int FkBaAssessmentsId { get; set; }

        [DisplayName("Antecedent")]
        [Required(ErrorMessage = "Please enter Antecedent.")]
        [DataType(DataType.Text)]
        public string? Antecedent { get; set; }

        [DisplayName("Behavior")]
        [Required(ErrorMessage = "Please enter Behavior.")]
        [DataType(DataType.Text)]
        public string? Behavior { get; set; }

        [DisplayName("Consequence")]
        [Required(ErrorMessage = "Please enter Consequence.")]
        [DataType(DataType.Text)]
        public string? Consequence { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual BaAssessments? FkBaAssessments { get; set; }

    }
}
