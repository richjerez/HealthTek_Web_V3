using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class Widgets
    {

        [DisplayName("ID")]
        public int WidgetId { get; set; }

        [DisplayName("Widget Name")]
        [Required(ErrorMessage = "Please enter Widget Name.")]
        [DataType(DataType.Text)]
        public string? WidgetName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter View Name.")]
        [DisplayName("View Name")]
        public string? ViewName { get; set; }

        [DataType(DataType.Text)]
        ///[Required(ErrorMessage = "Please enter Style.")]
        ///This is being nulled because there are current values 
        ///on the database that contain NULL values 
        [DisplayName("Style")]
        public string? Style { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Class Name.")]
        [DisplayName("Class Name")]
        public string? ClassName { get; set; }

        public DateTime? LastUpdateDate { get; set; }
    }
}
