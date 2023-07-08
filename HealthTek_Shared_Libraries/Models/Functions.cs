using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace HealthTek_Shared_Libraries
{
    public partial class Functions
    {
        [DisplayName("ID")]
        public int FunctionsId { get; set; }

        [DisplayName("Behavior")]
        public int? FkMaladaptivesId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Function.")]
        [DisplayName("Function")]
        public string? FunctionName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Desription")]
        public string? Description { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Preventive Strategies")]
        public string? PreventiveStrategies { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Replacement Skills")]
        public string? ReplacementSkills { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Management Strategies")]
        public string? ManagementStrategies { get; set; }

        [DisplayName("Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Last Updated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:M/d/yyyy HH:mm}")]
        public DateTime LastUpdateDate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual Maladaptives? FkMaladaptives { get; set; }
    }
}
