using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class ReplacementMeasurementsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public ReplacementMeasurementsController(IdentityContext context)
        {
            _context = context;
        }

        public JsonResult GetGraphView(int id)
        {
            var maladaptives = _context.Replacements.Where(m => m.ReplacementsId == id).Include(m => m.ReplacementMeasurements)
                .Include(m => m.ShortTermObjectives).Include(m => m.LongTermObjectives).FirstOrDefault();
            var max = maladaptives.BaselineAverage * 1.5;
            List<string> returnList = new List<string>();
            List<string> returnDates = new List<string>();
            returnList.Add("0 Not Met");
            returnList.Add(maladaptives.Baseline1.Value.ToString());
            returnList.Add(maladaptives.Baseline2.Value.ToString());
            returnList.Add(maladaptives.Baseline3.Value.ToString());
            returnDates.Add(maladaptives.Baseline1StartDate.Value.AddDays(-7).ToShortDateString());
            returnDates.Add(maladaptives.Baseline1StartDate.Value.ToShortDateString());
            returnDates.Add(maladaptives.Baseline2StartDate.Value.ToShortDateString());
            returnDates.Add(maladaptives.Baseline3StartDate.Value.ToShortDateString());
            foreach (var item in maladaptives.ShortTermObjectives)
            {
                returnList.Add(item.ReducedNumber.ToString());
                returnDates.Add(item.InitiateDate.Value.ToShortDateString());
            }
            return Json(new { Baseline = returnList, Dates = returnDates, Maladaptive = maladaptives.ReplacementName, Max = max });
        }

        // GET: MaladaptiveMeasurements
        public async Task<IActionResult> Index(int id)
        {
            var replacement = await _context.Replacements.FindAsync(id);
            return View(replacement);
        }

        // GET: ReplacementMeasurements/Details/5
        public IActionResult Details() => PartialView();

        // GET: ReplacementMeasurements/Create
        public IActionResult Create(int id)
        {
            ReplacementMeasurements replacementMeasurements = new ReplacementMeasurements();
            var replacement = _context.Replacements.FirstOrDefault(m => m.ReplacementsId == id);
            replacementMeasurements.FkReplacementsId = id;
            replacementMeasurements.FkReplacements = replacement;
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame);
            return PartialView(replacementMeasurements);
        }

        // POST: ReplacementMeasurements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ReplacementMeasurements replacementMeasurements)
        {
            if (ModelState.IsValid)
            {
                replacementMeasurements.FkReplacements = null;
                replacementMeasurements.LastUpdateDate = DateTime.Now;
                replacementMeasurements.CreationDate = DateTime.Now;
                replacementMeasurements.DateMeasured = DateTime.Now;
                _context.Add(replacementMeasurements);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", replacementMeasurements) });
        }

        // GET: ReplacementMeasurements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacementMeasurements = await _context.ReplacementMeasurements.Include(m => m.FkReplacements).FirstOrDefaultAsync(i => i.ReplacementMeasurementsId == id);
            if (replacementMeasurements == null)
            {
                return NotFound();
            }
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame);
            return PartialView(replacementMeasurements);
        }

        // POST: ReplacementMeasurements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] ReplacementMeasurements replacementMeasurements)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(replacementMeasurements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReplacementMeasurementsExists(replacementMeasurements.ReplacementMeasurementsId))
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
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", replacementMeasurements) });
        }

        // GET: ReplacementMeasurements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var replacementMeasurements = await _context.ReplacementMeasurements
                .Include(r => r.FkReplacements)
                .FirstOrDefaultAsync(m => m.ReplacementMeasurementsId == id);
            if (replacementMeasurements == null)
            {
                return NotFound();
            }

            return PartialView(replacementMeasurements);
        }

        // POST: ReplacementMeasurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var replacementMeasurements = await _context.ReplacementMeasurements.FindAsync(id);
            _context.ReplacementMeasurements.Remove(replacementMeasurements);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool ReplacementMeasurementsExists(int id)
        {
            return _context.ReplacementMeasurements.Any(e => e.ReplacementMeasurementsId == id);
        }
    }
}
