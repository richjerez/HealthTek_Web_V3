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
    public class ShortTermObjectivesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public ShortTermObjectivesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: ShortTermObjectives
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.ShortTermObjectives.Include(s => s.FkMaladaptives).Include(s => s.FkReplacements);
            return View(await identityContext.ToListAsync());
        }

        // GET: ShortTermObjectives/Details/5
        public IActionResult Details()
        {
            ViewData["STOStatus"] = new SelectList(externalLists.BehaviorStatuses);
            return PartialView();
        }

        // GET: ShortTermObjectives/Create
        public IActionResult Create(int id, string ObjType)
        {

            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame);
            ViewData["Status"] = new SelectList(externalLists.BehaviorStatuses);
            ShortTermObjectives shortTermObjectives = new ShortTermObjectives();
            switch (ObjType)
            {
                case "Maladaptive":
                    var behavior = _context.Maladaptives.Where(d => d.MaladaptivesId == id).Include(s => s.ShortTermObjectives.Where(m => m.StoType == ObjType)).AsNoTracking().FirstOrDefault();
                    if (behavior.ShortTermObjectives.Count != 0)
                    {
                        shortTermObjectives.ObjectiveNumber = behavior.ShortTermObjectives.Last().ObjectiveNumber + 1;
                    }
                    else
                    {
                        shortTermObjectives.ObjectiveNumber = 1;
                    }
                    shortTermObjectives.FkMaladaptivesId = id;
                    break;
                case "Replacement":
                    var replacement = _context.Replacements.Where(d => d.ReplacementsId == id).Include(s => s.ShortTermObjectives.Where(m => m.StoType == ObjType)).AsNoTracking().FirstOrDefault();
                    if (replacement.ShortTermObjectives.Count != 0)
                    {
                        shortTermObjectives.ObjectiveNumber = replacement.ShortTermObjectives.Last().ObjectiveNumber + 1;
                    }
                    else
                    {
                        shortTermObjectives.ObjectiveNumber = 1;
                    }
                    shortTermObjectives.FkReplacementsId = id;
                    break;
                case "CTG-M":
                case "CTG-I":
                case "CTG-R":
                case "Preferences":
                    var intervention = _context.CaregiverTrainingGoals.Where(d => d.CaregiverTrainingGoalsId == id)
                        .Include(s => s.ShortTermObjectives.Where(m => m.StoType == ObjType)).AsNoTracking().FirstOrDefault();
                    if (intervention != null && intervention.ShortTermObjectives.Count != 0)
                    {
                        shortTermObjectives.ObjectiveNumber = intervention.ShortTermObjectives.Last().ObjectiveNumber + 1;
                    }
                    else
                    {
                        shortTermObjectives.ObjectiveNumber = 1;
                    }
                    shortTermObjectives.FkCaregiverTrainingGoalsId = id;
                    break;

            }
            shortTermObjectives.StoType = ObjType;
            return PartialView(shortTermObjectives);
        }

        // POST: ShortTermObjectives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ShortTermObjectives sto)
        {
            if (ModelState.IsValid)
            {
                var behaviors = new Maladaptives();
                var patient = new Clients();
                sto.CreationDate = DateTime.Now;
                sto.LastUpdateDate = DateTime.Now;

                switch (sto.StoType)
                {
                    case "Maladaptive":
                        behaviors = _context.Maladaptives.Where(d => d.MaladaptivesId == sto.FkMaladaptivesId).Include(s => s.FkClients).Include(s => s.ShortTermObjectives).FirstOrDefault();
                        patient = behaviors.FkClients;
                        CalculateSTO(sto, behaviors, patient, behaviors.BaselineAverage.Value);
                        break;
                    case "Replacement":
                        behaviors = _context.Maladaptives.Where(d => d.FkReplacementsId == sto.FkReplacementsId).Include(s => s.FkClients).Include(s => s.ShortTermObjectives).FirstOrDefault();
                        patient = behaviors.FkClients;
                        CalculateSTO(sto, behaviors, patient, behaviors.BaselineAverage.Value);
                        break;
                    case "CTG-R":
                    case "CTG-M":
                    case "CTG-I":
                    case "Preferences":
                        if (sto.Description == null)
                        {
                            ModelState.AddModelError("", "The Description for this type of objective should not be null.");
                            foreach (var ModelState in ViewData.ModelState.Values)
                            {
                                foreach (var ModelErrors in ModelState.Errors)
                                {
                                    string errormessage = ModelErrors.ErrorMessage;
                                }
                            }
                        }
                        sto.IsCurrent = true;
                        _context.ShortTermObjectives.Add(sto);
                        await _context.SaveChangesAsync();
                        break;
                }
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", sto) });
        }

        // GET: ShortTermObjectives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shortTermObjectives = await _context.ShortTermObjectives.FindAsync(id);
            if (shortTermObjectives == null)
            {
                return NotFound();
            }
            ViewData["Status"] = new SelectList(externalLists.BehaviorStatuses, shortTermObjectives.StoStatus);
            ViewData["TimeFrame"] = new SelectList(externalLists.TimeFrame, shortTermObjectives.Timeframe);
            return PartialView(shortTermObjectives);
        }

        // POST: ShortTermObjectives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] ShortTermObjectives sto)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    switch (sto.StoType)
                    {
                        case "Maladaptive":
                            var behavior = await _context.Maladaptives.Include(s => s.FkClients)
                                .Include(s => s.ShortTermObjectives).AsNoTracking()
                                .FirstOrDefaultAsync(i => i.MaladaptivesId == sto.FkMaladaptivesId);
                            var patient = _context.Clients.Where(m => m.ClientsId == behavior.FkClientsId).AsNoTracking().FirstOrDefault();
                            List<ShortTermObjectives> stos = new List<ShortTermObjectives>();
                            foreach (var mal in behavior.ShortTermObjectives)
                            {
                                if (mal.ObjectiveNumber > sto.ObjectiveNumber)
                                {
                                    stos.Add(mal);
                                }
                            }
                            behavior.ShortTermObjectives = null;
                            behavior.ShortTermObjectives = stos;
                            UpdateSTO(sto, behavior, patient, behavior.BaselineAverage.Value);
                            break;
                        case "Replacement":
                            behavior = _context.Maladaptives.Include(m => m.FkReplacements)
                                .Where(d => d.FkReplacements.ReplacementsId == sto.FkReplacementsId)
                                .Include(s => s.ShortTermObjectives).AsNoTracking().FirstOrDefault();
                            patient = _context.Clients.Where(m => m.ClientsId == behavior.FkClientsId).AsNoTracking().FirstOrDefault();
                            List<ShortTermObjectives> rplacestos = new List<ShortTermObjectives>();
                            foreach (var mal in behavior.ShortTermObjectives)
                            {
                                if (mal.ObjectiveNumber > sto.ObjectiveNumber)
                                {
                                    rplacestos.Add(mal);
                                }
                            }
                            behavior.ShortTermObjectives = null;
                            behavior.ShortTermObjectives = rplacestos;
                            UpdateSTO(sto, behavior, patient, behavior.BaselineAverage.Value);
                            break;
                        case "CTG-R":
                        case "CTG-M":
                        case "CTG-I":
                            if (sto.Description == null)
                            {
                                ModelState.AddModelError("", "The Description for this type of objective should not be null.");
                                foreach (var ModelState in ViewData.ModelState.Values)
                                {
                                    foreach (var ModelErrors in ModelState.Errors)
                                    {
                                        string errormessage = ModelErrors.ErrorMessage;
                                    }
                                    return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", sto) });
                                }
                            }
                            _context.ShortTermObjectives.Update(sto);
                            await _context.SaveChangesAsync();
                            break;
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShortTermObjectivesExists(sto.ShortTermObjectivesId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", sto) });
        }

        // GET: ShortTermObjectives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shortTermObjectives = await _context.ShortTermObjectives
                .Include(s => s.FkMaladaptives)
                .Include(s => s.FkCaregiverTrainingGoals)
                .ThenInclude(s => s.FkMaladaptives)
                .Include(s => s.FkCaregiverTrainingGoals)
                .ThenInclude(s => s.FkPreferences)
                .Include(s => s.FkCaregiverTrainingGoals)
                .ThenInclude(s => s.FkBaAssessmentsInterventions)
                .ThenInclude(s => s.FkInterventions)
                .Include(s => s.FkReplacements)
                .FirstOrDefaultAsync(m => m.ShortTermObjectivesId == id);
            if (shortTermObjectives == null)
            {
                return NotFound();
            }
            return PartialView(shortTermObjectives);
        }

        // POST: ShortTermObjectives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromForm] ShortTermObjectives shortTermObjectives)
        {
            _context.ShortTermObjectives.Remove(shortTermObjectives);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool ShortTermObjectivesExists(int id)
        {
            return _context.ShortTermObjectives.Any(e => e.ShortTermObjectivesId == id);
        }

        public async Task<JsonResult> ChangeStatus(int id, string status)
        {
            var shortTermObjectives = await _context.ShortTermObjectives.Include(m => m.FkMaladaptives).Include(m => m.FkReplacements).FirstOrDefaultAsync(m => m.ShortTermObjectivesId == id);
            var newid = 0;
            if (shortTermObjectives.FkMaladaptives != null)
            {
                newid = shortTermObjectives.FkMaladaptives.FkBaAssessmentsId;
            }
            else if (shortTermObjectives.FkReplacements != null)
            {
                newid = shortTermObjectives.FkReplacements.FkBaAssessmentsId;
            }
            shortTermObjectives.StoStatus = status;
            shortTermObjectives.LastUpdateDate = DateTime.Now;
            if (shortTermObjectives.StoStatus == "In Progress")
            {
                shortTermObjectives.IsCurrent = true;
            }
            else
            {
                shortTermObjectives.IsCurrent = false;
            }
            _context.ShortTermObjectives.Update(shortTermObjectives);
            await _context.SaveChangesAsync();
            return Json(new { data = "ok" });
        }

        public void CalculateSTO(ShortTermObjectives sto, Maladaptives behaviors, Clients patient, int Avg)
        {
            int reduction = Avg;
            int secondReduction = 0;
            var masteryDate = sto.MasteryDate;
            switch (sto.Timeframe)
            {
                case "Week(s)":
                    masteryDate = sto.InitiateDate.Value.AddDays(sto.Duration * 7);
                    sto.MasteryDate = masteryDate;
                    break;
                case "Month(s)":
                    masteryDate = sto.InitiateDate.Value.AddMonths(sto.Duration);
                    sto.MasteryDate = masteryDate;
                    break;
            }
            if (sto.ObjectiveNumber == 1)
            {
                sto.StoStatus = "In Progress";
                sto.IsCurrent = true;
            }
            else
            {
                sto.StoStatus = "Not Started";
            }
            switch (sto.StoType)
            {
                case "Maladaptive":
                    if (sto.IsReductionPercentage)
                    {
                        var reduct = (int)Math.Round((decimal)((decimal)behaviors.BaselineAverage * (decimal)(sto.ReductionNumber) / 100));
                        secondReduction = (int)reduction - reduct;
                    }
                    else
                    {
                        secondReduction = reduction - sto.ReductionNumber.Value;
                    }
                    break;
                case "Replacement":
                    if (sto.IsReductionPercentage)
                    {
                        if (sto.ObjectiveNumber == 1)
                        {
                            reduction = 0;
                        }
                        secondReduction = reduction + sto.ReductionNumber.Value;
                    }
                    if (secondReduction > 100)
                    {
                        return;
                    }
                    break;
            }
            if (secondReduction < 0 || reduction == secondReduction)
            {
                secondReduction = 0;
            }
            sto.Description = patient.FullName + " will reduce "
                + behaviors.MaladaptiveName + " from " + reduction
                + " to " + secondReduction + " incidents per week for "
                + sto.Duration + " consecutive " + sto.Timeframe
                + " Initiated on " + sto.InitiateDate.Value.ToShortDateString()
                + " to " + sto.MasteryDate.Value.ToShortDateString();
            sto.ReducedNumber = secondReduction;
            _context.ShortTermObjectives.Add(sto);
            _context.SaveChanges();

            if (sto.IsAutomatic)
            {
                var objNum = sto.ObjectiveNumber + 1;
                if (secondReduction > 0)
                {
                    ShortTermObjectives newSTO = new ShortTermObjectives();
                    newSTO = sto;
                    newSTO.ShortTermObjectivesId = 0;
                    newSTO.ObjectiveNumber = objNum;
                    newSTO.InitiateDate = masteryDate;
                    newSTO.StoStatus = "Not Started";
                    newSTO.ReducedNumber = secondReduction;
                    CalculateSTO(newSTO, behaviors, patient, secondReduction);
                }
            }

        }
        public void UpdateSTO(ShortTermObjectives sto, Maladaptives behaviors, Clients patient, int Avg)
        {
            int reduction = Avg;
            int secondReduction = 0;
            var masteryDate = sto.MasteryDate;
            switch (sto.Timeframe)
            {
                case "Week(s)":
                    masteryDate = sto.InitiateDate.Value.AddDays(sto.Duration * 7);
                    sto.MasteryDate = masteryDate;
                    break;
                case "Month(s)":
                    masteryDate = sto.InitiateDate.Value.AddMonths(sto.Duration);
                    sto.MasteryDate = masteryDate;
                    break;
            }
            if (sto.ObjectiveNumber == 1 || sto.StoStatus == "In Progress")
            {
                sto.StoStatus = "In Progress";
                sto.IsCurrent = true;
            }
            else
            {
                sto.StoStatus = "Not Started";
            }
            switch (sto.StoType)
            {
                case "Maladaptive":
                    if (sto.IsReductionPercentage)
                    {
                        var reduct = (int)Math.Round((decimal)((decimal)reduction * (decimal)(sto.ReductionNumber) / 100));
                        secondReduction = (int)reduction - reduct;
                    }
                    else
                    {
                        secondReduction = reduction - sto.ReductionNumber.Value;
                    }
                    break;
                case "Replacement":
                    if (sto.IsReductionPercentage)
                    {
                        var reduct = (int)Math.Round((decimal)((decimal)reduction * (decimal)(sto.ReductionNumber) / 100));
                        secondReduction = (int)reduction + reduct;
                    }
                    else
                    {
                        secondReduction = reduction + sto.ReductionNumber.Value;
                    }
                    break;
            }
            if (secondReduction < 0)
            {
                secondReduction = 0;
            }
            sto.Description = patient.FullName + " will reduce "
                + behaviors.MaladaptiveName + " from " + reduction
                + " to " + secondReduction + " incidents per week for "
                + sto.Duration + " consecutive " + sto.Timeframe
                + " Initiated on " + sto.InitiateDate.Value.ToShortDateString()
                + " to " + sto.MasteryDate.Value.ToShortDateString();
            var auto = sto.IsAutomatic;
            sto.ReducedNumber = secondReduction;
            _context.ShortTermObjectives.Update(sto);
            _context.SaveChanges();

            if (auto)
            {
                var objNum = sto.ObjectiveNumber + 1;
                var newSTO = behaviors.ShortTermObjectives.Where(m => m.ObjectiveNumber == objNum).FirstOrDefault();
                if (newSTO == null)
                {
                    if (secondReduction > 0)
                    {
                        ShortTermObjectives shortTermObjectives = new ShortTermObjectives();
                        shortTermObjectives.ReductionNumber = sto.ReductionNumber;
                        shortTermObjectives.IsReductionPercentage = sto.IsReductionPercentage;
                        shortTermObjectives.FkCaregiverTrainingGoalsId = sto.FkCaregiverTrainingGoalsId;
                        shortTermObjectives.FkMaladaptivesId = sto.FkMaladaptivesId;
                        shortTermObjectives.FkReplacementsId = sto.FkReplacementsId;
                        shortTermObjectives.Timeframe = sto.Timeframe;
                        shortTermObjectives.Duration = sto.Duration;
                        shortTermObjectives.IsCcc = sto.IsCcc;
                        shortTermObjectives.IsAutomatic = sto.IsAutomatic;
                        shortTermObjectives.IsCurrent = sto.IsCurrent;
                        shortTermObjectives.StoType = sto.StoType;
                        shortTermObjectives.ObjectiveNumber = objNum;
                        shortTermObjectives.InitiateDate = masteryDate;
                        shortTermObjectives.ReducedNumber = secondReduction;
                        CalculateSTO(shortTermObjectives, behaviors, patient, secondReduction);
                    }
                }
                else
                {
                    if (secondReduction > 0)
                    {
                        newSTO.ReductionNumber = sto.ReductionNumber;
                        newSTO.IsReductionPercentage = sto.IsReductionPercentage;
                        newSTO.Timeframe = sto.Timeframe;
                        newSTO.Duration = sto.Duration;
                        newSTO.IsCcc = sto.IsCcc;
                        newSTO.IsCurrent = sto.IsCurrent;
                        newSTO.ObjectiveNumber = objNum;
                        newSTO.InitiateDate = masteryDate;
                        newSTO.ReducedNumber = secondReduction;

                        UpdateSTO(newSTO, behaviors, patient, secondReduction);
                    }
                    else
                    {
                        var Temp = behaviors.ShortTermObjectives.ToList();
                        foreach (var mal in Temp)
                        {
                            if (mal.ObjectiveNumber > sto.ObjectiveNumber)
                            {
                                try
                                {
                                    _context.ShortTermObjectives.Remove(mal);
                                    _context.SaveChanges();
                                }
                                catch (DbUpdateConcurrencyException)
                                {
                                }
                            }
                        }
                    }

                }

            }

        }
    }
}
