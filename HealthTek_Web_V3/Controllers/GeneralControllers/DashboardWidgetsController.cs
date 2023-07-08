using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.GeneralControllers
{
    public class DashboardWidgetsController : Controller
    {
        private readonly IdentityContext _context;

        public DashboardWidgetsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: DashboardWidgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboardWidgets = await _context.DashboardWidgets
                .Include(d => d.FkWidget)
                .FirstOrDefaultAsync(m => m.DashboardWidgetId == id);
            if (dashboardWidgets == null)
            {
                return NotFound();
            }

            return PartialView(dashboardWidgets);
        }

        // POST: DashboardWidgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dashboardWidgets = await _context.DashboardWidgets.FindAsync(id);
            _context.DashboardWidgets.Remove(dashboardWidgets);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
