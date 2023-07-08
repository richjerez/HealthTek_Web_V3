using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.ServiceControllers
{
    [Authorize(Policy = "QAViews")]
    public class QualityAssuranceController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;

        public QualityAssuranceController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int? id)
        {
            var codes = await _context.ServiceCodes.OrderBy(m => m.CodeTitle).ToListAsync();
            var firstcode = await _context.ServiceCodes.FirstAsync();
            var identityContext = await _context.Appointments.Where(m => m.FkServiceCodesId == firstcode.ServiceCodesId).Include(a => a.FkClients).Include(a => a.FkEmployees).ToListAsync();
            if (id != null)
            {
                firstcode = _context.ServiceCodes.FirstOrDefault(m => m.ServiceCodesId == id);
            }
            if (firstcode != null)
            {
                switch (firstcode.CodeTitle)
                {
                    default:
                        identityContext = await _context.Appointments.Where(m => m.FkServiceCodesId == firstcode.ServiceCodesId).ToListAsync();
                        break;
                    case "BA Assessment":
                        identityContext = await _context.Appointments.Where(m => m.FkServiceCodesId == firstcode.ServiceCodesId && m.QaStatus != "Approved" && m.QaStatus != "Rejected").Include(a => a.BaAssessments).Include(a => a.FkClients).Include(a => a.FkEmployees).ToListAsync();
                        break;
                    case "BA Note ABA":
                    case "BA Note Grp":
                    case "BA Note LA":
                    case "BA Note RBT":
                        identityContext = await _context.Appointments
                            .Where(m => m.FkServiceCodesId == firstcode.ServiceCodesId
                            && m.QaStatus != "Approved" && m.QaStatus != "Rejected")
                            .Include(a => a.BaProgressNotes)
                            .Include(a => a.FkClients)
                            .Include(a => a.FkEmployees)
                            .ThenInclude(a => a.FkESignatures)
                            .ToListAsync();
                        break;
                }
            }
            ViewData["ActiveTab"] = firstcode.CodeTitle;
            ViewData["Services"] = codes;
            return View(identityContext);
        }
        public async Task<IActionResult> ChangeQAStatus(int id, string status)
        {
            var appointment = _context.Appointments.Include(m => m.FkClients).ThenInclude(m => m.Authorizations).FirstOrDefault(o => o.AppointmentsId == id);
            var acceptqa = "Approved";
            var denyqa = "Rejected";
            var billing = "Billable"; ;
            var user = _userManager.GetUserAsync(User).Result;
            switch (status)
            {
                case "Accept":
                    appointment.QaStatus = acceptqa;
                    appointment.BillingStatus = billing;
                    var auth = appointment.FkClients.Authorizations.Where(m => m.FkServiceCodesId == appointment.FkServiceCodesId).FirstOrDefault();
                    auth.UnitsUsed += appointment.Units;
                    _context.Authorizations.Update(auth);
                    await _context.SaveChangesAsync();
                    break;
                case "Reject":
                    appointment.QaStatus = denyqa;
                    //Create Task 
                    Tasks tasks = new Tasks();
                    tasks.CreationDate = DateTime.Now;
                    tasks.LastUpdateDate = DateTime.Now;
                    tasks.FkAssignedToId = appointment.FkEmployeesId;
                    tasks.TaskType = "Reply";
                    tasks.TaskStatus = denyqa;
                    tasks.TaskDescription = "Quality Assurance Rejection";
                    tasks.TaskNote = "We are sorry to inform you that the folowing appointment has been rejected!";
                    tasks.FkAssignedById = user.FkEmployeesId;
                    tasks.TaskIdentifier = appointment.AppointmentsId.ToString();
                    _context.Tasks.Add(tasks);
                    await _context.SaveChangesAsync();

                    break;
            }
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), id);
        }
        public async Task<IActionResult> ToggleFormSignature(int id, string status, string toggleAction, string who)
        {
            var user = await _userManager.GetUserAsync(User);
            var unsigned = "Unsigned";
            var billable = "Billable";

            switch (toggleAction)
            {
                case "Sign":
                    switch (status)
                    {
                        case "BA Assessment":
                            var sign = _context.BaAssessments.Include(m => m.FkAppointments).FirstOrDefault(i => i.BaAssessmentsId == id);
                            var empSignature = _context.Employees.Include(m => m.FkESignatures).FirstOrDefault(m => m.EmployeesId == user.FkEmployeesId);
                            switch (who)
                            {
                                case "Super":
                                    sign.FkSupervisorSignatureId = empSignature.FkESignatures.ESignaturesId;
                                    break;
                                case "User":
                                    sign.FkAnalystSignatureId = empSignature.FkESignatures.ESignaturesId;
                                    break;
                            }
                            // Cheack to see if both supervisor and supervisee have signed and set the status
                            if (sign.FkAnalystSignatureId != null && sign.FkSupervisorSignatureId != null)
                            {
                                sign.FkAppointments.QaStatus = billable;
                                sign.FkAppointments.BillingStatus = billable;
                            }
                            else
                            {
                                sign.FkAppointments.QaStatus = unsigned;
                            }

                            _context.BaAssessments.Update(sign);
                            await _context.SaveChangesAsync();

                            _context.Appointments.Update(sign.FkAppointments);
                            await _context.SaveChangesAsync();

                            break;
                        case "BA Note ABA":
                        case "BA Note Grp":
                        case "BA Note LA":
                        case "BA Note RBT":
                            var signNote = _context.BaProgressNotes.Include(m => m.FkAppointments).FirstOrDefault(i => i.BaProgressNotesId == id);
                            empSignature = _context.Employees.Include(m => m.FkESignatures).FirstOrDefault(m => m.EmployeesId == user.FkEmployeesId);
                            switch (who)
                            {
                                case "Super":
                                    signNote.FkSupervisorSignatureId = empSignature.FkESignatures.ESignaturesId;
                                    break;
                                case "User":
                                    signNote.FkEmployeeSignatureId = empSignature.FkESignatures.ESignaturesId;
                                    break;
                            }
                            // Cheack to see if both supervisor and supervisee have signed and set the status
                            if (signNote.FkEmployeeSignatureId != null && signNote.FkSupervisorSignatureId != null)
                            {
                                signNote.FkAppointments.QaStatus = billable;
                            }
                            else
                            {
                                signNote.FkAppointments.QaStatus = unsigned;
                            }

                            _context.BaProgressNotes.Update(signNote);
                            await _context.SaveChangesAsync();

                            _context.Appointments.Update(signNote.FkAppointments);
                            await _context.SaveChangesAsync();

                            break;
                    }
                    break;
                case "RemoveSignature":
                    switch (status)
                    {
                        case "BA Assessment":
                            var removesignature = await _context.BaAssessments.Include(f => f.FkAppointments).FirstOrDefaultAsync(i => i.BaAssessmentsId == id);
                            switch (who)
                            {
                                case "Super":
                                    removesignature.FkSupervisorSignatureId = null;
                                    break;
                                case "User":
                                    removesignature.FkAnalystSignatureId = null;
                                    break;
                            }
                            removesignature.FkAppointments.QaStatus = unsigned;
                            _context.BaAssessments.Update(removesignature);
                            await _context.SaveChangesAsync();
                            _context.Appointments.Update(removesignature.FkAppointments);
                            await _context.SaveChangesAsync();
                            break;
                        case "BA Note ABA":
                        case "BA Note Grp":
                        case "BA Note LA":
                        case "BA Note RBT":
                            var remsignature = await _context.BaProgressNotes.Include(m => m.FkAppointments).FirstOrDefaultAsync(i => i.BaProgressNotesId == id);
                            switch (who)
                            {
                                case "Super":
                                    remsignature.FkSupervisorSignatureId = null;
                                    break;
                                case "User":
                                    remsignature.FkEmployeeSignatureId = null;
                                    break;
                            }
                            remsignature.FkAppointments.QaStatus = unsigned;
                            _context.BaProgressNotes.Update(remsignature);
                            await _context.SaveChangesAsync();
                            _context.Appointments.Update(remsignature.FkAppointments);
                            await _context.SaveChangesAsync();
                            break;
                    }
                    break;
            }
            return RedirectToAction(nameof(Index), id);
        }
    }
}
