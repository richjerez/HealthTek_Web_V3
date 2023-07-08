using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class Diagnosis
    {

        [DisplayName("ID")]
        public int DiagnosisId { get; set; }

        [DisplayName("Type")]
        public string? DiagnosisType { get; set; }

        [DisplayName("Client")]
        public int FkClientsId { get; set; }

        [Required(ErrorMessage = "Please enter Diagnosis Code.")]
        [DisplayName("Diagnosis Code")]
        [DataType(DataType.Text)]
        public string? DiagnosisCode { get; set; }

        [Required(ErrorMessage = "Please enter Diagnosis Name.")]
        [DisplayName("Diagnosis Name")]
        [DataType(DataType.Text)]
        public string? DiagnosisName { get; set; }

        [DisplayName("Description")]
        [DataType(DataType.Text)]
        public string? Description { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Clients? FkClients { get; set; }
    }
}
