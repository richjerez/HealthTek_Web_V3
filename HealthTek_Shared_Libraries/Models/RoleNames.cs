using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class RoleNames
    {
        public RoleNames()
        {
            EmployeesRoleNames = new HashSet<EmployeesRoleNames>();
        }

        [DisplayName("ID")]
        public int RoleNamesId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Role Name.")]
        [DisplayName("Role")]
        public string? RoleName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Description")]
        public string? RoleDescription { get; set; }

        [DataType(DataType.Text)]
        public string? DescriptiveRole
        {
            get
            {
                string descriptive = RoleName + ": " + " " + RoleDescription;
                return descriptive;
            }
        }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Category.")]
        [DisplayName("Category")]
        public string? Category { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy}")]
        public DateTime LastUpdateDate { get; set; }

        public virtual ICollection<EmployeesRoleNames>? EmployeesRoleNames { get; set; }

    }
}
