using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace HealthTek_Web_V3.Controllers
{
    public static class ManageSideBarMenu
    {
        public static string Dashboard => "Dashboard";

        public static string Authorizations => "Authorizations";

        public static string Intakes => "Intakes";

        public static string Appointments => "Appointments";

        public static string Employees => "Employees";

        public static string Clients => "Clients";

        public static string Assignments => "Assignments";

        public static string ClientInsurances => "ClientInsurances";

        public static string QualityAssurance => "QualityAssurance";
        public static string Billing => "Billing";

        public static string Supervisions => "Supervisions";

        public static string FileInbox => "FileInbox";

        public static string FileDropbox => "FileDropbox";

        public static string DashboardNavClass(ViewContext viewContext) => PageNavClass(viewContext, Dashboard);
        public static string AuthorizationsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Authorizations);
        public static string IntakesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Intakes);
        public static string AppointmentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Appointments);
        public static string EmployeesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Employees);
        public static string ClientsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Clients);
        public static string AssignmentsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Assignments);
        public static string ClientInsurancesNavClass(ViewContext viewContext) => PageNavClass(viewContext, ClientInsurances);
        public static string QualityAssuranceNavClass(ViewContext viewContext) => PageNavClass(viewContext, QualityAssurance);
        public static string BillingNavClass(ViewContext viewContext) => PageNavClass(viewContext, Billing);
        public static string SupervisionsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Supervisions);
        public static string FileInboxNavClass(ViewContext viewContext) => PageNavClass(viewContext, FileInbox);
        public static string FileDropboxNavClass(ViewContext viewContext) => PageNavClass(viewContext, FileDropbox);
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
