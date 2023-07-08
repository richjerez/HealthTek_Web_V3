using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
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
    [Authorize(Policy = "AppointmentViews")]
    public class AppointmentsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ExternalLists externalLists = new ExternalLists();

        public AppointmentsController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var identityContext = await _context.Appointments
                .Where(m => m.FkEmployeesId == user.FkEmployeesId)
                .Include(a => a.FkClients)
                .Include(a => a.FkEmployees)
                .Include(a => a.FkBatches)
                .Include(a => a.FkEndLocation)
                .Include(a => a.FkFacilities)
                .Include(a => a.FkServiceCodes)
                .Include(a => a.FkStartLocation)
                .ToListAsync();
            List<CalendarModel> Calendar = new List<CalendarModel>();
            foreach (var item in identityContext)
            {
                var serviceTitle = "";
                if (item.AppointmentType == "Services")
                {
                    serviceTitle = "Service: " + item.FkServiceCodes.CodeTitle +
                        "\nClient: " + item.FkClients.FullName + "\nStart: " + item.FkStartLocation.LocationName +
                        "\nEnd: " + item.FkEndLocation.LocationName;
                }
                Calendar.Add(new CalendarModel
                {
                    id = item.AppointmentsId,
                    title = item.AppointmentType + "\n" + serviceTitle + "\n" + item.Description,
                    start = item.StartTime,
                    end = item.EndTime,
                    allDay = false,
                    className = item.ClassName,
                });
            }
            return View(Calendar);
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .Include(a => a.FkClients)
                .Include(a => a.FkEmployees)
                .Include(a => a.FkEndLocation)
                .Include(a => a.FkFacilities)
                .Include(a => a.FkBatches)
                .Include(a => a.FkServiceCodes)
                .Include(a => a.FkStartLocation)
                .FirstOrDefaultAsync(m => m.AppointmentsId == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // GET: Appointments/Create
        public IActionResult Create(int? id)
        {
            ViewData["FkBatchesId"] = new SelectList(_context.Set<Batches>(), "BatchesId", "BatchNumber");
            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName");
            ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "FullName");
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName");
            ViewData["FkServiceCodesId"] = new SelectList(_context.Set<ServiceCodes>(), "ServiceCodesId", "CodeTitle");
            ViewData["Types"] = new SelectList(externalLists.AppointmentTypes);
            if (id != null)
            {
                ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", id);
                ViewData["Types"] = new SelectList(externalLists.AppointmentTypes, "Services");
            }
            return PartialView();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Appointments appointments, string style)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var start = "Draft";
                switch (style)
                {
                    case "primary":
                        appointments.ClassName = "bg-primary text-white";
                        break;
                    case "info":
                        appointments.ClassName = "bg-info text-white";
                        break;
                    case "warning":
                        appointments.ClassName = "bg-warning text-white";
                        break;
                    case "danger":
                        appointments.ClassName = "bg-danger text-white";
                        break;
                    case "success":
                        appointments.ClassName = "bg-success text-white";
                        break;
                }
                appointments.FkEmployeesId = user.FkEmployeesId;
                appointments.QaStatus = start;
                _context.Appointments.Add(appointments);
                await _context.SaveChangesAsync();
                var code = _context.ServiceCodes.Find(appointments.FkServiceCodesId).CodeTitle;
                switch (code)
                {
                    case "BA Assessment":
                        BaAssessments assessments = new BaAssessments();
                        assessments.FkAppointmentsId = appointments.AppointmentsId;
                        assessments.CreationDate = DateTime.Now;
                        assessments.LastUpdateDate = DateTime.Now;
                        _context.BaAssessments.Add(assessments);
                        await _context.SaveChangesAsync();
                        appointments.FkBaAssessmentsId = assessments.BaAssessmentsId;
                        appointments.FkBaProgressNotesId = null;

                        BaMonthlyReports monthlyReports = new BaMonthlyReports();
                        monthlyReports.FkAppointmentsId = appointments.AppointmentsId;
                        monthlyReports.FkBaAssessmentsId = assessments.BaAssessmentsId;
                        monthlyReports.CreationDate = DateTime.Now;
                        monthlyReports.LastUpdateDate = DateTime.Now;
                        _context.BaMonthlyReports.Add(monthlyReports);
                        await _context.SaveChangesAsync();
                        appointments.FkBaMonthlyReportsId = monthlyReports.BaMonthlyReportsId;

                        break;
                    case "BA Note ABA":
                    case "BA Note Grp":
                    case "BA Note LA":
                    case "BA Note RBT":
                        BaProgressNotes progressNote = new BaProgressNotes();
                        progressNote.FkAppointmentsId = appointments.AppointmentsId;
                        progressNote.CreationDate = DateTime.Now;
                        progressNote.LastUpdateDate = DateTime.Now;
                        _context.BaProgressNotes.Add(progressNote);
                        await _context.SaveChangesAsync();
                        appointments.FkBaAssessmentsId = null;
                        appointments.FkBaMonthlyReportsId = null;
                        appointments.FkBaProgressNotesId = progressNote.BaProgressNotesId;
                        break;

                    case "BA Reassessment":
                        var assessment = await _context.BaAssessments.Include(a => a.FkAppointments).Where(m => m.FkAppointments.FkClientsId == appointments.FkClientsId).OrderBy(m => m.FkAppointments.StartTime).LastOrDefaultAsync();
                        BaReassessments reassessments = new BaReassessments();
                        reassessments.CreationDate = DateTime.Now;
                        reassessments.LastUpdateDate = DateTime.Now;
                        reassessments.FkBaInitialAssessmentsId = assessment.BaAssessmentsId;
                        _context.BaReassessments.Add(reassessments);
                        await _context.SaveChangesAsync();
                        appointments.FkBaAssessmentsId = null;
                        appointments.FkBaMonthlyReportsId = null;
                        appointments.FkBaProgressNotesId = null;
                        appointments.FkBaReAssessmentsId = reassessments.BaReassessmentsId;
                        break;

                    case "CFARS":
                        Cfars cfars = new Cfars();
                        cfars.FkAppointmentsId = appointments.AppointmentsId;
                        cfars.CreationDate = DateTime.Now;
                        cfars.LastUpdateDate = DateTime.Now;
                        _context.Cfars.Add(cfars);
                        await _context.SaveChangesAsync();
                        appointments.FkCfarsId = cfars.CfarsId;
                        break;

                }
                _context.Appointments.Update(appointments);
                await _context.SaveChangesAsync();
                // Check employee batch for this month
                // if one doesnt exist Create new batch and add appointment
                // if it does add appointment to batch

                var batches = _context.Batches.Where(m => m.FkEmployeesId == user.FkEmployeesId && m.BatchDate.Month == DateTime.Now.Month).FirstOrDefault();
                if (batches != null)
                {
                    batches.Total += appointments.FkServiceCodes.CodeRate;
                    batches.Appointments.Add(appointments);
                    _context.Batches.Update(batches);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    Batches batch = new Batches();
                    batch.BatchDate = DateTime.Now;
                    batch.CreationDate = DateTime.Now;
                    batch.LastUpdateDate = DateTime.Now;
                    batch.FkEmployeesId = user.FkEmployeesId;
                    batch.FkFacilitiesId = appointments.FkFacilitiesId;
                    batch.BatchNumber = Guid.NewGuid().ToString("N").Substring(0, 6);
                    batch.Total = appointments.FkServiceCodes.CodeRate;
                    _context.Batches.Add(batch);
                    await _context.SaveChangesAsync();
                    batch.Appointments.Add(appointments);
                    await _context.SaveChangesAsync();
                }
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkBatchesId"] = new SelectList(_context.Set<Batches>(), "BatchesId", "BatchNumber", appointments.FkBatchesId);
            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", appointments.FkClientsId);
            ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "FullName", appointments.FkEmployeesId);
            ViewData["FkEndLocationId"] = new SelectList(_context.Set<Locations>(), "LocationsId", "FullLocation", appointments.FkEndLocationId);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", appointments.FkFacilitiesId);
            ViewData["FkServiceCodesId"] = new SelectList(_context.Set<ServiceCodes>(), "ServiceCodesId", "CodeTitle", appointments.FkServiceCodesId);
            ViewData["FkStartLocationId"] = new SelectList(_context.Set<Locations>(), "LocationsId", "FullLocation", appointments.FkStartLocationId);
            ViewData["Types"] = new SelectList(externalLists.AppointmentTypes, appointments.AppointmentType);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", appointments) });
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments.FindAsync(id);
            if (appointments == null)
            {
                return NotFound();
            }
            ViewData["FkBatchesId"] = new SelectList(_context.Set<Batches>(), "BatchesId", "BatchNumber", appointments.FkBatchesId);
            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", appointments.FkClientsId);
            ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "FullName", appointments.FkEmployeesId);
            ViewData["FkEndLocationId"] = new SelectList(_context.Set<Locations>().Where(m => m.LocationsId == appointments.FkEndLocationId), "LocationsId", "FullLocation", appointments.FkEndLocationId);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", appointments.FkFacilitiesId);
            ViewData["FkServiceCodesId"] = new SelectList(_context.Set<ServiceCodes>(), "ServiceCodesId", "CodeTitle", appointments.FkServiceCodesId);
            ViewData["FkStartLocationId"] = new SelectList(_context.Set<Locations>().Where(m => m.LocationsId == appointments.FkStartLocationId), "LocationsId", "FullLocation", appointments.FkStartLocationId);
            ViewData["Types"] = new SelectList(externalLists.AppointmentTypes, appointments.AppointmentType);
            return PartialView(appointments);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Appointments appointments, string style)
        {
            if (id != appointments.AppointmentsId)
            {
                return NotFound();
            }
            switch (style)
            {
                case "primary":
                    appointments.ClassName = "bg-primary text-white";
                    break;
                case "info":
                    appointments.ClassName = "bg-info text-white";
                    break;
                case "warning":
                    appointments.ClassName = "bg-warning text-white";
                    break;
                case "danger":
                    appointments.ClassName = "bg-danger text-white";
                    break;
                case "success":
                    appointments.ClassName = "bg-success text-white";
                    break;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appointments.LastUpdateDate = DateTime.Now;
                    _context.Update(appointments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentsExists(appointments.AppointmentsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkBatchesId"] = new SelectList(_context.Set<Batches>(), "BatchesId", "BatchNumber", appointments.FkBatchesId);
            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", appointments.FkClientsId);
            ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "FullName", appointments.FkEmployeesId);
            ViewData["FkEndLocationId"] = new SelectList(_context.Set<Locations>().Where(m => m.LocationsId == appointments.FkEndLocationId), "LocationsId", "FullLocation", appointments.FkEndLocationId);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", appointments.FkFacilitiesId);
            ViewData["FkServiceCodesId"] = new SelectList(_context.Set<ServiceCodes>(), "ServiceCodesId", "CodeTitle", appointments.FkServiceCodesId);
            ViewData["FkStartLocationId"] = new SelectList(_context.Set<Locations>().Where(m => m.LocationsId == appointments.FkStartLocationId), "LocationsId", "FullLocation", appointments.FkStartLocationId);
            ViewData["Types"] = new SelectList(externalLists.AppointmentTypes, appointments.AppointmentType);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", appointments) });
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointments = await _context.Appointments
                .Include(a => a.FkClients)
                .FirstOrDefaultAsync(m => m.AppointmentsId == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return PartialView(appointments);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointments = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentsExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentsId == id);
        }

        /// <summary>
        /// Gets the clients locations
        /// </summary>
        /// <param name="patId"></param>
        /// <returns>Client Locations</returns>
        public JsonResult GetLocation(int patId)
        {
            return Json(new SelectList(_context.Set<Locations>().Where(l => l.FkClientsId == patId), "LocationsId", "FullLocation"));
        }

        public async Task<JsonResult> UpdateDates(int id, string date, string flag)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            var newDate = DateTime.Parse(date);
            switch (flag)
            {
                case "start":
                    appointment.StartTime = newDate;
                    break;
                case "end":
                    appointment.EndTime = newDate;
                    break;
            }
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return Json(new { data = "ok" });
        }
    }
}
