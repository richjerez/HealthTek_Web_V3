using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class LocationsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public LocationsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Locations/Create
        public IActionResult Create(int id)
        {
            ViewData["FkCaregiversId"] = new SelectList(_context.Caregivers, "CaregiversId", "CaregiversId");
            ViewData["FkEmployeesId"] = new SelectList(_context.Employees, "EmployeesId", "EmployeesId");
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilitiesId");
            ViewData["FkClientsId"] = id;
            ViewData["Types"] = new SelectList(externalLists.PlaceOfServices, "Key", "Value");
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr");

            return PartialView();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Locations locations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(locations);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkCaregiversId"] = new SelectList(_context.Caregivers, "CaregiversId", "CaregiversId", locations.FkCaregiversId);
            ViewData["FkEmployeesId"] = new SelectList(_context.Employees, "EmployeesId", "EmployeesId", locations.FkEmployeesId);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilitiesId", locations.FkFacilitiesId);
            ViewData["FkClientsId"] = locations.FkClientsId;
            ViewData["Types"] = new SelectList(externalLists.PlaceOfServices, "Key", "Value", locations.PlaceOfService);
            ViewData["States"] = new SelectList(externalLists.States, locations.State);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities, locations.City);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", locations.County);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", locations) });
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locations = await _context.Locations.FindAsync(id);
            if (locations == null)
            {
                return NotFound();
            }
            ViewData["FkCaregiversId"] = new SelectList(_context.Caregivers, "CaregiversId", "CaregiversId", locations.FkCaregiversId);
            ViewData["FkEmployeesId"] = new SelectList(_context.Employees, "EmployeesId", "EmployeesId", locations.FkEmployeesId);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilitiesId", locations.FkFacilitiesId);
            ViewData["FkClientsId"] = locations.FkClientsId;
            ViewData["Types"] = new SelectList(externalLists.PlaceOfServices, "Key", "Value", locations.PlaceOfService);
            ViewData["States"] = new SelectList(externalLists.States, locations.State);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities, locations.City);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", locations.County);
            return PartialView(locations);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Locations locations)
        {
            if (id != locations.LocationsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationsExists(locations.LocationsId))
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
            ViewData["FkCaregiversId"] = new SelectList(_context.Caregivers, "CaregiversId", "CaregiversId", locations.FkCaregiversId);
            ViewData["FkEmployeesId"] = new SelectList(_context.Employees, "EmployeesId", "EmployeesId", locations.FkEmployeesId);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilitiesId", locations.FkFacilitiesId);
            ViewData["FkClientsId"] = locations.FkClientsId;
            ViewData["Types"] = new SelectList(externalLists.PlaceOfServices, "Key", "Value", locations.PlaceOfService);
            ViewData["States"] = new SelectList(externalLists.States, locations.State);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities, locations.City);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", locations.County);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", locations) });
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locations = await _context.Locations
                .Include(l => l.FkClients)
                .FirstOrDefaultAsync(m => m.LocationsId == id);
            if (locations == null)
            {
                return NotFound();
            }

            return PartialView(locations);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locations = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(locations);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool LocationsExists(int id)
        {
            return _context.Locations.Any(e => e.LocationsId == id);
        }
    }
}
