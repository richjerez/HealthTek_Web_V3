using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class Interventions
    {
        public Interventions()
        {
            BaAssessmentsInterventions = new HashSet<BaAssessmentsInterventions>();
            BaProgressNotesInterventions = new HashSet<BaProgressNotesInterventions>();
        }

        [DisplayName("ID")]
        public int InterventionsId { get; set; }

        [Required(ErrorMessage = "Please enter Intervention Name.")]
        [DataType(DataType.Text)]
        [DisplayName("Name")]
        public string? InterventionName { get; set; }

#nullable enable
        [DisplayName("Description")]
        [DataType(DataType.Text)]
        public string? InterventionDescription { get; set; }

#nullable disable

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }


        public virtual ICollection<BaAssessmentsInterventions> BaAssessmentsInterventions { get; set; }
        public virtual ICollection<BaProgressNotesInterventions> BaProgressNotesInterventions { get; set; }
    }
}
