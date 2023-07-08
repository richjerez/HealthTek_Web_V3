using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class EmployeesOperatingCounties
    {
        [DisplayName("ID")]
        public int EmployeesOperatingCountiesId { get; set; }

        [DisplayName("Employee")]
        public string? FkEmployeesId { get; set; }

        [DisplayName("Operating Counties")]
        public int FkOperatingCountiesId { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("FkEmployeesId")]
        public virtual Employees? FkEmployees { get; set; }

        [ForeignKey("FkOperatingCountiesId")]
        public virtual OperatingCounties? FkOperatingCounties { get; set; }
    }
}
