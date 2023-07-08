using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "ADMIN")]
    public class RoleNamesController : Controller
    {
        private readonly IdentityContext _context;

        public RoleNamesController(IdentityContext context)
        {
            _context = context;
        }

        [Route("Employee-Roles")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoleNames.ToListAsync());
        }

        // GET: RoleNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleNames = await _context.RoleNames
                .FirstOrDefaultAsync(m => m.RoleNamesId == id);
            if (roleNames == null)
            {
                return NotFound();
            }

            return View(roleNames);
        }

        // GET: RoleNames/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: RoleNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] RoleNames roleNames)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleNames);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", roleNames) });
        }

        // GET: RoleNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleNames = await _context.RoleNames.FindAsync(id);
            if (roleNames == null)
            {
                return NotFound();
            }
            return PartialView(roleNames);
        }

        // POST: RoleNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] RoleNames roleNames)
        {
            if (id != roleNames.RoleNamesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleNames);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleNamesExists(roleNames.RoleNamesId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", roleNames) });
        }

        // GET: RoleNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleNames = await _context.RoleNames
                .FirstOrDefaultAsync(m => m.RoleNamesId == id);
            if (roleNames == null)
            {
                return NotFound();
            }

            return PartialView(roleNames);
        }

        // POST: RoleNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleNames = await _context.RoleNames.FindAsync(id);
            _context.RoleNames.Remove(roleNames);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleNamesExists(int id)
        {
            return _context.RoleNames.Any(e => e.RoleNamesId == id);
        }
    }
}
