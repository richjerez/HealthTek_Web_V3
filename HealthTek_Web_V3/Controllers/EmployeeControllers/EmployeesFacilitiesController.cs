using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.EmployeeControllers
{
    [Authorize]
    public class EmployeesFacilitiesController : Controller
    {
        private readonly IdentityContext _context;

        public EmployeesFacilitiesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: EmployeesFacilities/Create
        public IActionResult Create(string id)
        {
            EmployeesFacilities employeesFacilities = new EmployeesFacilities();
            employeesFacilities.FkEmployeesId = id;
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilityName");
            return PartialView(employeesFacilities);
        }

        // POST: EmployeesFacilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EmployeesFacilities employeesFacilities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeesFacilities);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilityName", employeesFacilities.FkFacilitiesId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", employeesFacilities) });
        }

        // GET: EmployeesFacilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesFacilities = await _context.EmployeesFacilities.FindAsync(id);
            if (employeesFacilities == null)
            {
                return NotFound();
            }
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilityName", employeesFacilities.FkFacilitiesId);
            return PartialView(employeesFacilities);
        }

        // POST: EmployeesFacilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] EmployeesFacilities employeesFacilities)
        {
            if (id != employeesFacilities.EmployeesFacilitiesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeesFacilities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesFacilitiesExists(employeesFacilities.EmployeesFacilitiesId))
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
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilityName", employeesFacilities.FkFacilitiesId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", employeesFacilities) });
        }
        private bool EmployeesFacilitiesExists(int id)
        {
            return _context.EmployeesFacilities.Any(e => e.EmployeesFacilitiesId == id);
        }
    }
}
