using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthTek_Shared_Libraries
{
    public partial class DashboardWidgets
    {
        [DisplayName("ID")]
        [Key]
        public int DashboardWidgetId { get; set; }

        [DisplayName("Widget Name")]
        public int FkWidgetId { get; set; }
        public int HierarchySlot { get; set; }

        [DisplayName("Model Name")]
        public int FkDashboardId { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public Dashboards? FkDashboards { get; set; }
        public Widgets? FkWidget { get; set; }
    }
}
