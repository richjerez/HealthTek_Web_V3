using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthTek_Shared_Libraries
{
    public class Dashboards
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Dashboards()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            FkDashboardWidgets = new HashSet<DashboardWidgets>();
        }
        [NotMapped]
        public List<int>? Widgets { get; set; }

        public int DashboardId { get; set; }
        public string? DashboardName { get; set; }
        public bool MainView { get; set; }
        public string? FkUserId { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public IEnumerable<DashboardWidgets> FkDashboardWidgets { get; set; }

    }
}
