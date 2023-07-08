using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class ServiceCodes
    {
        public ServiceCodes()
        {
            Appointments = new HashSet<Appointments>();
            Authorizations = new HashSet<Authorizations>();
            Documents = new HashSet<Documents>();
        }

        [DisplayName("ID")]
        public int ServiceCodesId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Service Code Type.")]
        [DisplayName("Type")]
        public string? ServiceCodeType { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter CPT Code.")]
        [DisplayName("CPT Code")]
        public string? CptCode { get; set; }
#nullable enable

        [DataType(DataType.Text)]
        [DisplayName("1st Modifier")]
        public string? ModifierFirst { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("2nd Modifier")]
        public string? ModifierSecond { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Title.")]
        [DisplayName("Title")]
        public string? CodeTitle { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string? CodeDescription { get; set; }

        [DisplayName("Code Rate")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter Rate.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? CodeRate { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Rate Type.")]
        [DisplayName("Rate Type")]
        public string? CodeRateType { get; set; }
#nullable disable
        public string Code
        {
            get
            {
                return CodeTitle + ":   (" + CodeDescription + ")";
            }
        }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
        public virtual ICollection<Authorizations> Authorizations { get; set; }
        public virtual ICollection<Documents> Documents { get; set; }

        public string FullCode
        {
            get
            {
                if (!string.IsNullOrEmpty(ModifierFirst) && !string.IsNullOrEmpty(ModifierSecond))
                    return CptCode + ": " + ModifierFirst + ": " + ModifierSecond + "   (" + CodeTitle + ")";
                else if (!string.IsNullOrEmpty(ModifierFirst))
                    return CptCode + ": " + ModifierFirst + "   (" + CodeTitle + ")";
                else
                    return CptCode + "   (" + CodeTitle + ")";
            }
        }

        public string FullCpt
        {
            get
            {
                if (!string.IsNullOrEmpty(ModifierFirst) && !string.IsNullOrEmpty(ModifierSecond))
                    return CptCode + ": " + ModifierFirst + ": " + ModifierSecond;
                else if (!string.IsNullOrEmpty(ModifierFirst))
                    return CptCode + ": " + ModifierFirst;
                else
                    return CptCode;
            }
        }

    }
}
