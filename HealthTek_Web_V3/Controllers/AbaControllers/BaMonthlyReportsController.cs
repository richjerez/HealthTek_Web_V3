using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class BaMonthlyReportsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists _externalLists = new ExternalLists();

        public BaMonthlyReportsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: BaMonthlyReports
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.BaMonthlyReports.Include(b => b.FkAppointments).Include(b => b.FkBaAssessments);
            return View(await identityContext.ToListAsync());
        }

        // GET: BaMonthlyReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baMonthlyReports = await _context.BaMonthlyReports
                .Include(b => b.FkAppointments)
                .Include(b => b.FkBaAssessments)
                .FirstOrDefaultAsync(m => m.BaMonthlyReportsId == id);
            if (baMonthlyReports == null)
            {
                return NotFound();
            }

            return View(baMonthlyReports);
        }

        // GET: BaMonthlyReports/Create
        public IActionResult Create()
        {
            ViewData["FkAppointmentsId"] = new SelectList(_context.Appointments, "AppointmentsId", "AppointmentsId");
            ViewData["FkBaAssessmentsId"] = new SelectList(_context.BaAssessments, "BaAssessmentsId", "BaAssessmentsId");
            return View();
        }

        // POST: BaMonthlyReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] BaMonthlyReports baMonthlyReports)
        {
            if (ModelState.IsValid)
            {
                _context.Add(baMonthlyReports);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkAppointmentsId"] = new SelectList(_context.Appointments, "AppointmentsId", "AppointmentsId", baMonthlyReports.FkAppointmentsId);
            ViewData["FkBaAssessmentsId"] = new SelectList(_context.BaAssessments, "BaAssessmentsId", "BaAssessmentsId", baMonthlyReports.FkBaAssessmentsId);
            return View(baMonthlyReports);
        }

        // GET: BaMonthlyReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baMonthlyReports = await _context.BaMonthlyReports
                .Include(m => m.EnvironmentalChanges)
                .Include(m => m.FkAppointments)
                .ThenInclude(m => m.FkStartLocation)
                .AsSplitQuery()
                .Include(m => m.FkAppointments)
                .ThenInclude(m => m.FkEndLocation)
                .AsSplitQuery()
                .Include(m => m.FkAppointments)
                .ThenInclude(m => m.FkEmployees)
                .ThenInclude(m => m.EmployeesRoleNames)
                .ThenInclude(m => m.FkRoleNames)
                .AsNoTracking()
                .AsSplitQuery()
                .Include(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .ThenInclude(m => m.ClientsFacilities)
                .AsNoTracking()
                .AsSplitQuery()
                .Include(m => m.FkBaAssessments)
                .ThenInclude(m => m.FkAppointments)
                .ThenInclude(m => m.FkServiceCodes)
                .AsSplitQuery()
                .Include(m => m.FkBaAssessments)
                .ThenInclude(m => m.FkAppointments)
                .ThenInclude(m => m.FkClients)
                .AsSplitQuery()
                .Include(m => m.FkBaAssessments)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .AsSplitQuery()
                .Include(m => m.FkBaAssessments)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.ShortTermObjectives)
                .AsSplitQuery()
                .Include(m => m.FkBaAssessments)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.LongTermObjectives)
                .AsSplitQuery()
                .Include(m => m.FkBaAssessments)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.ShortTermObjectives)
                .AsSplitQuery()
                .Include(m => m.FkBaAssessments)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.LongTermObjectives)
                .AsSplitQuery()
                .Include(m => m.FkBaAssessments)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.LongTermObjectives)
                .AsSplitQuery()
                .Include(m => m.FkBaAssessments)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.ShortTermObjectives)
                .AsSplitQuery()
                .Include(m => m.FkBaAssessments)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.LongTermObjectives)
                .AsSplitQuery()
                .Include(m => m.FkBaAssessments)
                .ThenInclude(m => m.Maladaptives)
                .ThenInclude(m => m.FkReplacements)
                .ThenInclude(m => m.FkCaregiverTrainingGoals)
                .ThenInclude(m => m.ShortTermObjectives)
                .AsSplitQuery()
                .FirstOrDefaultAsync(m => m.BaMonthlyReportsId == id);
            if (baMonthlyReports == null)
            {
                return NotFound();
            }
            ViewData["STOStatus"] = new SelectList(_externalLists.BehaviorStatuses);
            return View(baMonthlyReports);
        }

        // POST: BaMonthlyReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] BaMonthlyReports baMonthlyReports)
        {
            if (id != baMonthlyReports.BaMonthlyReportsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(baMonthlyReports);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaMonthlyReportsExists(baMonthlyReports.BaMonthlyReportsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkAppointmentsId"] = new SelectList(_context.Appointments, "AppointmentsId", "AppointmentsId", baMonthlyReports.FkAppointmentsId);
            ViewData["FkBaAssessmentsId"] = new SelectList(_context.BaAssessments, "BaAssessmentsId", "BaAssessmentsId", baMonthlyReports.FkBaAssessmentsId);
            return View(baMonthlyReports);
        }

        // GET: BaMonthlyReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baMonthlyReports = await _context.BaMonthlyReports
                .Include(b => b.FkAppointments)
                .Include(b => b.FkBaAssessments)
                .FirstOrDefaultAsync(m => m.BaMonthlyReportsId == id);
            if (baMonthlyReports == null)
            {
                return NotFound();
            }

            return View(baMonthlyReports);
        }

        // POST: BaMonthlyReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baMonthlyReports = await _context.BaMonthlyReports.FindAsync(id);
            _context.BaMonthlyReports.Remove(baMonthlyReports);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaMonthlyReportsExists(int id)
        {
            return _context.BaMonthlyReports.Any(e => e.BaMonthlyReportsId == id);
        }
    }
}
