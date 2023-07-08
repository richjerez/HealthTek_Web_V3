using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.GeneralControllers
{
    [Authorize(Policy = "SUPERUSER")]
    public class WidgetsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public WidgetsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Widgets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Widgets.ToListAsync());
        }

        // GET: Widgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var widgets = await _context.Widgets
                .FirstOrDefaultAsync(m => m.WidgetId == id);
            if (widgets == null)
            {
                return NotFound();
            }

            return PartialView(widgets);
        }

        // GET: Widgets/Create
        public IActionResult Create()
        {
            ViewData["WidgetViews"] = new SelectList(externalLists.WidgetViews);
            ViewData["WidgetStyles"] = new SelectList(externalLists.WidgetStyles);
            return PartialView();
        }

        // POST: Widgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Widgets widgets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(widgets);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["WidgetViews"] = new SelectList(externalLists.WidgetViews, widgets.ViewName);
            ViewData["WidgetStyles"] = new SelectList(externalLists.WidgetStyles, widgets.Style);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", widgets) });
        }

        // GET: Widgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var widgets = await _context.Widgets.FindAsync(id);
            if (widgets == null)
            {
                return NotFound();
            }
            ViewData["WidgetViews"] = new SelectList(externalLists.WidgetViews);
            ViewData["WidgetStyles"] = new SelectList(externalLists.WidgetStyles);
            return PartialView(widgets);
        }

        // POST: Widgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Widgets widgets)
        {
            if (id != widgets.WidgetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(widgets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WidgetsExists(widgets.WidgetId))
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
            ViewData["WidgetViews"] = new SelectList(externalLists.WidgetViews, widgets.ViewName);
            ViewData["WidgetStyles"] = new SelectList(externalLists.WidgetStyles, widgets.Style);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", widgets) });
        }

        // GET: Widgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var widgets = await _context.Widgets
                .FirstOrDefaultAsync(m => m.WidgetId == id);
            if (widgets == null)
            {
                return NotFound();
            }

            return PartialView(widgets);
        }

        // POST: Widgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var widgets = await _context.Widgets.FindAsync(id);
            _context.Widgets.Remove(widgets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WidgetsExists(int id)
        {
            return _context.Widgets.Any(e => e.WidgetId == id);
        }
    }
}
