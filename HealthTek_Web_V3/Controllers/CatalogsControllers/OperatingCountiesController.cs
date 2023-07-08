using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "ADMIN")]
    public class OperatingCountiesController : Controller
    {
        private readonly IdentityContext _context;

        public OperatingCountiesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: OperatingCounties
        public async Task<IActionResult> Index()
        {
            return View(await _context.OperatingCounties.ToListAsync());
        }

        // GET: OperatingCounties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operatingCounties = await _context.OperatingCounties
                .FirstOrDefaultAsync(m => m.OperatingCountiesId == id);
            if (operatingCounties == null)
            {
                return NotFound();
            }

            return View(operatingCounties);
        }

        // GET: OperatingCounties/Create
        public IActionResult Create()
        {
            return PartialView();
        }

        // POST: OperatingCounties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] OperatingCounties operatingCounties)
        {
            if (ModelState.IsValid)
            {
                operatingCounties.CreationDate = DateTime.Now;
                operatingCounties.LastUpdateDate = DateTime.Now;
                _context.Add(operatingCounties);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", operatingCounties) });
        }

        // GET: OperatingCounties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operatingCounties = await _context.OperatingCounties.FindAsync(id);
            if (operatingCounties == null)
            {
                return NotFound();
            }
            return PartialView(operatingCounties);
        }

        // POST: OperatingCounties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] OperatingCounties operatingCounties)
        {
            if (id != operatingCounties.OperatingCountiesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    operatingCounties.LastUpdateDate = DateTime.Now;
                    _context.Update(operatingCounties);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperatingCountiesExists(operatingCounties.OperatingCountiesId))
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

            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", operatingCounties) });
        }

        // GET: OperatingCounties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operatingCounties = await _context.OperatingCounties
                .FirstOrDefaultAsync(m => m.OperatingCountiesId == id);
            if (operatingCounties == null)
            {
                return NotFound();
            }

            return PartialView(operatingCounties);
        }

        // POST: OperatingCounties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operatingCounties = await _context.OperatingCounties.FindAsync(id);
            _context.OperatingCounties.Remove(operatingCounties);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperatingCountiesExists(int id)
        {
            return _context.OperatingCounties.Any(e => e.OperatingCountiesId == id);
        }
    }
}
