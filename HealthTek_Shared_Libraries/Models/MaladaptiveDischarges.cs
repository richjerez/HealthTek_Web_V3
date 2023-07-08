using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class MaladaptiveDischarges
    {
        [DisplayName("ID")]
        public int MaladaptiveDischargesId { get; set; }

        [Display(Name = "Maladaptive")]
        public int FkMaladaptivesId { get; set; }

        [Display(Name = "Discharge Frequency")]
        public int DischargeFrequency { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Expected Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime? ExpectedDate { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Discharge All STOs")]
        public string? DischargeAllStos { get; set; }

        [DisplayName("Transition from individual intensive treatment to social skills group interventions")]
        public bool IsSocialSkills { get; set; }

        [DisplayName("Refer to a tutoring program, if required")]
        public bool IsTutoringProgram { get; set; }

        [DisplayName("Learning and being reinforced by the natural environment")]
        public bool IsLearningByEnvironment { get; set; }

        [DisplayName("Transition to a less restrictive classroom")]
        public bool IsTransitionClassroom { get; set; }

        [DisplayName("Transition to vocational program/job placement")]
        public bool IsTransitionVocational { get; set; }

        [DisplayName("Transition to Adult Day Training Program")]
        public bool IsTransitionAdtp { get; set; }

        [DisplayName("Transition to college")]
        public bool IsTransitionCollege { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public Maladaptives? FkMaladaptives { get; set; }
    }
}
