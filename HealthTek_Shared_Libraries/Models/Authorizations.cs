using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class Authorizations
    {
        public Authorizations()
        {
            AuthorizationNotes = new HashSet<AuthorizationNotes>();
        }

        [NotMapped]
        public string? Notes { get; set; }

        #region Details
        [Display(Name = "ID")]
        public int AuthorizationsId { get; set; }

        [Required(ErrorMessage = "The Status is required.")]
        [Display(Name = "Status")]
        [DataType(DataType.Text)]
        public string? AuthorizationStatus { get; set; }

        [Required(ErrorMessage = "The Effective Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Effective Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:M/d/yyyy}")]
        public DateTime? EffectiveDate { get; set; }

        [Required(ErrorMessage = "The Expiration Date is required.")]
        [Display(Name = "Expiration Date")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }

        [Required(ErrorMessage = "The Unit Amount is required.")]
        [Display(Name = "Unit Amount")]
        public int? UnitAmount { get; set; }

        [Display(Name = "Units Used")]
        public int? UnitsUsed { get; set; }

        [Required(ErrorMessage = "The Authorization Number is required.")]
        [Display(Name = "PA Number")]
        [DataType(DataType.Text)]
        public string? AuthorizationNumber { get; set; }

        [Display(Name = "Denial Reason (Optional)")]
        [DataType(DataType.Text)]
        public string? DenialReason { get; set; }

#nullable enable
        [Display(Name = "Location (Where services are to be delivered)")]
        [DataType(DataType.Text)]
        public string? AuthLocation { get; set; }
#nullable disable

        [Display(Name = "Weekly Hours")]
        public int? WeeklyHours { get; set; }


        [Display(Name = "Approved?")]
        public bool IsApproved { get; set; }
        #endregion

        #region Methods
        public int? UnitsLeft
        {
            get
            {
                int? unitsleft = UnitAmount - UnitsUsed;
                return unitsleft;
            }
        }
        [Display(Name = "Weekly Units")]
        public int? WeeklyUnits
        {
            get
            {
                int? unitsPerWeek = 0;
                if (UnitAmount > 0 && WeeklyHours > 0)
                {
                    unitsPerWeek = UnitAmount / WeeklyHours;
                }
                return unitsPerWeek;
            }
            set { }
        }

        #endregion

        [Display(Name = "Created")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        #region ForeignKeys
        [Display(Name = "Client")]
        public int FkClientsId { get; set; }

        [Display(Name = "Facility")]
        public int FkFacilitiesId { get; set; }

        [Display(Name = "Assessment")]
        public int? FkBaAssessmentsId { get; set; }

        [Display(Name = "Service Code")]
        public int FkServiceCodesId { get; set; }

        public virtual BaAssessments FkBaAssessments { get; set; }

        public virtual ServiceCodes FkServiceCodes { get; set; }

        public virtual Facilities FkFacilities { get; set; }

        public virtual Clients FkClients { get; set; }

        public virtual ICollection<AuthorizationNotes> AuthorizationNotes { get; set; }
        #endregion
    }

}

