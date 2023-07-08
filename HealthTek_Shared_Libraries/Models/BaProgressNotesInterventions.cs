using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class BaProgressNotesInterventions
    {
        [DisplayName("ID")]
        public int BaProgressNotesInterventionsId { get; set; }

        [DisplayName("Maladaptive")]
        public int FkBaProgressNotesId { get; set; }

        [DisplayName("Intervention")]
        public int FkInterventionsId { get; set; }

        [DisplayName("Comment")]
        [DataType(DataType.Text)]
        public string? InterventionComment { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual BaProgressNotes FkBaProgressNotes { get; set; }

        public virtual Interventions FkInterventions { get; set; }
    }
}
