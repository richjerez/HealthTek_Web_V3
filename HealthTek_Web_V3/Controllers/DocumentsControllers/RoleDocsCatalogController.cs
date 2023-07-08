using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "ADMIN")]
    public class RoleDocsCatalogController : Controller
    {
        private readonly ExternalLists _externalLists = new ExternalLists();
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _hostEnv;

        public RoleDocsCatalogController(IdentityContext context, UserManager<AppUser> userManager, IWebHostEnvironment hostEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostEnv = hostEnv;
        }

        [Route("HR-Requirements")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoleDocsCatalog.ToListAsync());
        }

        // GET: RoleDocsCatalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleDocsCatalog = await _context.RoleDocsCatalog
                .FirstOrDefaultAsync(m => m.RoleDocsCatalogId == id);
            if (roleDocsCatalog == null)
            {
                return NotFound();
            }

            return View(roleDocsCatalog);
        }

        // GET: RoleDocsCatalogs/Create
        public IActionResult Create()
        {
            ViewData["RoleNames"] = new SelectList(_context.Set<RoleNames>(), "RoleNamesId", "RoleName");
            ViewData["DocumentExpiration"] = new SelectList(_externalLists.DocumentExpiration);
            return PartialView();
        }

        // POST: RoleDocsCatalogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] RoleDocsCatalog roleDocsCatalog)
        {
            if (ModelState.IsValid)
            {
                if (roleDocsCatalog.Expiration == "Never Expires")
                {
                    roleDocsCatalog.NeverExpires = true;
                }
                roleDocsCatalog.CreationDate = DateTime.Now;
                roleDocsCatalog.LastUpdateDate = DateTime.Now;
                if (roleDocsCatalog.CustomFile != null)
                {
                    UploadFileHelper uploadFile = new UploadFileHelper(_hostEnv);
                    await uploadFile.UploadFileAsync(roleDocsCatalog.CustomFile, "unsorted", false);
                    roleDocsCatalog.TemplateUrl = "/files/templates/" + roleDocsCatalog.CustomFile.FileName;
                }
                // Add roles to Employee EmployeeRoleNames
                if (roleDocsCatalog.HrRoles != null)
                {
                    foreach (var item in roleDocsCatalog.HrRoles)
                    {
                        var roleCheck = _context.RoleNames.FirstOrDefault(m => m.RoleNamesId == item);
                        roleDocsCatalog.Roles = String.Join(",", roleCheck.RoleName);
                    }
                }

                _context.Add(roleDocsCatalog);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            var roles = new SelectList(_context.Set<RoleNames>(), "RoleNamesId", "RoleName");
            if (roleDocsCatalog.Roles != null)
            {
                foreach (var item in roles)
                {
                    if (roleDocsCatalog.Roles.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }
            }
            ViewData["RoleNames"] = roles;
            ViewData["DocumentExpiration"] = new SelectList(_externalLists.DocumentExpiration);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", roleDocsCatalog) });
        }

        // GET: RoleDocsCatalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleDocsCatalog = await _context.RoleDocsCatalog.FindAsync(id);
            if (roleDocsCatalog == null)
            {
                return NotFound();
            }
            var roles = new SelectList(_context.Set<RoleNames>(), "RoleNamesId", "RoleName");
            if (roleDocsCatalog.Roles != null)
            {
                foreach (var item in roles)
                {
                    if (roleDocsCatalog.Roles.Contains(item.Text))
                    {
                        item.Selected = true;
                    }
                }

            }
            ViewData["RoleNames"] = roles;
            ViewData["DocumentExpiration"] = new SelectList(_externalLists.DocumentExpiration);
            return PartialView(roleDocsCatalog);
        }

        // POST: RoleDocsCatalogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] RoleDocsCatalog roleDocsCatalog)
        {
            if (id != roleDocsCatalog.RoleDocsCatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (roleDocsCatalog.Expiration == "Never Expires")
                    {
                        roleDocsCatalog.NeverExpires = true;
                    }
                    // Add roles to Employee EmployeeRoleNames
                    if (roleDocsCatalog.HrRoles != null)
                    {
                        foreach (var item in roleDocsCatalog.HrRoles)
                        {
                            var roleCheck = _context.RoleNames.FirstOrDefault(m => m.RoleNamesId == item);
                            roleDocsCatalog.Roles += roleCheck.RoleName + ", ";
                        }
                    }

                    roleDocsCatalog.LastUpdateDate = DateTime.Now;
                    _context.Update(roleDocsCatalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleDocsCatalogExists(roleDocsCatalog.RoleDocsCatalogId))
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
            var roles = new SelectList(_context.Set<RoleNames>(), "RoleNamesId", "RoleName");
            foreach (var item in roles)
            {
                if (roleDocsCatalog.Roles.Contains(item.Value))
                {
                    item.Selected = true;
                }
            }
            ViewData["RoleNames"] = roles;
            ViewData["DocumentExpiration"] = new SelectList(_externalLists.DocumentExpiration);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", roleDocsCatalog) });
        }

        // GET: RoleDocsCatalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleDocsCatalog = await _context.RoleDocsCatalog
                .FirstOrDefaultAsync(m => m.RoleDocsCatalogId == id);
            if (roleDocsCatalog == null)
            {
                return NotFound();
            }

            return PartialView(roleDocsCatalog);
        }

        // POST: RoleDocsCatalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleDocsCatalog = await _context.RoleDocsCatalog.FindAsync(id);
            _context.RoleDocsCatalog.Remove(roleDocsCatalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleDocsCatalogExists(int id)
        {
            return _context.RoleDocsCatalog.Any(e => e.RoleDocsCatalogId == id);
        }
    }
}
