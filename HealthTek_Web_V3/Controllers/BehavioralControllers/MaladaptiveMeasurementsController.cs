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
    public class MaladaptiveMeasurementsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public MaladaptiveMeasurementsController(IdentityContext context)
        {
            _context = context;
        }
        public JsonResult GetGraphView(int id)
        {
            var maladaptives = _context.Maladaptives.Where(m => m.MaladaptivesId == id).Include(m => m.MaladaptiveMeasurements)
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
            return Json(new { Baseline = returnList, Dates = returnDates, Maladaptive = maladaptives.MaladaptiveName, Max = max });
        }

        // GET: MaladaptiveMeasurements
        public async Task<IActionResult> Index(int id)
        {
            var maladaptive = await _context.Maladaptives.FindAsync(id);
            return View(maladaptive);
        }


        // GET: MaladaptiveMeasurements/Details/5
        public IActionResult Details() => PartialView();


        // GET: MaladaptiveMeasurements/Create
        public async Task<IActionResult> Create(int? id)
        {
            MaladaptiveMeasurements maladaptiveMeasurements = new MaladaptiveMeasurements();
            var mal = await _context.Maladaptives.FirstOrDefaultAsync(m => m.MaladaptivesId == id);
            maladaptiveMeasurements.FkMaladaptivesId = id.Value;
            maladaptiveMeasurements.FkMaladaptives = mal;
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame);
            ViewData["Interventions"] = new SelectList(_context.Interventions, "InterventionName", "InterventionName");
            return PartialView(maladaptiveMeasurements);
        }

        // POST: MaladaptiveMeasurements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] MaladaptiveMeasurements maladaptiveMeasurements)
        {

            if (ModelState.IsValid)
            {
                maladaptiveMeasurements.FkMaladaptives = null;
                maladaptiveMeasurements.DateMeasured = DateTime.Now;
                maladaptiveMeasurements.CreationDate = DateTime.Now;
                maladaptiveMeasurements.LastUpdateDate = DateTime.Now;
                maladaptiveMeasurements.InterventionsUsed = String.Join(", ", maladaptiveMeasurements.InterventionsUsedList.ToArray());
                _context.Add(maladaptiveMeasurements);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            if (maladaptiveMeasurements.InterventionsUsedList == null)
            {
                ModelState.AddModelError("", "Please select which interventions were used!");
                foreach (var ModelState in ViewData.ModelState.Values)
                {
                    foreach (var ModelErrors in ModelState.Errors)
                    {
                        string errormessage = ModelErrors.ErrorMessage;
                    }
                }
            }
            var mal = await _context.Maladaptives.FirstOrDefaultAsync(m => m.MaladaptivesId == maladaptiveMeasurements.FkMaladaptivesId);
            maladaptiveMeasurements.FkMaladaptives = mal;
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame);
            ViewData["Interventions"] = new SelectList(_context.Interventions, "InterventionName", "InterventionName");
            var json = new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", maladaptiveMeasurements) };
            return Json(json);
        }

        // GET: MaladaptiveMeasurements/Edit/5
        public async Task<IActionResult> Edit(int? id, int BaNotesId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maladaptiveMeasurements = await _context.MaladaptiveMeasurements.Include(m => m.FkMaladaptives).FirstOrDefaultAsync(m => m.MaladaptiveMeasurementsId == id);
            if (maladaptiveMeasurements == null)
            {
                return NotFound();
            }
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame, maladaptiveMeasurements.DurationUnit);
            var interventions = maladaptiveMeasurements.InterventionsUsed;
            var allInterventions = new SelectList(_context.Interventions, "InterventionName", "InterventionName");
            foreach (var item in allInterventions)
            {
                if (interventions.Contains(item.Value))
                {
                    item.Selected = true;
                }
            }
            ViewData["Interventions"] = allInterventions;
            return PartialView(maladaptiveMeasurements);
        }

        // POST: MaladaptiveMeasurements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] MaladaptiveMeasurements maladaptiveMeasurements)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    maladaptiveMeasurements.InterventionsUsed = String.Join(", ", maladaptiveMeasurements.InterventionsUsedList.ToArray());
                    maladaptiveMeasurements.LastUpdateDate = DateTime.Now;
                    _context.Update(maladaptiveMeasurements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaladaptiveMeasurementsExists(maladaptiveMeasurements.MaladaptiveMeasurementsId))
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
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame, maladaptiveMeasurements.DurationUnit);
            var interventions = maladaptiveMeasurements.InterventionsUsed;
            var allInterventions = new SelectList(_context.Interventions, "InterventionName", "InterventionName");
            foreach (var item in allInterventions)
            {
                if (interventions.Contains(item.Value))
                {
                    item.Selected = true;
                }
            }
            ViewData["Interventions"] = allInterventions;
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", maladaptiveMeasurements) });
        }

        // GET: MaladaptiveMeasurements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maladaptiveMeasurements = await _context.MaladaptiveMeasurements
                .Include(m => m.FkMaladaptives)
                .FirstOrDefaultAsync(m => m.MaladaptiveMeasurementsId == id);
            if (maladaptiveMeasurements == null)
            {
                return NotFound();
            }

            return View(maladaptiveMeasurements);
        }

        // POST: MaladaptiveMeasurements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maladaptiveMeasurements = await _context.MaladaptiveMeasurements.FindAsync(id);
            _context.MaladaptiveMeasurements.Remove(maladaptiveMeasurements);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool MaladaptiveMeasurementsExists(int id)
        {
            return _context.MaladaptiveMeasurements.Any(e => e.MaladaptiveMeasurementsId == id);
        }
    }
}
