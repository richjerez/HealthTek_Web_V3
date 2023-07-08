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
    [Authorize]
    public class MedicationsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();
        public MedicationsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Medications/Create
        public IActionResult Create(int id)
        {
            Medications medications = new Medications();
            medications.FkClientsId = id;
            ViewData["Medications"] = new SelectList(externalLists.Medications);
            return PartialView(medications);
        }

        // POST: Medications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Medications medications)
        {
            if (ModelState.IsValid)
            {
                medications.CreationDate = DateTime.Now;
                medications.LastUpdateDate = DateTime.Now;
                _context.Medications.Add(medications);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["Medications"] = new SelectList(externalLists.Medications);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", medications) });
        }

        // GET: Medications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medications = await _context.Medications.FindAsync(id);
            if (medications == null)
            {
                return NotFound();
            }
            ViewData["Medications"] = new SelectList(externalLists.Medications);
            return PartialView(medications);
        }

        // POST: Medications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Medications medications)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    medications.LastUpdateDate = DateTime.Now;
                    _context.Medications.Update(medications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicationsExists(medications.MedicationsId))
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
            ViewData["Medications"] = new SelectList(externalLists.Medications);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", medications) });
        }

        // GET: Medications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medications = await _context.Medications.Include(m => m.FkClients)
                .FirstOrDefaultAsync(m => m.MedicationsId == id);
            if (medications == null)
            {
                return NotFound();
            }
            return PartialView(medications);
        }

        // POST: Medications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromForm] Medications medications)
        {
            _context.Medications.Remove(medications);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool MedicationsExists(int id)
        {
            return _context.Medications.Any(e => e.MedicationsId == id);
        }
    }
}
