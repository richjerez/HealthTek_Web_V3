using DinkToPdf;
using DinkToPdf.Contracts;
using HealthTek_Shared_Libraries.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;

namespace HealthTek_Web_V3.Services
{
    public interface IReportService
    {
        public byte[] GeneratePdfReport(string ClassName, string? EmployeeId);
    }
    public class ReportService : IReportService
    {
        private readonly IConverter _converter;
        private readonly IdentityContext _context;
        public ReportService(IConverter converter, IdentityContext context)
        {
            _converter = converter;
            _context = context;
        }
        public byte[] GeneratePdfReport(string ClassName, string? EmployeeId)
        {
            var html = new StringBuilder();
            var daterange = "";
            switch (ClassName)
            {
                #region Billing
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
                    daterange = "From: " + billing.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Service</th>
                                        <th style='padding: 8px;'>DOS</th>
                                        <th style='padding: 8px;'>Client-MR#|DX</th>
                                        <th style='padding: 8px;'>Employee-NPI|ID</th>
                                        <th style='padding: 8px;'>PA Number-Code</th>
                                        <th style='padding: 8px;'>Units-Min|Hrs</th>
                                        <th style='padding: 8px;'>Start-End</th>
                                        <th style='padding: 8px;'>Status</th>
                                    </tr>");
                    foreach (var item in billing)
                    {
                        var auth = item.FkClients.Authorizations.Where(m => m.FkServiceCodesId == item.FkServiceCodesId).FirstOrDefault();
                        if (auth != null)
                        {
                            html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                  </tr>", item.QaStatus, item.StartTime.ToString(), item.FkClients.FullName + " - " + item.FkClients.ClientsFacilities.FirstOrDefault().ChartNumber
          + " | " + item.FkClients.Diagnosis.FirstOrDefault().DiagnosisCode, item.FkEmployees.EmployeeLabel + " - " + item.FkEmployees.EmployeeIdentifier
          + " | " + item.FkEmployees.ProviderNumber, auth.AuthorizationNumber + " - " + item.FkServiceCodes.FullCode,
          auth.UnitAmount + " - " + auth.WeeklyUnits + " | " + auth.WeeklyHours, item.FkStartLocation.LocationName + " - " +
          item.FkEndLocation.LocationName, item.BillingStatus);
                        }
                        else
                        {
                            html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                  </tr>", item.QaStatus, item.StartTime.ToString(), item.FkClients.FullName + " - " + item.FkClients.ClientsFacilities.FirstOrDefault().ChartNumber
          + " | " + item.FkClients.Diagnosis.FirstOrDefault().DiagnosisCode, item.FkEmployees.EmployeeLabel + " - " + item.FkEmployees.EmployeeIdentifier
          + " | " + item.FkEmployees.ProviderNumber, " No Auth - " + item.FkServiceCodes.FullCode,
          "No Auth", item.FkStartLocation.LocationName + " - " +
          item.FkEndLocation.LocationName, item.BillingStatus);
                        }

                    }
                    break;
                #endregion
                #region Authorizations
                case "Authorizations":
                    var auth0 = _context.Authorizations.Include(m => m.FkClients).Include(m => m.FkServiceCodes).ToList();
                    daterange = "From: " + auth0.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Status</th>
                                        <th style='padding: 8px;'>Client</th>
                                        <th style='padding: 8px;'>Code</th>
                                        <th style='padding: 8px;'>Pa Number</th>
                                        <th style='padding: 8px;'>Units</th>
                                        <th style='padding: 8px;'>Effective Date</th>
                                        <th style='padding: 8px;'>Expiration Date</th>
                                    </tr>");
                    foreach (var item in auth0)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>
                                    <td style='padding: 5px;'>{3}</td>
                                    <td style='padding: 5px;'>{4}</td>
                                    <td style='padding: 5px;'>{5}</td>
                                    <td style='padding: 5px;'>{6}</td>
                                  </tr>", item.AuthorizationStatus, item.FkClients.FullName, item.FkServiceCodes.CodeTitle,
                                  item.AuthorizationNumber, item.UnitAmount, item.EffectiveDate, item.ExpirationDate);
                    }
                    break;
                #endregion
                #region Tasks
                case "Tasks":
                    var tasks = _context.Tasks.Include(m => m.FkAssignedBy).Where(m => m.FkAssignedToId == EmployeeId).ToList();
                    daterange = "From: " + tasks.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Due Date</th>
                                        <th style='padding: 8px;'>Subject</th>
                                        <th style='padding: 8px;'>Description</th>
                                        <th style='padding: 8px;'>Assigned By</th>
                                        <th style='padding: 8px;'>Type</th>
                                        <th style='padding: 8px;'>Status</th>
                                    </tr>");
                    foreach (var item in tasks)
                    {
                        var date = item.DueDate.ToString();
                        if (date != null)
                        {
                            date = "No Due Date";
                        }
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>
                                    <td style='padding: 5px;'>{3}</td>
                                    <td style='padding: 5px;'>{4}</td>
                                    <td style='padding: 5px;'>{5}</td>
                                  </tr>", date, item.TaskSubject, item.TaskDescription, item.FkAssignedBy.FullName, item.TaskType, item.TaskStatus);
                    }
                    break;
                #endregion
                #region CaregiverCompChecksCatalog
                case "CaregiverCompChecksCatalog":
                    var CaregiverCompChecksCatalogModel = _context.CaregiverCompChecksCatalog.ToList();
                    daterange = "From: " + CaregiverCompChecksCatalogModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Training Item</th>
                                    </tr>");
                    foreach (var item in CaregiverCompChecksCatalogModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                  </tr>", item.TrainingItem);
                    }
                    break;
                #endregion
                #region CaregiverFeedback
                case "CaregiverFeedback":
                    var CaregiverFeedbackModel = _context.CaregiverFeedback.ToList();
                    daterange = "From: " + CaregiverFeedbackModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Feedback</th>
                                        <th style='padding: 8px;'>Created</th>
                                        <th style='padding: 8px;'>Last Updated</th>
                                    </tr>");
                    foreach (var item in CaregiverFeedbackModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>
                                  </tr>", item.Feedback, item.CreationDate, item.LastUpdateDate);
                    }
                    break;
                #endregion
                #region Caregivers
                case "Caregivers":
                    var CaregiversModel = _context.Caregivers.Include(m => m.Locations).ToList();
                    daterange = "From: " + CaregiversModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Full Name</th>
                                        <th style='padding: 8px;'>Client</th>
                                        <th style='padding: 8px;'>Relationship</th>
                                        <th style='padding: 8px;'>Phone Number</th>
                                        <th style='padding: 8px;'>Email</th>
                                        <th style='padding: 8px;'>Comments</th>
                                        <th style='padding: 8px;'>Created</th>
                                        <th style='padding: 8px;'>Last Updated</th>
                                    </tr>");
                    foreach (var item in CaregiversModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>
                                    <td style='padding: 5px;'>{3}</td>
                                    <td style='padding: 5px;'>{4}</td>
                                    <td style='padding: 5px;'>{5}</td>
                                    <td style='padding: 5px;'>{6}</td>
                                    <td style='padding: 5px;'>{7}</td>
                                  </tr>", item.FullName, item.FkClients.FullName, item.Relationship, item.PhoneNumber, item.Email, item.Comments, item.CreationDate, item.LastUpdateDate);
                    }
                    break;
                #endregion
                #region Clients
                case "Clients":
                    var ClientsModel = _context.Clients.Include(m => m.Locations).ToList();
                    daterange = "From: " + ClientsModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Full Name</th>
                                        <th style='padding: 8px;'>Status</th>
                                        <th style='padding: 8px;'>Email</th>
                                        <th style='padding: 8px;'>Phone Number</th>
                                        <th style='padding: 8px;'>Alternate Number</th>
                                        <th style='padding: 8px;'>School Level</th>
                                        <th style='padding: 8px;'>Created</th>
                                        <th style='padding: 8px;'>Last Updated</th>
                                    </tr>");
                    foreach (var item in ClientsModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>
                                    <td style='padding: 5px;'>{3}</td>
                                    <td style='padding: 5px;'>{4}</td>
                                    <td style='padding: 5px;'>{5}</td>
                                    <td style='padding: 5px;'>{6}</td>
                                    <td style='padding: 5px;'>{7}</td>
                                  </tr>", item.FullName, item.ClientStatus, item.Email, item.MainPhoneNumber, item.AlternateContactInfo, item.SchoolLevel ?? "", item.CreationDate, item.LastUpdateDate);
                    }
                    break;
                #endregion
                #region ClientEvents
                case "ClientEvents":
                    var ClientEventsModel = _context.ClientEvents.ToList();
                    daterange = "From: " + ClientEventsModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Event Type</th>
                                    </tr>");
                    foreach (var item in ClientEventsModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                  </tr>", item.EventType);
                    }
                    break;
                case "ClientEventTypesCatalog":
                    var ClientEventTypesModel = _context.ClientEventTypesCatalog.ToList();
                    daterange = "From: " + ClientEventTypesModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Event Type</th>
                                    </tr>");
                    foreach (var item in ClientEventTypesModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                  </tr>", item.EventType);
                    }
                    break;
                #endregion
                #region ClientInsurances
                case "ClientInsurances":
                    var ClientInsurancesModel = _context.ClientInsurances.Include(m => m.FkClients).ToList();
                    daterange = "From: " + ClientInsurancesModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Client</th>
                                        <th style='padding: 8px;'>Status</th>
                                        <th style='padding: 8px;'>Policy Name</th>
                                        <th style='padding: 8px;'>Program</th>
                                    </tr>");
                    foreach (var item in ClientInsurancesModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>
                                    <td style='padding: 5px;'>{3}</td>
                                  </tr>", item.FkClients.FullName, item.PolicyStatus, item.PolicyName, item.PolicyProgram);
                    }
                    break;
                #endregion
                #region ClientInsurancesCatalog
                case "ClientInsurancesCatalog":
                    var ClientInsurancesCatalogModel = _context.ClientInsurancesCatalog.ToList();
                    daterange = "From: " + ClientInsurancesCatalogModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Policy Name</th>
                                        <th style='padding: 8px;'>Policy Program</th>
                                    </tr>");
                    foreach (var item in ClientInsurancesCatalogModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                  </tr>", item.PolicyName, item.PolicyProgram);
                    }
                    break;
                #endregion
                #region Documents
                case "Documents":
                    var DocumentsModel = _context.Documents.ToList();
                    daterange = "From: " + DocumentsModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Type</th>
                                        <th style='padding: 8px;'>Title</th>
                                        <th style='padding: 8px;'>Description</th>
                                        <th style='padding: 8px;'>Identifier</th>
                                        <th style='padding: 8px;'>Created</th>
                                        <th style='padding: 8px;'>Last Updated</th>
                                    </tr>");
                    foreach (var item in DocumentsModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>
                                    <td style='padding: 5px;'>{3}</td>
                                    <td style='padding: 5px;'>{4}</td>
                                    <td style='padding: 5px;'>{5}</td>
                                  </tr>", item.DocumentType, item.DocumentTitle, item.DocumentDescription, item.DocumentIdentifier, item.CreationDate, item.LastUpdateDate);
                    }
                    break;
                #endregion
                #region Employees
                case "Employees":
                    var EmployeesModel = _context.Employees.Include(m => m.Locations).ToList();
                    daterange = "From: " + EmployeesModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Full Name</th>
                                        <th style='padding: 8px;'>PhoneNumber</th>
                                        <th style='padding: 8px;'>Age</th>
                                        <th style='padding: 8px;'>Gender</th>
                                        <th style='padding: 8px;'>Address</th>
                                        <th style='padding: 8px;'>Join Date</th>
                                    </tr>");
                    foreach (var item in EmployeesModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>
                                    <td style='padding: 5px;'>{3}</td>
                                    <td style='padding: 5px;'>{4}</td>
                                    <td style='padding: 5px;'>{5}</td>
                                  </tr>", item.FullName, item.PhoneNumber, item.Age, item.Gender, item.Locations.FullPrimaryAddress, item.CreationDate.ToString());
                    }
                    break;
                #endregion
                #region EnvironmentalsCatalog
                case "EnvironmentalsCatalog":
                    var EnvironmentalsCatalogModel = _context.EnvironmentalsCatalog.ToList();
                    daterange = "From: " + EnvironmentalsCatalogModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Descriptione</th>
                                        <th style='padding: 8px;'>Category</th>
                                    </tr>");
                    foreach (var item in EnvironmentalsCatalogModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                  </tr>", item.Description, item.Category);
                    }
                    break;
                #endregion
                #region Facilities
                case "Facilities":
                    var FacilitiesModel = _context.Facilities.ToList();
                    daterange = "From: " + FacilitiesModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Name</th>
                                        <th style='padding: 8px;'>Type</th>
                                    </tr>");
                    foreach (var item in FacilitiesModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                  </tr>", item.FacilityName, item.FacilityType);
                    }
                    break;
                #endregion
                #region Functions
                case "Functions":
                    var FunctionsModel = _context.Functions.ToList();
                    daterange = "From: " + FunctionsModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Function</th>
                                        <th style='padding: 8px;'>Description</th>
                                    </tr>");
                    foreach (var item in FunctionsModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                  </tr>", item.FunctionName, item.Description);
                    }
                    break;
                #endregion
                #region IntakeDocsCatalog
                case "IntakeDocsCatalog":
                    var IntakeDocsCatalogModel = _context.IntakeDocsCatalog.ToList();
                    daterange = "From: " + IntakeDocsCatalogModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Name</th>
                                        <th style='padding: 8px;'>Descriptione</th>
                                    </tr>");
                    foreach (var item in IntakeDocsCatalogModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                  </tr>", item.IntakeDocName, item.IntakeDocDescription);
                    }
                    break;
                #endregion
                #region Intakes
                case "Intakes":
                    var IntakesModel = _context.Intakes.Include(m => m.FkClients).ToList();
                    daterange = "From: " + IntakesModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Status</th>
                                        <th style='padding: 8px;'>Client</th>
                                        <th style='padding: 8px;'>Note</th>
                                    </tr>");
                    foreach (var item in IntakesModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>
                                  </tr>", item.IntakeStatus, item.FkClients.FullName, item.StatusNote);
                    }
                    break;
                #endregion
                #region Interventions
                case "Interventions":
                    var InterventionsModel = _context.Interventions.ToList();
                    daterange = "From: " + InterventionsModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Intervention</th>
                                        <th style='padding: 8px;'>Description</th>
                                    </tr>");
                    foreach (var item in InterventionsModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                  </tr>", item.InterventionName, item.InterventionDescription);
                    }
                    break;
                #endregion
                #region MaladaptivesCatalog
                case "MaladaptivesCatalog":
                    var MaladaptivesCatalogModel = _context.MaladaptivesCatalog.ToList();
                    daterange = "From: " + MaladaptivesCatalogModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Maladaptive Name</th>
                                    </tr>");
                    foreach (var item in MaladaptivesCatalogModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>		
                                  </tr>", item.MaladaptiveName);
                    }
                    break;
                #endregion
                #region OperatingCounties
                case "OperatingCounties":
                    var OperatingCountiesModel = _context.OperatingCounties.ToList();
                    daterange = "From: " + OperatingCountiesModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>County</th>
                                        <th style='padding: 8px;'>State</th>
                                    </tr>");
                    foreach (var item in OperatingCountiesModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                  </tr>", item.County, item.State);
                    }
                    break;
                #endregion
                #region PreferencesCatalog
                case "PreferencesCatalog":
                    var PreferencesCatalogModel = _context.PreferencesCatalog.ToList();
                    daterange = "From: " + PreferencesCatalogModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Preference</th>
                                        <th style='padding: 8px;'>Creation Date</th>
                                        <th style='padding: 8px;'>Last Update Date</th>
                                    </tr>");
                    foreach (var item in PreferencesCatalogModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>					
                                  </tr>", item.Preference, item.CreationDate, item.LastUpdateDate);
                    }
                    break;
                #endregion
                #region RbtCompetencies
                case "RbtCompetencies":
                    var RbtCompetenciesModel = _context.RbtCompetencies.ToList();
                    daterange = "From: " + RbtCompetenciesModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Client</th>
                                        <th style='padding: 8px;'>Competency Date</th>
                                    </tr>");
                    foreach (var item in RbtCompetenciesModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>				
                                  </tr>", item.Supervisions.FkAppointments.FkClients.FullName, item.CompetencyDate);
                    }
                    break;
                #endregion
                #region RbtCompTrainingsCatalog
                case "RbtCompTrainingsCatalog":
                    var RbtCompTrainingsCatalogModel = _context.RbtCompTrainingsCatalog.ToList();
                    daterange = "From: " + RbtCompTrainingsCatalogModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Training Item</th>
                                    </tr>");
                    foreach (var item in RbtCompTrainingsCatalogModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>	
                                  </tr>", item.TrainingItem);
                    }
                    break;
                #endregion
                #region ReinforcerCatalog
                case "ReinforcerCatalog":
                    var ReinforcerCatalogModel = _context.ReinforcerCatalog.ToList();
                    daterange = "From: " + ReinforcerCatalogModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Reinforcer Name</th>
                                        <th style='padding: 8px;'>Reinforcer Description</th>
                                    </tr>");
                    foreach (var item in ReinforcerCatalogModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>				
                                  </tr>", item.ReinforcerName, item.ReinforcerDescription);
                    }
                    break;
                #endregion
                #region RoleDocsCatalog
                case "RoleDocsCatalog":
                    var RoleDocsCatalog = _context.RoleDocsCatalog.ToList();
                    daterange = "From: " + RoleDocsCatalog.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Title</th>
                                        <th style='padding: 8px;'>Expiration</th>
                                        <th style='padding: 8px;'>Description</th>
                                    </tr>");
                    foreach (var item in RoleDocsCatalog)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>			
                                    <td style='padding: 5px;'>{1}</td>			
                                    <td style='padding: 5px;'>{2}</td>			
                                  </tr>", item.Title, item.Expiration, item.Description);
                    }
                    break;
                #endregion
                #region ReplacementsCatalog
                case "ReplacementsCatalog":
                    var ReplacementsCatalogModel = _context.ReplacementsCatalog.ToList();
                    daterange = "From: " + ReplacementsCatalogModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Replacement</th>
                                    </tr>");
                    foreach (var item in ReplacementsCatalogModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>			
                                  </tr>", item.Replacement);
                    }
                    break;
                #endregion
                #region RoleNames
                case "RoleNames":
                    var RoleNamesModel = _context.RoleNames.ToList();
                    daterange = "From: " + RoleNamesModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Role</th>
                                        <th style='padding: 8px;'>Description</th>
                                        <th style='padding: 8px;'>Category</th>
                                    </tr>");
                    foreach (var item in RoleNamesModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>					
                                  </tr>", item.RoleName, item.RoleDescription, item.Category);
                    }
                    break;
                #endregion
                #region ServiceCodes
                case "ServiceCodes":
                    var ServiceCodesModel = _context.ServiceCodes.ToList();
                    daterange = "From: " + ServiceCodesModel.FirstOrDefault().CreationDate.ToShortDateString() + " To: " + DateTime.Now.ToShortDateString();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Type</th>
                                        <th style='padding: 8px;'>FullCpt</th>
                                        <th style='padding: 8px;'>Title</th>
                                        <th style='padding: 8px;'>Description</th>
                                        <th style='padding: 8px;'>Code Rate</th>
                                        <th style='padding: 8px;'>Rate Type</th>
                                    </tr>");
                    foreach (var item in ServiceCodesModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>
                                    <td style='padding: 5px;'>{3}</td>
                                    <td style='padding: 5px;'>{4}</td>						
                                    <td style='padding: 5px;'>{5}</td>						
                                  </tr>", item.ServiceCodeType, item.FullCpt, item.CodeTitle, item.CodeDescription, item.CodeRate, item.CodeRateType);
                    }
                    break;
                #endregion
                #region Supervisions
                case "Supervisions":
                    var SupervisionsModel = _context.Supervisions.Include(m => m.FkAppointments).ThenInclude(m => m.FkEmployees).Include(m => m.FkAppointments).ThenInclude(m => m.FkFacilities).ThenInclude(m => m.ClientsFacilities).ToList();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Supervision Status</th>
                                        <th style='padding: 8px;'>Client</th>
                                        <th style='padding: 8px;'>Employee</th>
                                        <th style='padding: 8px;'>Appointment</th>
                                        <th style='padding: 8px;'>Duration</th>
                                    </tr>");
                    foreach (var item in SupervisionsModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>
                                    <td style='padding: 5px;'>{3}</td>
                                    <td style='padding: 5px;'>{4}</td>											
                                  </tr>", item.SupervisionStatus, item.FkAppointments.FkFacilities.ClientsFacilities.FirstOrDefault().ClientChartLabel, item.FkAppointments.FkEmployees.EmployeeLabel, item.FkAppointments.TimeSlot, item.SupervisionDuration);
                    }
                    break;
                #endregion
                #region Dashboards
                case "Dashboards":
                    var DashboardsModel = _context.Dashboards.Where(m => m.FkUserId == EmployeeId)
                        .Include(m => m.FkDashboardWidgets).ThenInclude(m => m.FkWidget).ToList();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Name</th>
                                        <th style='padding: 8px;'>Widgets</th>
                                    </tr>");
                    foreach (var item in DashboardsModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                  </tr>", item.DashboardName, string.Join(", ", item.FkDashboardWidgets.Select(m => m.FkWidget.WidgetName)));
                    }
                    break;
                #endregion
                #region Widgets
                case "Widgets":
                    var WidgetsModel = _context.Widgets.ToList();
                    html.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Bio-Behavioral Corp (BBC)</h1></div>
                                <table width='100%'>
                                    <tr style='background: #4e73df;color: white;text-align: left;'>
                                        <th style='padding: 8px;'>Widget</th>
                                        <th style='padding: 8px;'>Model</th>
                                        <th style='padding: 8px;'>View</th>
                                    </tr>");
                    foreach (var item in WidgetsModel)
                    {
                        html.AppendFormat(@"<tr>
                                    <td style='padding: 5px;'>{0}</td>
                                    <td style='padding: 5px;'>{1}</td>
                                    <td style='padding: 5px;'>{2}</td>					
                                  </tr>", item.WidgetName, item.Style, item.ViewName);
                    }
                    break;
                    #endregion
            }
            html.Append(@"
                                </table>
                            </body>
                        </html>");

            #region Settings
            GlobalSettings globalSettings = new GlobalSettings();
            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Landscape;
            globalSettings.PaperSize = PaperKind.A4;
            globalSettings.Margins = new MarginSettings { Top = 10, Bottom = 10 };
            ObjectSettings objectSettings = new ObjectSettings();
            objectSettings.PagesCount = true;
            objectSettings.HtmlContent = html.ToString();
            WebSettings webSettings = new WebSettings();
            webSettings.DefaultEncoding = "utf-8";
            HeaderSettings headerSettings = new HeaderSettings();
            headerSettings.FontSize = 12;
            headerSettings.FontName = "Ariel";
            headerSettings.Right = "Page [page] of [toPage]";
            headerSettings.Line = true;
            FooterSettings footerSettings = new FooterSettings();
            footerSettings.FontSize = 10;
            footerSettings.FontName = "Ariel";
            footerSettings.Left = "HealthTek 2022";
            footerSettings.Right = daterange;
            footerSettings.Line = true;
            objectSettings.HeaderSettings = headerSettings;
            objectSettings.FooterSettings = footerSettings;
            objectSettings.WebSettings = webSettings;
            #endregion

            HtmlToPdfDocument htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };
            return _converter.Convert(htmlToPdfDocument);
        }

        public byte[] GeneratePdfReport(string ClassName)
        {
            throw new NotImplementedException();
        }
    }
}
