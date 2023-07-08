using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class FacilitiesOperatingCounties
    {
        [DisplayName("ID")]
        public int FacilitiesOperatingCountiesId { get; set; }

        [DisplayName("Facility")]
        public int FkFacilitiesId { get; set; }

        [DisplayName("Operating County")]
        public int FkOperatingCountiesId { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual Facilities? FkFacilities { get; set; }

        public virtual OperatingCounties? FkOperatingCounties { get; set; }
    }
}
