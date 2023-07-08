using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class Maladaptives
    {
        public Maladaptives()
        {
            //ClientEvents = new HashSet<ClientEvents>();
            //LongTermObjectives = new List<LongTermObjectives>();
            //ShortTermObjectives = new List<ShortTermObjectives>();
        }

        [DisplayName("ID")]
        public int MaladaptivesId { get; set; }

        [DisplayName("Caregiver Training Goal")]
        public int? FkCaregiverTrainingGoalsId { get; set; }

        [DisplayName("Client")]
        public int FkClientsId { get; set; }

        [DisplayName("Maladaptive Discharge")]
        public int? FkMaladaptiveDischargesId { get; set; }

        [DisplayName("Replacement")]
        public int? FkReplacementsId { get; set; }

        [DisplayName("Assessment")]
        public int FkBaAssessmentsId { get; set; }

        [DisplayName("Baseline Average")]
        public int? BaselineAverage { get; set; }

        [DisplayName("Baseline Week 1")]
        public int? Baseline1 { get; set; }

        [DisplayName("BL Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline1StartDate { get; set; }

        [DisplayName("BL End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline1EndDate { get; set; }

        [DisplayName("Baseline Week 2")]
        public int? Baseline2 { get; set; }

        [DisplayName("BL Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline2StartDate { get; set; }

        [DisplayName("BL End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline2EndDate { get; set; }

        [DisplayName("Baseline Week 3")]
        public int? Baseline3 { get; set; }

        [DisplayName("BL Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline3StartDate { get; set; }

        [DisplayName("BL End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? Baseline3EndDate { get; set; }

        [Required]
        [DisplayName("Maladaptive Name")]
        public string? MaladaptiveName { get; set; }

        [DisplayName("Topography")]
        public string? Topography { get; set; }

        [DisplayName("Archived Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTime? ArchivedDate { get; set; }

        [DisplayName("Intensity")]
        public int? MaladaptiveIntensity { get; set; }

        [DisplayName("Baseline Duration")]
        public int? BaselineDuration { get; set; }

        [DisplayName("Baseline Duration")]
        public string? BaselineDurationUnit { get; set; }

        //[DisplayName("Function")]
        //public string MaladaptiveFunction { get; set; }

        [DisplayName("Prevalent Setting Event")]
        public string? PrevalentSettingEvent { get; set; }

        [DisplayName("Collection Method")]
        public string? CollectionMethod { get; set; }

        [DisplayName("Comment")]
        public string? MaladaptiveComment { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public CaregiverTrainingGoals? FkCaregiverTrainingGoals { get; set; }

        public Replacements? FkReplacements { get; set; }

        public MaladaptiveDischarges? FkMaladaptiveDischarges { get; set; }

        [ForeignKey("FkClientsId")]
        public virtual Clients? FkClients { get; set; }

        [ForeignKey("FkBaAssessmentsId")]
        public virtual BaAssessments? FkBaAssessments { get; set; }

        public virtual MaladaptiveMeasurements? MaladaptiveMeasurements { get; set; }

        public virtual ICollection<Functions>? FunctionsList { get; set; }

        public virtual ICollection<ClientEvents>? ClientEvents { get; set; }

        public virtual List<LongTermObjectives>? LongTermObjectives { get; set; }

        public virtual List<ShortTermObjectives>? ShortTermObjectives { get; set; }
    }
}
