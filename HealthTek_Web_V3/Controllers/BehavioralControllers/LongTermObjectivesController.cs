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

namespace HealthTek_Web_V3.Controllers.BehaviorControllers
{
    [Authorize]
    public class LongTermObjectivesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public LongTermObjectivesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: LongTermObjectives
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.LongTermObjectives.Include(l => l.FkMaladaptives).Include(l => l.FkReplacements);
            return View(await identityContext.ToListAsync());
        }

        // GET: LongTermObjectives/Details/5
        public IActionResult Details()
        {
            return PartialView();
        }

        // GET: LongTermObjectives/Create
        public IActionResult Create(int id, string ObjType)
        {
            ViewData["FkReplacementsId"] = new SelectList(_context.Replacements, "ReplacementsId", "ReplacementName");
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame);
            ViewData["Status"] = new SelectList(externalLists.BehaviorStatuses);
            LongTermObjectives longTermObjectives = new LongTermObjectives();
            switch (ObjType)
            {
                case "Maladaptive":
                    var behavior = _context.Maladaptives.Where(d => d.MaladaptivesId == id).Include(s => s.LongTermObjectives.Where(m => m.LtoType == ObjType)).FirstOrDefault();
                    if (behavior.LongTermObjectives.Count != 0)
                    {
                        longTermObjectives.ObjectiveNumber = behavior.LongTermObjectives.Last().ObjectiveNumber + 1;
                    }
                    else
                    {
                        longTermObjectives.ObjectiveNumber = 1;
                        longTermObjectives.IsCurrent = true;
                    }
                    longTermObjectives.FkMaladaptivesId = id;
                    break;
                case "Replacement":
                    var replacement = _context.Replacements.Where(d => d.ReplacementsId == id).Include(s => s.LongTermObjectives.Where(m => m.LtoType == ObjType)).AsNoTracking().FirstOrDefault();
                    if (replacement.LongTermObjectives.Count != 0)
                    {
                        longTermObjectives.ObjectiveNumber = replacement.LongTermObjectives.Last().ObjectiveNumber + 1;
                    }
                    else
                    {
                        longTermObjectives.ObjectiveNumber = 1;
                        longTermObjectives.IsCurrent = true;
                    }
                    longTermObjectives.FkReplacementsId = id;
                    break;
                case "CTG-M":
                case "CTG-I":
                case "CTG-R":
                case "Preferences":
                    var intervention = _context.CaregiverTrainingGoals.Where(d => d.CaregiverTrainingGoalsId == id).Include(s => s.LongTermObjectives.Where(m => m.LtoType == ObjType)).FirstOrDefault();
                    if (intervention != null && intervention.LongTermObjectives.Count != 0)
                    {
                        longTermObjectives.ObjectiveNumber = intervention.LongTermObjectives.Last().ObjectiveNumber + 1;
                    }
                    else
                    {
                        longTermObjectives.ObjectiveNumber = 1;
                        longTermObjectives.IsCurrent = true;
                    }
                    longTermObjectives.FkCaregiverTrainingGoalsId = id;
                    break;
            }
            longTermObjectives.LtoType = ObjType;
            return PartialView(longTermObjectives);
        }

        // POST: LongTermObjectives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] LongTermObjectives ltos)
        {
            if (ModelState.IsValid)
            {

                ltos.CreationDate = DateTime.Now;
                ltos.LastUpdateDate = DateTime.Now;
                switch (ltos.LtoType)
                {
                    case "Maladaptive":
                        var behavior = _context.Maladaptives.Where(d => d.MaladaptivesId == ltos.FkMaladaptivesId).Include(s => s.LongTermObjectives).AsNoTracking().FirstOrDefault();
                        var patients = _context.Clients.Where(m => m.ClientsId == behavior.FkClientsId).FirstOrDefault();
                        if (ltos.Description == null)
                        {
                            ltos.Description = patients.FullName + " will reduce " + behavior.MaladaptiveName + " at under " + ltos.LtoGoal
        + "  incidents per week for  " + ltos.Duration + " consecutive " + ltos.Timeframe + " Initiated on " + ltos.InitiateDate.Value.ToShortDateString() + " to " + ltos.MasteryDate.Value.ToShortDateString();

                        }
                        break;
                    case "Replacement":
                        behavior = _context.Maladaptives.Include(m => m.FkReplacements).Where(d => d.FkReplacements.ReplacementsId == ltos.FkReplacementsId).Include(s => s.LongTermObjectives).AsNoTracking().FirstOrDefault();
                        patients = _context.Clients.Where(m => m.ClientsId == behavior.FkClientsId).FirstOrDefault();
                        if (ltos.Description == null)
                        {
                            ltos.Description = patients.FullName + " will reduce " + behavior.MaladaptiveName + " at under " + ltos.LtoGoal
        + "  incidents per week for  " + ltos.Duration + " consecutive " + ltos.Timeframe + " Initiated on " + ltos.InitiateDate.Value.ToShortDateString() + " to " + ltos.MasteryDate.Value.ToShortDateString();

                        }
                        break;
                    case "CTG-M":
                    case "CTG-R":
                    case "CTG-I":
                    case "Preferences":
                        ltos.IsCurrent = true;
                        break;
                }
                _context.Add(ltos);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkMaladaptivesId"] = new SelectList(_context.Maladaptives, "MaladaptivesId", "MaladaptiveName", ltos.FkMaladaptivesId);
            ViewData["FkReplacementsId"] = new SelectList(_context.Replacements, "ReplacementsId", "ReplacementName", ltos.FkReplacementsId);
            ViewData["Status"] = new SelectList(externalLists.BehaviorStatuses, ltos.LtoStatus);
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame, ltos.Timeframe);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", ltos) });
        }

        // GET: LongTermObjectives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var longTermObjectives = await _context.LongTermObjectives.FindAsync(id);
            if (longTermObjectives == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(externalLists.BehaviorStatuses, longTermObjectives.LtoStatus);
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame, longTermObjectives.Timeframe);
            return PartialView(longTermObjectives);
        }

        // POST: LongTermObjectives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] LongTermObjectives longTermObjectives)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    switch (longTermObjectives.LtoType)
                    {
                        case "Maladaptive":
                            var behavior = _context.Maladaptives.Where(d => d.MaladaptivesId == longTermObjectives.FkMaladaptivesId).Include(s => s.LongTermObjectives).AsNoTracking().FirstOrDefault();
                            var patients = _context.Clients.Where(m => m.ClientsId == behavior.FkClientsId).FirstOrDefault();
                            if (longTermObjectives.Description == null)
                            {
                                longTermObjectives.Description = patients.FullName + " will reduce " + behavior.MaladaptiveName + " at under " + longTermObjectives.LtoGoal
        + "  incidents per week for  " + longTermObjectives.Duration + " consecutive " + longTermObjectives.Timeframe + " Initiated on " + longTermObjectives.InitiateDate.Value.ToShortDateString() + " to " + longTermObjectives.MasteryDate.Value.ToShortDateString();
                            }
                            break;
                        case "Replacement":
                            behavior = _context.Maladaptives.Where(d => d.FkReplacementsId == longTermObjectives.FkReplacementsId).Include(s => s.LongTermObjectives).AsNoTracking().FirstOrDefault();
                            patients = _context.Clients.Where(m => m.ClientsId == behavior.FkClientsId).FirstOrDefault();
                            if (longTermObjectives.Description == null)
                            {
                                longTermObjectives.Description = patients.FullName + " will reduce " + behavior.MaladaptiveName + " at under " + longTermObjectives.LtoGoal
        + "  incidents per week for  " + longTermObjectives.Duration + " consecutive " + longTermObjectives.Timeframe + " Initiated on " + longTermObjectives.InitiateDate.Value.ToShortDateString() + " to " + longTermObjectives.MasteryDate.Value.ToShortDateString();
                            }
                            break;

                    }
                    _context.Update(longTermObjectives);
                    await _context.SaveChangesAsync();
                    return Redirect(Request.Headers["Referer"].ToString());

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LongTermObjectivesExists(longTermObjectives.LongTermObjectivesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["Status"] = new SelectList(externalLists.BehaviorStatuses, longTermObjectives.LtoStatus);
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame, longTermObjectives.Timeframe);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", longTermObjectives) });
        }

        // GET: LongTermObjectives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var longTermObjectives = await _context.LongTermObjectives
                .Include(s => s.FkMaladaptives)
                .Include(s => s.FkCaregiverTrainingGoals)
                .ThenInclude(s => s.FkMaladaptives)
                .Include(s => s.FkCaregiverTrainingGoals)
                .ThenInclude(s => s.FkPreferences)
                .Include(s => s.FkCaregiverTrainingGoals)
                .ThenInclude(s => s.FkBaAssessmentsInterventions)
                .ThenInclude(s => s.FkInterventions)
                .Include(s => s.FkReplacements)
                .FirstOrDefaultAsync(m => m.LongTermObjectivesId == id);
            if (longTermObjectives == null)
            {
                return NotFound();
            }
            return PartialView(longTermObjectives);
        }

        // POST: LongTermObjectives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromForm] LongTermObjectives longTermObjectives)
        {
            _context.LongTermObjectives.Remove(longTermObjectives);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool LongTermObjectivesExists(int id)
        {
            return _context.LongTermObjectives.Any(e => e.LongTermObjectivesId == id);
        }
        public async Task<JsonResult> ChangeStatus(int id, string status)
        {
            var longTermObjectives = await _context.LongTermObjectives.Include(m => m.FkMaladaptives).Include(m => m.FkReplacements).FirstOrDefaultAsync(m => m.LongTermObjectivesId == id);
            var newid = 0;
            if (longTermObjectives.FkMaladaptives != null)
            {
                newid = longTermObjectives.FkMaladaptives.FkBaAssessmentsId;
            }
            else if (longTermObjectives.FkReplacements != null)
            {
                newid = longTermObjectives.FkReplacements.FkBaAssessmentsId;
            }
            longTermObjectives.LtoStatus = status;
            longTermObjectives.LastUpdateDate = DateTime.Now;
            if (longTermObjectives.LtoStatus == "In Progress")
            {
                longTermObjectives.IsCurrent = true;
            }
            else
            {
                longTermObjectives.IsCurrent = false;
            }
            _context.LongTermObjectives.Update(longTermObjectives);
            await _context.SaveChangesAsync();
            return Json(new { data = "ok" });
        }

    }
}
