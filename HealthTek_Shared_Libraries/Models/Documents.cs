using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class Documents
    {
        [DisplayName("ID")]
        public int DocumentsId { get; set; }

        #region NotMapped
        [Required]
        [NotMapped]
        [DisplayName("Document")]
        public ICollection<IFormFile>? customFiles { get; set; }
        #endregion

        #region Document Details
        [DisplayName("Status")]
        [DataType(DataType.Text)]
        public string? DocumentStatus { get; set; }

        [DisplayName("Type")]
        public string? DocumentType { get; set; }

        [DisplayName("Document Title")]
        //[Required(ErrorMessage = "Please enter Title.")]
        [DataType(DataType.Text)]
        public string? DocumentTitle { get; set; }

        [DisplayName("Document Description")]
        //[Required(ErrorMessage = "Please enter Description.")]
        [DataType(DataType.Text)]
        public string? DocumentDescription { get; set; }

        [DisplayName("Document Identifier")]
        //[Required(ErrorMessage = "Please enter Identifier.")]
        [DataType(DataType.Text)]
        public string? DocumentIdentifier { get; set; }

        [DisplayName("Document URL")]
        [DataType(DataType.Url)]
        public string? DocumentUrl { get; set; }

        [DisplayName("Template URL")]
        [DataType(DataType.Url)]
        public string? TemplateUrl { get; set; }

        [DisplayName("Sorted?")]
        public bool IsSorted { get; set; }

        [DisplayName("IsRequired?")]
        public bool IsRequired { get; set; }

        [DisplayName("Is Attached")]
        public bool IsAttached { get; set; }

        [DisplayName("Document Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? DocumentEffectiveDate { get; set; }

        [DisplayName("Document Expiration Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? DocumentExpirationDate { get; set; }

        #endregion

        #region Fk id's
        [DisplayName("Status")]
        public int? FkIntakesId { get; set; }

        [DisplayName("Employee")]
        public string? FkEmployeesId { get; set; }

        [DisplayName("Uploaded By")]
        public string? FkUploadedById { get; set; }

        [DisplayName("Client")]
        public int? FkClientsId { get; set; }
        #endregion

        #region Object Creation
        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Employees? FkUploadedBy { get; set; }

        #endregion

        [JsonIgnore]
        public virtual Intakes? FkIntakes { get; set; }

        public virtual Employees? FkEmployees { get; set; }

        public virtual Clients? FkClients { get; set; }

    }
}
