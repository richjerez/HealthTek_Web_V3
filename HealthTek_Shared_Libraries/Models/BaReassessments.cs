using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class BaReassessments
    {
        public BaReassessments()
        {
            Reassessments = new HashSet<BaReassessments>();
        }

        [DisplayName("ID")]
        public int BaReassessmentsId { get; set; }

        //Initial Assessment
        [DisplayName("Initial Assessment")]
        public int FkBaInitialAssessmentsId { get; set; }

        public int FkBaReAssessmentsId { get; set; }

        [DisplayName("Summary")]
        [Required(ErrorMessage = "Please enter Summary.")]
        [DataType(DataType.Text)]
        public string? Summary { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }


        [ForeignKey("FkBaInitialAssessmentsId")]
        public virtual BaAssessments? InitialAssessment { get; set; }
        public virtual BaReassessments? ReAssessment { get; set; }
        public virtual ICollection<BaReassessments>? Reassessments { get; set; }
    }
}
