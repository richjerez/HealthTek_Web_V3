using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "ADMIN")]
    public class UserRolesController : Controller
    {
        private readonly IdentityContext _context;

        // Class Constructor
        public UserRolesController(IdentityContext context)
        {
            _context = context;
        }

        [Route("User-Roles")]
        public async Task<IActionResult> Index() => View(await _context.Roles.Where(m => m.NormalizedName != "SUPERUSER").ToListAsync());

        // GET: AppRoless/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AppRoles = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (AppRoles == null)
            {
                return NotFound();
            }

            return View(AppRoles);
        }

        // GET: AppRoless/Create
        public IActionResult Create() => PartialView();

        // POST: AppRoless/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserRoles appRoles)
        {
            if (ModelState.IsValid)
            {
                appRoles.Name = appRoles.Name.Trim().ToUpper();
                appRoles.NormalizedName = appRoles.Name.Normalize();
                _context.Add(appRoles);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", appRoles) });
        }

        // GET: AppRoless/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AppRoles = await _context.Roles.FindAsync(id);
            if (AppRoles == null)
            {
                return NotFound();
            }
            return PartialView(AppRoles);
        }

        // POST: AppRoless/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromForm] UserRoles appRoles)
        {
            if (id != appRoles.Id)
            {
                return NotFound();
            }
            appRoles.NormalizedName = appRoles.Name.Normalize();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appRoles);
                    _context.Entry(appRoles).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppRolesExists(appRoles.Id))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", appRoles) });
        }

        // GET: AppRoless/Delete/5
        public async Task<IActionResult> Delete(string id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.alert = "warning";
            }
            var AppRoles = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (AppRoles == null)
            {
                return NotFound();
            }

            return PartialView(AppRoles);
        }

        // POST: AppRoless/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var AppRoles = await _context.Roles
                 .FirstOrDefaultAsync(m => m.Id == id);
            _context.Remove(AppRoles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppRolesExists(string id) => _context.Roles.Any(e => e.Id == id);
    }
}
