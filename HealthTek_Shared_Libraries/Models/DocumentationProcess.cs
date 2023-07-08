using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class DocumentationProcess
    {
        public DocumentationProcess() { }

        [DisplayName("ID")]
        public int DocumentationProcessId { get; set; }

        [DisplayName("Role")]
        public string? Role { get; set; }

        #region Fk id's
        [DisplayName("Employee")]
        public string? FkEmployeesId { get; set; }

        [DisplayName("Uploaded By")]
        public string? FkUploadedById { get; set; }
        public int FkRoleDocsCatalogId { get; set; }
        public int? FkDocumentsId { get; set; }
        #endregion

        #region Object Creation
        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("FkUploadedById")]
        public virtual Employees? FkUploadedBy { get; set; }
        #endregion

        [ForeignKey("FkDocumentsId")]
        public virtual Documents? FkDocuments { get; set; }
        [ForeignKey("FkEmployeesId")]
        public virtual Employees? FkEmployees { get; set; }
        [ForeignKey("FkRoleDocsCatalogId")]
        public virtual RoleDocsCatalog? RoleDocsCatalogs { get; set; }

    }
}
