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
    public class CaregiversController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public CaregiversController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Caregivers
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.Caregivers.Include(c => c.FkClients).Include(c => c.FkEsignatures);
            return View(await identityContext.ToListAsync());
        }

        // GET: Caregivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregivers = await _context.Caregivers
                .Include(c => c.FkClients)
                .Include(c => c.FkEsignatures)
                .FirstOrDefaultAsync(m => m.CaregiversId == id);
            if (caregivers == null)
            {
                return NotFound();
            }

            return View(caregivers);
        }

        // GET: Caregivers/Create
        public IActionResult Create(int id)
        {
            ViewData["FkClientsId"] = id;
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr");
            return PartialView();
        }

        // POST: Caregivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Caregivers caregivers)
        {
            if (ModelState.IsValid)
            {
                _context.Caregivers.Add(caregivers);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkClientsId"] = caregivers.FkClientsId;
            ViewData["States"] = new SelectList(externalLists.States, caregivers.Locations.State);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities, caregivers.Locations.City);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", caregivers.Locations.County);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", caregivers) });
        }

        // GET: Caregivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregivers = _context.Caregivers.Include(m => m.Locations).FirstOrDefault(m => m.CaregiversId == id);
            if (caregivers == null)
            {
                return NotFound();
            }
            ViewData["FkClientsId"] = caregivers.FkClientsId;
            if (caregivers.Locations != null)
            {
                ViewData["States"] = new SelectList(externalLists.States, caregivers.Locations.State);
                ViewData["Cities"] = new SelectList(externalLists.FloridaCities, caregivers.Locations.City);
                ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", caregivers.Locations.County);
            }
            else
            {
                ViewData["States"] = new SelectList(externalLists.States);
                ViewData["Cities"] = new SelectList(externalLists.FloridaCities);
                ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr");
            }
            return PartialView(caregivers);
        }

        // POST: Caregivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Caregivers caregivers)
        {
            if (id != caregivers.CaregiversId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Caregivers.Update(caregivers);
                    await _context.SaveChangesAsync();
                    return Redirect(Request.Headers["Referer"].ToString());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaregiversExists(caregivers.CaregiversId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            ViewData["FkClientsId"] = caregivers.FkClientsId;
            ViewData["States"] = new SelectList(externalLists.States, caregivers.Locations.State);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities, caregivers.Locations.City);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", caregivers.Locations.County);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", caregivers) });
        }

        // GET: Caregivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caregivers = await _context.Caregivers
                .Include(c => c.FkClients)
                .FirstOrDefaultAsync(m => m.CaregiversId == id);
            if (caregivers == null)
            {
                return NotFound();
            }

            return PartialView(caregivers);
        }

        // POST: Caregivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caregivers = await _context.Caregivers.FindAsync(id);
            _context.Caregivers.Remove(caregivers);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Clients", new { id = caregivers.FkClientsId });
        }

        private bool CaregiversExists(int id)
        {
            return _context.Caregivers.Any(e => e.CaregiversId == id);
        }
    }
}
