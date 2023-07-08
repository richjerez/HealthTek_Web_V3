using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Services
{
    public class CSVExporter : FileResult
    {
        private readonly string _employeeData;
        private readonly string _classname;
        private readonly IdentityContext _context;
        public CSVExporter(string employeeData, string fileDownloadName, IdentityContext context, string classname) : base("text/csv")
        {
            _employeeData = employeeData;
            FileDownloadName = fileDownloadName;
            _context = context;
            _classname = classname;
        }
        public async override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            context.HttpContext.Response.Headers.Add("Content-Disposition", new[] { "attachment; filename=" + FileDownloadName });
            using (var streamWriter = new StreamWriter(response.Body))
            {
                switch (_classname)
                {
                    case "Tasks":
                        var tasks = _context.Tasks.Where(m => m.FkAssignedToId == _employeeData)
    .Include(m => m.FkAssignedBy).ToList();
                        await streamWriter.WriteLineAsync(
  $"Due Date, Subject, Description, Assigned By, Type, Status");
                        foreach (var p in tasks)
                        {
                            var duedate = "";
                            if (p.DueDate != null)
                            {
                                duedate = p.DueDate.ToString();
                            }
                            else
                            {
                                duedate = "No Due Date";
                            }
                            await streamWriter.WriteLineAsync(
                              $"{duedate}, {p.TaskSubject}, {p.TaskDescription}" +
                              $", {p.FkAssignedBy.EmployeeLabel}, {p.TaskType}, {p.TaskStatus}"
                            );
                            await streamWriter.FlushAsync();
                        }
                        break;
                    case "Billing":
                        var billing = _context.Appointments
                    .Where(m => m.AppointmentType.Contains("Service") && m.FkServiceCodes != null
                        && m.FkClients.Authorizations != null)
                    .Include(m => m.FkEmployees)
                    .Include(m => m.FkBatches)
                    .Include(m => m.FkServiceCodes)
                    .Include(m => m.FkClients)
                    .ThenInclude(m => m.ClientsFacilities)
                    .AsSplitQuery()
                    .Include(m => m.FkClients)
                    .ThenInclude(m => m.Authorizations)
                    .AsSplitQuery()
                    .Include(m => m.FkClients)
                    .ThenInclude(m => m.Diagnosis)
                    .AsSplitQuery()
                    .Include(m => m.FkStartLocation)
                    .Include(m => m.FkEndLocation).ToList();
                        await streamWriter.WriteLineAsync(
  $"Service, DOS, Client-MR#|DX, Employee-NPI|ID, PA Number-Code, Units-Min|Hrs, Start-End, Status"
);
                        foreach (var p in billing)
                        {
                            var auth = p.FkClients.Authorizations.Where(m => m.FkServiceCodesId == p.FkServiceCodesId).FirstOrDefault();
                            if (auth != null)
                            {
                                await streamWriter.WriteLineAsync(
  $"{p.QaStatus}, {p.StartTime}, {p.FkClients.FullName + " - " + p.FkClients.ClientsFacilities.FirstOrDefault().ChartNumber + " | " + p.FkClients.Diagnosis.FirstOrDefault().DiagnosisCode}" +
  $", {p.FkEmployees.EmployeeLabel + " - " + p.FkEmployees.EmployeeIdentifier + " | " + p.FkEmployees.ProviderNumber}, " +
  $"{auth.AuthorizationNumber + " - " + p.FkServiceCodes.FullCode}, {auth.UnitAmount + " - " + auth.WeeklyUnits + " | " + auth.WeeklyHours}, " +
  $"{p.FkStartLocation.LocationName + " - " + p.FkEndLocation.LocationName}, {p.BillingStatus}");
                            }
                            else
                            {
                                await streamWriter.WriteLineAsync(
$"{p.QaStatus}, {p.StartTime}, {p.FkClients.FullName + " - " + p.FkClients.ClientsFacilities.FirstOrDefault().ChartNumber + " | " + p.FkClients.Diagnosis.FirstOrDefault().DiagnosisCode}" +
$", {p.FkEmployees.EmployeeLabel + " - " + p.FkEmployees.EmployeeIdentifier + " | " + p.FkEmployees.ProviderNumber}, " +
$"{"No Auth - " + p.FkServiceCodes.FullCode}, {"No Auth"}, " +
$"{p.FkStartLocation.LocationName + " - " + p.FkEndLocation.LocationName}, {p.BillingStatus}");
                            }

                            await streamWriter.FlushAsync();
                        }
                        break;
                }
                await streamWriter.FlushAsync();
            }
        }
    }
}
