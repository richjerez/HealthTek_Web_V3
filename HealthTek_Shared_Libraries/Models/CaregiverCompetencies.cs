using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class CaregiverCompetencies
    {
        public CaregiverCompetencies()
        {
        }

        [DisplayName("ID")]
        public int CaregiverCompetenciesId { get; set; }

        [DisplayName("BA PN")]
        public int FkBaProgressNotesId { get; set; }

        [DisplayName("E-Signature")]
        public int? FkUserSignaturesId { get; set; }

        [Required(ErrorMessage = "Please enter Date.")]
        [DisplayName("Competency Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        [DataType(DataType.Date)]
        public DateTime CompetencyDate { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public BaProgressNotes BaProgressNotes { get; set; }

        public virtual ESignatures FkUserSignatures { get; set; }

        public virtual List<CaregiverCompChecks> CaregiverCompChecks { get; set; }
    }
}
