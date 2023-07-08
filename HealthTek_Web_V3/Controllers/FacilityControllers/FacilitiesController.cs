using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "ADMIN")]
    public class FacilitiesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public FacilitiesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Facilities
        public async Task<IActionResult> Index()
        {
            var identityContext = await _context.Facilities.Include(f => f.Locations).Where(m => m.Locations.LocationName.Contains("Office")).ToListAsync();
            return View(identityContext);
        }

        [Route("Facility/Profile/{id}/{table?}")]
        public async Task<IActionResult> Details(int? id, string? table)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilities = new Facilities();
            List<TabModel> model = new List<TabModel>();
            model.Add(new TabModel { Name = "Clients", Active = "active" });
            model.Add(new TabModel { Name = "Employees", Active = "" });
            model.Add(new TabModel { Name = "Batches", Active = "" });
            if (table == null || table == string.Empty)
            {
                table = "Clients";
            }
            switch (table)
            {
                case "Employees":
                    facilities = await _context.Facilities
                .Include(f => f.Locations)
                .Include(f => f.FacilitiesOperatingCounties)
                .ThenInclude(f => f.FkOperatingCounties)
                .Include(f => f.ClientsFacilities)
                .ThenInclude(f => f.FkClients)
                .Include(f => f.EmployeesFacilities)
                .ThenInclude(f => f.FkEmployees)
                .Include(f => f.EmployeesFacilities)
                .ThenInclude(f => f.FkEmployees)
                .ThenInclude(f => f.Locations)
                .FirstOrDefaultAsync(m => m.FacilitiesId == id);
                    var emp = facilities.EmployeesFacilities.Select(m => m.FkEmployees).ToList();
                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Employees").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Employees";
                    ViewData["Table"] = emp;
                    break;
                case "Clients":
                    facilities = await _context.Facilities
                .Include(f => f.Batches)
                .Include(f => f.Locations)
                .Include(f => f.FacilitiesOperatingCounties)
                .ThenInclude(f => f.FkOperatingCounties)
                .Include(f => f.EmployeesFacilities)
                .ThenInclude(f => f.FkEmployees)
                .Include(f => f.ClientsFacilities)
                .ThenInclude(f => f.FkClients)
                .Include(f => f.ClientsFacilities)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Authorizations)
                .ThenInclude(f => f.AuthorizationNotes)
                .Include(f => f.ClientsFacilities)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Locations)
                .FirstOrDefaultAsync(m => m.FacilitiesId == id);
                    var client = facilities.ClientsFacilities.Select(m => m.FkClients).ToList();
                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Clients").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Clients";
                    ViewData["Table"] = client;
                    break;
                case "Batches":
                    facilities = await _context.Facilities
                .Include(f => f.Locations)
                .Include(f => f.FacilitiesOperatingCounties)
                .ThenInclude(f => f.FkOperatingCounties)
                .Include(f => f.Batches)
                .FirstOrDefaultAsync(m => m.FacilitiesId == id);
                    var batches = facilities.Batches.ToList();
                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Batches").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Batches";
                    ViewData["Table"] = batches;
                    break;
            }
            if (facilities == null)
            {
                return NotFound();
            }
            var batch = facilities.Batches.Where(m => m != null).ToList();
            var thisMonth = batch.Where(m => m != null && m.BatchDate.Month == DateTime.Now.Month).ToList();
            var prevMonth = batch.Where(m => m != null && m.BatchDate.Month == DateTime.Now.AddMonths(-1).Month).ToList();
            decimal currentTotal = 0;
            foreach (var item in thisMonth)
            {
                currentTotal += (decimal)item.Total;
            }
            decimal prevTotal = 0;
            foreach (var item in prevMonth)
            {
                prevTotal += (decimal)item.Total;
            }
            ViewData["EmployeeTotal"] = facilities.EmployeesFacilities.Select(m => m.FkEmployees).Count<Employees>();
            ViewData["ClientTotal"] = facilities.ClientsFacilities.Select(m => m.FkClients).Count<Clients>();
            ViewData["Tabs"] = model;
            ViewData["ID"] = facilities.FacilitiesId;
            ViewData["CurrentMonth"] = currentTotal;
            ViewData["PrevMonth"] = prevTotal;
            return View(facilities);
        }

        // GET: Facilities/Create
        public IActionResult Create()
        {
            List<string> timeZone = new List<string>();
            Locations locations = new Locations();
            ExternalLists externalLists = new ExternalLists();
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                timeZone.Add(z.Id);
            }
            ViewData["TimeZones"] = new SelectList(timeZone);
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Countries"] = new SelectList(externalLists.Countries);
            ViewData["Types"] = new SelectList(externalLists.FacilityTypes);
            return PartialView();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Facilities facilities, [FromForm] Locations locations)
        {
            if (ModelState.IsValid)
            {
                var locationOffice = "Office";
                locations.LocationName = "Main Office";
                locations.PlaceOfService = locationOffice;
                _context.Locations.Add(locations);
                await _context.SaveChangesAsync();

                facilities.Locations = locations;
                facilities.FkLocationsId = locations.LocationsId;
                facilities.CreationDate = DateTime.Now;
                facilities.LastUpdateDate = DateTime.Now;
                _context.Facilities.Add(facilities);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            List<string> timeZone = new List<string>();
            ExternalLists externalLists = new ExternalLists();
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                timeZone.Add(z.Id);
            }
            ViewData["TimeZones"] = new SelectList(timeZone);
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Countries"] = new SelectList(externalLists.Countries);
            ViewData["Types"] = new SelectList(externalLists.FacilityTypes, facilities.FacilityType);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", facilities) });
        }

        // GET: Facilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilities = await _context.Facilities.Include(l => l.Locations).FirstOrDefaultAsync(i => i.FacilitiesId == id);
            if (facilities == null)
            {
                return NotFound();
            }
            List<string> timeZone = new List<string>();
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                timeZone.Add(z.Id);
            }
            ViewData["TimeZones"] = new SelectList(timeZone);
            ExternalLists externalLists = new ExternalLists();
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Countries"] = new SelectList(externalLists.Countries);
            ViewData["Types"] = new SelectList(externalLists.FacilityTypes, facilities.FacilityType);
            return PartialView(facilities);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Facilities facilities)
        {
            if (id != facilities.FacilitiesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    facilities.LastUpdateDate = DateTime.Now;
                    facilities.Locations.LastUpdateDate = DateTime.Now;
                    _context.Update(facilities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilitiesExists(facilities.FacilitiesId))
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
            List<string> timeZone = new List<string>();
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
            {
                timeZone.Add(z.Id);
            }
            ViewData["TimeZones"] = new SelectList(timeZone);
            ExternalLists externalLists = new ExternalLists();
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Countries"] = new SelectList(externalLists.Countries);
            ViewData["Types"] = new SelectList(externalLists.FacilityTypes, facilities.FacilityType);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", facilities) });
        }

        // GET: Facilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilities = await _context.Facilities
                .FirstOrDefaultAsync(m => m.FacilitiesId == id);
            if (facilities == null)
            {
                return NotFound();
            }

            return PartialView(facilities);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facilities = await _context.Facilities
                .Include(l => l.Locations)
                .Include(l => l.Appointments)
                .Include(l => l.Assignments)
                .Include(l => l.Authorizations)
                .Include(l => l.ClientsFacilities)
                .ThenInclude(l => l.FkClients)
                .Include(l => l.EmployeesFacilities)
                .ThenInclude(l => l.FkEmployees)
                .Include(l => l.FacilitiesOperatingCounties)
                .Include(l => l.Intakes)
                .FirstOrDefaultAsync(m => m.FacilitiesId == id);
            _context.Facilities.Remove(facilities);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool FacilitiesExists(int id)
        {
            return _context.Facilities.Any(e => e.FacilitiesId == id);
        }
    }
}
