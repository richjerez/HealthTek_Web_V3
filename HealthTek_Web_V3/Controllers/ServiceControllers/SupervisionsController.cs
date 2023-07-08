using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "SupervisionViews")]
    public class SupervisionsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        ExternalLists externalLists = new ExternalLists();

        public SupervisionsController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<JsonResult> GetSupervisorNumber(string id)
        {
            var employees = await _context.Employees.Where(m => m.EmployeesId == id)
                .Include(m => m.EmployeesRoleNames.Where(e => e.FkRoleNames.RoleName == "Supervisor"))
                .Select(m => m.EmployeesRoleNames.FirstOrDefault()).FirstOrDefaultAsync();
            return Json(new { html = employees.SupervisorNumber ?? "This is returning null" });
        }

        public JsonResult GetSupervision(DateTime? superdate, string? employee, int? client)
        {
            double timeSpanHours = 0;
            List<Supervisions> supervisions = new List<Supervisions>();
            var searchFilter = "";
            if (superdate != null)
            {
                supervisions = _context.Supervisions.Include(m => m.FkAppointments).Where(m => m.FkAppointments.EndTime.Value.Month == superdate.Value.Month).ToList();
                searchFilter = supervisions.Select(m => m.FkAppointments.EndTime.Value.ToString("MMMM")).FirstOrDefault();
                if (employee != null && client != null)
                {
                    supervisions = _context.Supervisions.Where(m => m.FkAppointments.EndTime.Value.Month == superdate.Value.Month && m.FkAppointments.FkEmployeesId == employee && m.FkAppointments.FkClientsId == client).ToList();
                    searchFilter = _context.ClientsFacilities.Where(m => m.FkClientsId == client).Select(m => m.ClientChartLabel).FirstOrDefault();
                }
                else if (employee != null && client == null)
                {
                    supervisions = _context.Supervisions.Where(m => m.FkAppointments.EndTime.Value.Month == superdate.Value.Month && m.FkAppointments.FkEmployeesId == employee).ToList();
                    searchFilter = _context.Employees.Where(m => m.EmployeesId == employee).Select(m => m.EmployeeLabel).FirstOrDefault();
                }
                else if (employee == null && client != null)
                {
                    supervisions = _context.Supervisions.Where(m => m.FkAppointments.EndTime.Value.Month == superdate.Value.Month && m.FkAppointments.FkClientsId == client).ToList();
                    searchFilter = _context.ClientsFacilities.Where(m => m.FkClientsId == client).Select(m => m.ClientChartLabel).FirstOrDefault();
                }
            }
            else
            {
                if (employee != null && client != null)
                {
                    supervisions = _context.Supervisions.Where(m => m.FkAppointments.FkEmployeesId == employee && m.FkAppointments.FkClientsId == client).ToList();
                    searchFilter = _context.ClientsFacilities.Where(m => m.FkClientsId == client).Select(m => m.ClientChartLabel).FirstOrDefault();
                }
                else if (employee != null && client == null)
                {
                    supervisions = _context.Supervisions.Where(m => m.FkAppointments.FkEmployeesId == employee).ToList();
                    searchFilter = _context.Employees.Where(m => m.EmployeesId == employee).Select(m => m.EmployeeLabel).FirstOrDefault();
                }
                else if (employee == null && client != null)
                {
                    supervisions = _context.Supervisions.Where(m => m.FkAppointments.FkClientsId == client).ToList();
                    searchFilter = _context.ClientsFacilities.Where(m => m.FkClientsId == client).Select(m => m.ClientChartLabel).FirstOrDefault();
                }

            }
            TimeSpan hours = new TimeSpan();
            foreach (var i in supervisions)
            {
                hours += i.EndTime.Value - i.StartTime.Value;
            }

            timeSpanHours = hours.TotalHours;
            return Json(new { ResultValue = timeSpanHours, SearchFilter = searchFilter });
        }

        public async Task<ActionResult> UpdateSupervisionESig(int id, string actionESign)
        {
            var user = await _userManager.GetUserAsync(User);

            var eSignaturesToList = _context.ESignatures.Where(e => e.FkEmployeesId == user.FkEmployeesId).FirstOrDefault();
            var supers = _context.Supervisions.Include(f => f.FkAppointments).FirstOrDefault(m => m.SupervisionsId == id);
            var signed = "Unsigned";
            var Submitted = "Submitted";
            var SubmittedQA = "Received";

            switch (actionESign)
            {
                case "addUserSign":
                    supers.FkUserSignaturesId = eSignaturesToList.ESignaturesId;
                    break;
                case "removeUserSign":
                    supers.FkUserSignaturesId = null;
                    break;
                case "addSupvSign":
                    supers.FkSupervisorSignaturesId = eSignaturesToList.ESignaturesId;
                    break;
                case "removeSupvSign":
                    supers.FkSupervisorSignaturesId = null;
                    break;
            }
            if (supers.FkUserSignaturesId != null && supers.FkSupervisorSignaturesId != null)
            {
                supers.SupervisionStatus = Submitted;
                supers.FkAppointments.QaStatus = SubmittedQA;
                _context.Appointments.Update(supers.FkAppointments);
                await _context.SaveChangesAsync();
            }
            else
            {
                supers.SupervisionStatus = signed;
            }
            _context.Supervisions.Update(supers);
            await _context.SaveChangesAsync();

            return Json(new { ok = true });
        }

        // GET: Supervisions
        public async Task<IActionResult> Index()
        {
            var identityContext = await _context.Supervisions
                .Include(s => s.FkUserSignature)
                .Include(s => s.FkSupervisorSignature)
                .Include(s => s.FkAppointments)
                .ThenInclude(s => s.FkEmployees)
                .ThenInclude(s => s.EmployeesRoleNames)
                .ThenInclude(s => s.FkRoleNames)
                .AsSplitQuery()
                .Include(s => s.FkAppointments)
                .ThenInclude(s => s.FkServiceCodes)
                .AsSplitQuery()
                .Include(s => s.FkAppointments)
                .ThenInclude(s => s.FkClients)
                .ThenInclude(s => s.ClientsFacilities)
                .AsSplitQuery()
                .Include(s => s.FkRbtCompetencies).ToListAsync();
            ViewData["Employees"] = new SelectList(_context.Employees, "EmployeesId", "EmployeeLabel");
            ViewData["Clients"] = new SelectList(_context.Clients, "ClientsId", "FullName");
            ViewData["Status"] = new SelectList(externalLists.SupervisionStatuses);
            return View(identityContext);

        }

        // GET: Supervisions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supervisions = await _context.Supervisions
                .Include(s => s.FkAppointments)
                .Include(s => s.FkRbtCompetencies)
                .FirstOrDefaultAsync(m => m.SupervisionsId == id);
            if (supervisions == null)
            {
                return NotFound();
            }

            return View(supervisions);
        }

        // GET: Supervisions/Create
        public async Task<IActionResult> Create()
        {
            var userid = await _userManager.GetUserAsync(User);
            ViewBag.Modes = new SelectList(externalLists.SupervisionModes);
            ViewBag.Characteristics = new SelectList(externalLists.SupervisionCharacteristics);
            ViewBag.Ratings = new SelectList(externalLists.SupervisionRatings);
            ViewBag.Supervisors = new SelectList(_context.Employees.Where(e => e.IsSupervisor == true).ToList(), "EmployeesId", "EmployeeLabel");
            ViewData["FkAppointmentsId"] = new SelectList(_context.Appointments.Where(a => a.FkEmployeesId == userid.FkEmployeesId && a.FkServiceCodes.CodeTitle.Contains("Ba Note")), "AppointmentsId", "TimeSlot");
            ViewData["FkRbtCompetenciesId"] = new SelectList(_context.RbtCompetencies, "RbtCompetenciesId", "RbtCompetenciesId");
            ViewData["Status"] = new SelectList(externalLists.SupervisionStatuses);
            return View();
        }

        // POST: Supervisions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Supervisions supervisions)
        {
            if (ModelState.IsValid)
            {
                double duration = (supervisions.EndTime.Value - supervisions.StartTime.Value).TotalMinutes / 60;
                supervisions.SupervisionDuration = (decimal)Math.Round(duration, 2);

                _context.Add(supervisions);
                await _context.SaveChangesAsync();
                RbtCompetencies rbt = new RbtCompetencies();
                BcabaSupvMeetings bcaba = new BcabaSupvMeetings();
                if (supervisions.HasRcc)
                {
                    rbt.FkSupervisionsId = supervisions.SupervisionsId;
                    rbt.CreationDate = DateTime.Now;
                    rbt.LastUpdateDate = DateTime.Now;
                    _context.Add(rbt);
                    await _context.SaveChangesAsync();

                    supervisions.FkRbtCompetencies = rbt;
                }
                if (supervisions.HasBcabaSupvMeeting)
                {
                    bcaba.FkSupervisionsId = supervisions.SupervisionsId;
                    bcaba.CreationDate = DateTime.Now;
                    bcaba.LastUpdateDate = DateTime.Now;
                    _context.Add(bcaba);
                    await _context.SaveChangesAsync();

                    supervisions.FkBcabaSupvMeetings = bcaba;
                }
                _context.Update(supervisions);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            var userid = await _userManager.GetUserAsync(User);
            ViewBag.Modes = new SelectList(externalLists.SupervisionModes, supervisions.SupervisionMode);
            ViewBag.Characteristics = new SelectList(externalLists.SupervisionCharacteristics, supervisions.SupervisionCharacteristics);
            ViewBag.Ratings = new SelectList(externalLists.SupervisionRatings, supervisions.PerformanceRating);
            ViewBag.Supervisors = new SelectList(_context.Employees.Where(e => e.IsSupervisor == true).ToList(), "EmployeesId", "EmployeeLabel", supervisions.SupervisorName);
            ViewData["FkAppointmentsId"] = new SelectList(_context.Appointments.Where(a => a.FkEmployeesId == userid.FkEmployeesId), "AppointmentsId", "TimeSlot", supervisions.FkAppointmentsId);
            ViewData["FkRbtCompetenciesId"] = new SelectList(_context.RbtCompetencies, "RbtCompetenciesId", "RbtCompetenciesId", supervisions.FkRbtCompetenciesId);
            ViewData["Status"] = new SelectList(externalLists.SupervisionStatuses, supervisions.SupervisionStatus);
            return View(supervisions);
        }

        // GET: Supervisions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supervisions = await _context.Supervisions.FindAsync(id);
            if (supervisions == null)
            {
                return NotFound();
            }
            var userid = await _userManager.GetUserAsync(User);
            ViewBag.Modes = new SelectList(externalLists.SupervisionModes, supervisions.SupervisionMode);
            ViewBag.Characteristics = new SelectList(externalLists.SupervisionCharacteristics, supervisions.SupervisionCharacteristics);
            ViewBag.Ratings = new SelectList(externalLists.SupervisionRatings, supervisions.PerformanceRating);
            ViewBag.Supervisors = new SelectList(_context.Employees.Where(e => e.IsSupervisor == true).ToList(), "EmployeesId", "EmployeeLabel", supervisions.SupervisorName);
            ViewData["FkAppointmentsId"] = new SelectList(_context.Appointments.Where(a => a.FkEmployeesId == userid.FkEmployeesId), "AppointmentsId", "TimeSlot", supervisions.FkAppointmentsId);
            ViewData["FkRbtCompetenciesId"] = new SelectList(_context.RbtCompetencies, "RbtCompetenciesId", "RbtCompetenciesId", supervisions.FkRbtCompetenciesId);
            ViewData["Status"] = new SelectList(externalLists.SupervisionStatuses, supervisions.SupervisionStatus);

            return View(supervisions);
        }

        // POST: Supervisions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Supervisions supervisions)
        {
            if (id != supervisions.SupervisionsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    RbtCompetencies rbt = new RbtCompetencies();
                    BcabaSupvMeetings bcaba = new BcabaSupvMeetings();
                    if (supervisions.HasRcc)
                    {
                        rbt.FkSupervisionsId = supervisions.SupervisionsId;
                        rbt.CreationDate = DateTime.Now;
                        rbt.LastUpdateDate = DateTime.Now;
                        _context.Add(rbt);
                        await _context.SaveChangesAsync();

                        supervisions.FkRbtCompetencies = rbt;
                    }
                    if (supervisions.HasBcabaSupvMeeting)
                    {
                        bcaba.FkSupervisionsId = supervisions.SupervisionsId;
                        bcaba.CreationDate = DateTime.Now;
                        bcaba.LastUpdateDate = DateTime.Now;
                        _context.Add(bcaba);
                        await _context.SaveChangesAsync();

                        supervisions.FkBcabaSupvMeetings = bcaba;
                    }
                    _context.Update(supervisions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupervisionsExists(supervisions.SupervisionsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var userid = await _userManager.GetUserAsync(User);
            ViewBag.Modes = new SelectList(externalLists.SupervisionModes, supervisions.SupervisionMode);
            ViewBag.Characteristics = new SelectList(externalLists.SupervisionCharacteristics, supervisions.SupervisionCharacteristics);
            ViewBag.Ratings = new SelectList(externalLists.SupervisionRatings, supervisions.PerformanceRating);
            ViewBag.Supervisors = new SelectList(_context.Employees.Where(e => e.IsSupervisor == true).ToList(), "EmployeesId", "EmployeeLabel", supervisions.SupervisorName);
            ViewData["FkAppointmentsId"] = new SelectList(_context.Appointments.Where(a => a.FkEmployeesId == userid.FkEmployeesId), "AppointmentsId", "TimeSlot", supervisions.FkAppointmentsId);
            ViewData["FkRbtCompetenciesId"] = new SelectList(_context.RbtCompetencies, "RbtCompetenciesId", "RbtCompetenciesId", supervisions.FkRbtCompetenciesId);
            ViewData["Status"] = new SelectList(externalLists.SupervisionStatuses, supervisions.SupervisionStatus);
            return View(supervisions);
        }

        // GET: Supervisions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supervisions = await _context.Supervisions
                .Include(s => s.FkAppointments)
                .ThenInclude(s => s.FkClients)
                .Include(s => s.FkAppointments)
                .ThenInclude(s => s.FkEmployees)
                .FirstOrDefaultAsync(m => m.SupervisionsId == id);
            if (supervisions == null)
            {
                return NotFound();
            }

            return PartialView(supervisions);
        }

        // POST: Supervisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supervisions = await _context.Supervisions.FindAsync(id);
            _context.Supervisions.Remove(supervisions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupervisionsExists(int id)
        {
            return _context.Supervisions.Any(e => e.SupervisionsId == id);
        }

    }
}
