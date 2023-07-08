using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace HealthTek_Shared_Libraries
{
    public partial class Batches
    {
        public Batches()
        {
            Appointments = new HashSet<Appointments>();
            Comments = new HashSet<Comments>();
        }

        [DisplayName("ID")]
        public int BatchesId { get; set; }

        [Required(ErrorMessage = "Please enter Batch Date.")]
        [DisplayName("Batch Date")]
        [DataType(DataType.Date)]
        public DateTime BatchDate { get; set; }

        [Required(ErrorMessage = "Please enter Batch #.")]
        [DisplayName("Batch Number")]
        public string? BatchNumber { get; set; }

        [Required(ErrorMessage = "Please enter Total.")]
        [DisplayName("Total")]
        [DataType(DataType.Currency)]
        public decimal? Total { get; set; }

        [DisplayName("Employee ID")]
        public string? FkEmployeesId { get; set; }

        [DisplayName("Facility ID")]
        public int FkFacilitiesId { get; set; }

        [DisplayName("Creation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime? CreationDate { get; set; }

        [DisplayName("Last Update Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime? LastUpdateDate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [DisplayName("Employee")]
        public virtual Employees? FkEmployees { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [DisplayName("Facility")]
        public virtual Facilities? FkFacilities { get; set; }

        public virtual ICollection<Appointments>? Appointments { get; set; }
        public virtual ICollection<Comments>? Comments { get; set; }
    }
}
