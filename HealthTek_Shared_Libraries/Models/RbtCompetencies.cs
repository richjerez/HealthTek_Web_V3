using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class RbtCompetencies
    {
        public RbtCompetencies()
        {
        }

        [DisplayName("ID")]
        public int RbtCompetenciesId { get; set; }

        [DisplayName("Supervision")]
        public int FkSupervisionsId { get; set; }

        [DisplayName("Supervisor E-Signature")]
        public int? FkSupervisorSignaturesId { get; set; }

        [DisplayName("Competency Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime CompetencyDate { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public Supervisions? Supervisions { get; set; }

        public virtual ESignatures? FkSupervisorSignatures { get; set; }

        public virtual List<RbtCompTrainings>? RbtCompTrainings { get; set; }
    }
}
