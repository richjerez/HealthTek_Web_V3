using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class Comments
    {
        [DisplayName("ID")]
        public int CommentsId { get; set; }

        [DisplayName("Client")]
        public int? FkClientsId { get; set; }

        [DisplayName("User")]
        public string? FkUserId { get; set; }

        [DisplayName("Batch")]
        public int? FkBatchesId { get; set; }

        [DisplayName("Note Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? NoteDate { get; set; }

        [DisplayName("Note")]
        [Required(ErrorMessage = "Please enter Note")]
        [DataType(DataType.Text)]
        public string? Notes { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Employees? FkEmployees { get; set; }

        public virtual Clients? FkClients { get; set; }

        public virtual Batches? FkBatches { get; set; }
    }
}
