using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public partial class EmployeesRoleNames
    {
        [DisplayName("ID")]
        public int EmployeesRoleNamesId { get; set; }

        [DisplayName("Employee ID")]
        public string? FkEmployeesId { get; set; }

        [DisplayName("Roles")]
        public int FkRoleNamesId { get; set; }

        [DisplayName("Supervisor Number")]
        [DataType(DataType.Text)]
        public string? SupervisorNumber { get; set; }

        [DisplayName("Wants Assignment")]
        public bool WantsAssignment { get; set; }

        [Display(Name = "Pay Rate")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SupervisorRate { get; set; }

        [DisplayName("Supervised From")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? SupervisionStartDate { get; set; }

        [DisplayName("Supervised Until")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? SupervisionEndDate { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        [ForeignKey("FkEmployeesId")]
        public virtual Employees? FkEmployees { get; set; }

        [ForeignKey("FkRoleNamesId")]
        public virtual RoleNames? FkRoleNames { get; set; }
    }
}
