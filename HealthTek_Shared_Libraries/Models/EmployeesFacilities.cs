using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class EmployeesFacilities
    {
        public int EmployeesFacilitiesId { get; set; }

        [DisplayName("Facilities")]
        public int FkFacilitiesId { get; set; }
        public string? FkEmployeesId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("FkFacilitiesId")]
        public virtual Facilities? FkFacilities { get; set; }

        [ForeignKey("FkEmployeesId")]
        public virtual Employees? FkEmployees { get; set; }
    }
}
