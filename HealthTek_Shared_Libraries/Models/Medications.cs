using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class Medications
    {
        public Medications()
        {

        }
        [DisplayName("ID")]
        public int MedicationsId { get; set; }

        [Display(Name = "Client")]
        public int FkClientsId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Medication Name.")]
        [DisplayName("Medication Name")]
        public string? MedicationName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string? Description { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Frequency")]
        public string? Frequency { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Duration")]
        public string? Duration { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Dosage")]
        public string? Dosage { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Prescribing Physician")]
        public string? PrescribingPhysician { get; set; }

        public virtual Clients? FkClients { get; set; }

    }
}
