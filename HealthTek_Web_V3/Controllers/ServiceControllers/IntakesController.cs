using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "IntakeViews")]
    public class IntakesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public IntakesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Intakes
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.Intakes.Include(i => i.FkClients).ThenInclude(i => i.ClientsFacilities).Include(i => i.FkFacilities);

            ViewData["Status"] = new SelectList(externalLists.IntakeStatuses);
            return View(await identityContext.ToListAsync());
        }

        // GET: Intakes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intakes = await _context.Intakes
                .Include(i => i.IntakeDocumentation)
                .Include(i => i.FkClients)
                .ThenInclude(i => i.ClientsFacilities)
                .Include(i => i.FkFacilities)
                .FirstOrDefaultAsync(m => m.IntakesId == id);
            if (intakes == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(externalLists.IntakeStatuses);
            ViewData["DocName"] = new SelectList(_context.Set<IntakeDocsCatalog>(), "IntakeDocName", "IntakeDocName");
            ViewData["Types"] = new SelectList(externalLists.DocumentTypes);

            return PartialView(intakes);
        }

        // GET: Intakes/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                ViewData["FkClientsId"] = new SelectList(_context.Clients, "ClientsId", "FullName");
            }
            else
            {
                ViewData["FkClientsId"] = new SelectList(_context.Clients, "ClientsId", "FullName", id);
            }
            ViewData["Facilities"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName");
            ViewData["Status"] = new SelectList(externalLists.IntakeStatuses);
            return PartialView();
        }

        // POST: Intakes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Intakes intakes)
        {
            if (ModelState.IsValid)
            {
                intakes.CreationDate = DateTime.Now;
                intakes.LastUpdateDate = DateTime.Now;

                _context.Intakes.Add(intakes);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkClientsId"] = new SelectList(_context.Clients, "ClientsId", "FullName", intakes.FkClientsId);
            ViewData["Facilities"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", intakes.FkFacilitiesId);
            ViewData["Status"] = new SelectList(externalLists.IntakeStatuses, intakes.IntakeStatus);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", intakes) });
        }

        // GET: Intakes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intakes = await _context.Intakes.FindAsync(id);
            if (intakes == null)
            {
                return NotFound();
            }
            ViewData["FkClientsId"] = new SelectList(_context.Clients, "ClientsId", "FullName", intakes.FkClientsId);
            ViewData["Facilities"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", intakes.FkFacilitiesId);
            ViewData["Status"] = new SelectList(externalLists.IntakeStatuses, intakes.IntakeStatus);
            return PartialView(intakes);
        }

        // POST: Intakes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Intakes intakes)
        {
            if (id != intakes.IntakesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intakes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IntakesExists(intakes.IntakesId))
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
            ViewData["FkClientsId"] = new SelectList(_context.Clients, "ClientsId", "FullName", intakes.FkClientsId);
            ViewData["Facilities"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", intakes.FkFacilitiesId);
            ViewData["Status"] = new SelectList(externalLists.IntakeStatuses, intakes.IntakeStatus);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", intakes) });
        }

        // GET: Intakes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intakes = await _context.Intakes
                .Include(i => i.FkClients)
                .FirstOrDefaultAsync(m => m.IntakesId == id);
            if (intakes == null)
            {
                return NotFound();
            }

            return PartialView(intakes);
        }

        // POST: Intakes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intakes = await _context.Intakes.FindAsync(id);
            _context.Intakes.Remove(intakes);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool IntakesExists(int id)
        {
            return _context.Intakes.Any(e => e.IntakesId == id);
        }

        public async Task<JsonResult> UpdateIntake(int id, string status)
        {
            var intake = await _context.Intakes.FindAsync(id);
            intake.IntakeStatus = status;
            _context.Intakes.Update(intake);
            await _context.SaveChangesAsync();
            return Json(new { data = "ok" });
        }

    }
}
