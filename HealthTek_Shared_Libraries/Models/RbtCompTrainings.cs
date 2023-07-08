using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class RbtCompTrainings
    {
        [DisplayName("ID")]
        public int RbtCompTrainingsId { get; set; }

        [DisplayName("RCC")]
        public int FkRbtCompetenciesId { get; set; }

        [DisplayName("Comment")]
        [DataType(DataType.Text)]
        public string? Comment { get; set; }

        [DisplayName("Training Item")]
        [DataType(DataType.Text)]
        public string? TrainingItem { get; set; }

        [DisplayName("Baseline")]
        public int Baseline { get; set; }

        [DisplayName("Competency Level")]
        public int CompetencyLevel { get; set; }

        [DisplayName("Previous Level")]
        public int? PreviousLevel { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual RbtCompetencies? FkRbtCompetencies { get; set; }
    }
}
