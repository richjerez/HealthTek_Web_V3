using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.FacilityControllers
{
    [Authorize]
    public class FacilitiesOperatingCountiesController : Controller
    {
        private readonly IdentityContext _context;

        public FacilitiesOperatingCountiesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: FacilitiesOperatingCounties/Create
        public IActionResult Create(int id)
        {
            FacilitiesOperatingCounties operatingCounties = new FacilitiesOperatingCounties();
            operatingCounties.FkFacilitiesId = id;
            ViewData["FkOperatingCountiesId"] = new SelectList(_context.OperatingCounties, "OperatingCountiesId", "County");
            return PartialView(operatingCounties);
        }

        // POST: FacilitiesOperatingCounties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] FacilitiesOperatingCounties facilitiesOperatingCounties)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facilitiesOperatingCounties);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkOperatingCountiesId"] = new SelectList(_context.OperatingCounties, "OperatingCountiesId", "County", facilitiesOperatingCounties.FkOperatingCountiesId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", facilitiesOperatingCounties) });
        }

        // GET: FacilitiesOperatingCounties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilitiesOperatingCounties = await _context.FacilitiesOperatingCounties.FindAsync(id);
            if (facilitiesOperatingCounties == null)
            {
                return NotFound();
            }
            ViewData["FkOperatingCountiesId"] = new SelectList(_context.OperatingCounties, "OperatingCountiesId", "County", facilitiesOperatingCounties.FkOperatingCountiesId);
            return PartialView(facilitiesOperatingCounties);
        }

        // POST: FacilitiesOperatingCounties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] FacilitiesOperatingCounties facilitiesOperatingCounties)
        {
            if (id != facilitiesOperatingCounties.FacilitiesOperatingCountiesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facilitiesOperatingCounties);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilitiesOperatingCountiesExists(facilitiesOperatingCounties.FacilitiesOperatingCountiesId))
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
            ViewData["FkOperatingCountiesId"] = new SelectList(_context.OperatingCounties, "OperatingCountiesId", "County", facilitiesOperatingCounties.FkOperatingCountiesId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", facilitiesOperatingCounties) });
        }

        private bool FacilitiesOperatingCountiesExists(int id)
        {
            return _context.FacilitiesOperatingCounties.Any(e => e.FacilitiesOperatingCountiesId == id);
        }
    }
}
