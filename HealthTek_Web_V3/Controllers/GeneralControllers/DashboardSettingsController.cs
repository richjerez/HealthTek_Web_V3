using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class DashboardSettingsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ExternalLists externalLists = new ExternalLists();

        public DashboardSettingsController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Dashboards
        public async Task<IActionResult> Index()
        {
            // get current user
            var user = await _userManager.GetUserAsync(User);
            var dashboards = await _context.Dashboards
                .Where(u => u.FkUserId == user.Id)
                .Include(m => m.FkDashboardWidgets)
                .ThenInclude(m => m.FkWidget)
                .ToListAsync();
            return View(dashboards);
        }

        // GET: Dashboards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dashboards = await _context.Dashboards
                .FirstOrDefaultAsync(m => m.DashboardId == id);
            if (dashboards == null)
            {
                return NotFound();
            }
            return View(dashboards);
        }

        // GET: Dashboards/Create
        public IActionResult Create() => PartialView();

        // POST: Dashboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Dashboards dashboards)
        {
            if (ModelState.ContainsKey("{Widgets}"))
                ModelState["{Widgets}"].Errors.Clear();
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                dashboards.FkUserId = user.Id;
                dashboards.LastUpdateDate = DateTime.Now;
                _context.Dashboards.Add(dashboards);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", dashboards) });
        }

        // GET: Dashboards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboards = await _context.Dashboards
                .Include(m => m.FkDashboardWidgets)
                .FirstOrDefaultAsync(m => m.DashboardId == id);
            if (dashboards == null)
            {
                return NotFound();
            }
            var widgets = new SelectList(_context.Set<Widgets>(), "WidgetId", "WidgetName");
            foreach (var item in widgets)
            {
                foreach (var widget in dashboards.FkDashboardWidgets)
                {
                    if (widget.FkWidgetId.ToString() == item.Value)
                    {
                        item.Selected = true;
                    }

                }
            }
            ViewData["Widgets"] = widgets;
            return PartialView(dashboards);
        }

        // POST: Dashboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Dashboards dashboards)
        {
            if (id != dashboards.DashboardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dash = _context.Dashboards.Where(d => d.DashboardId == id).Include(m => m.FkDashboardWidgets).AsNoTracking().FirstOrDefault();
                    var widgetCheck = dash.FkDashboardWidgets;
                    _context.DashboardWidgets.RemoveRange(widgetCheck);
                    _context.Entry(dash).State = EntityState.Detached;

                    foreach (var i in dashboards.Widgets)
                    {
                        DashboardWidgets widgets = new DashboardWidgets();
                        widgets.DashboardWidgetId = 0;
                        widgets.FkDashboardId = dashboards.DashboardId;
                        widgets.FkWidgetId = i;
                        widgets.LastUpdateDate = DateTime.Now;
                        _context.DashboardWidgets.Update(widgets);
                        await _context.SaveChangesAsync();
                    }
                    var user = await _userManager.GetUserAsync(User);
                    dashboards.FkUserId = user.Id;
                    dashboards.LastUpdateDate = DateTime.Now;
                    _context.Dashboards.Update(dashboards);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DashboardsExists(dashboards.DashboardId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", dashboards) });
        }

        // GET: Dashboards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboards = await _context.Dashboards
                .FirstOrDefaultAsync(m => m.DashboardId == id);
            if (dashboards == null)
            {
                return NotFound();
            }

            return PartialView(dashboards);
        }

        // POST: Dashboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dashboards = await _context.Dashboards.FindAsync(id);
            _context.Dashboards.Remove(dashboards);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DashboardsExists(int id)
        {
            return _context.Dashboards.Any(e => e.DashboardId == id);
        }
    }
}
