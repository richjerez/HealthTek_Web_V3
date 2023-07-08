using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.ServiceControllers
{
    [Authorize]
    public class BcabaSupvMeetingsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BcabaSupvMeetingsController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: BcabaSupvMeetings
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.BcabaSupvMeetings.Include(b => b.FkBcabaSignature).Include(b => b.FkSupervisor).Include(b => b.FkSupervisorSignature);
            return View(await identityContext.ToListAsync());
        }

        // GET: BcabaSupvMeetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bcabaSupvMeetings = await _context.BcabaSupvMeetings
                .Include(b => b.FkBcabaSignature)
                .Include(b => b.FkSupervisor)
                .Include(b => b.FkSupervisorSignature)
                .FirstOrDefaultAsync(m => m.BcabaSupvMeetingsId == id);
            if (bcabaSupvMeetings == null)
            {
                return NotFound();
            }

            return View(bcabaSupvMeetings);
        }

        // GET: BcabaSupvMeetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bcabaSupvMeetings = await _context.BcabaSupvMeetings.FindAsync(id);
            if (bcabaSupvMeetings == null)
            {
                return NotFound();
            }
            return PartialView(bcabaSupvMeetings);
        }

        // POST: BcabaSupvMeetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] BcabaSupvMeetings bcabaSupvMeetings, bool Sign)
        {
            if (id != bcabaSupvMeetings.BcabaSupvMeetingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    var empSignature = await _context.Employees.Where(u => u.EmployeesId == user.FkEmployeesId).Include(m => m.FkESignatures).Select(f => f.FkESignatures.ESignaturesId).FirstOrDefaultAsync();
                    if (Sign)
                    {
                        if (bcabaSupvMeetings.FkSupervisorId == user.FkEmployeesId)
                        {
                            bcabaSupvMeetings.FkSupervisorSignatureId = empSignature;
                            bcabaSupvMeetings.SupvSignDate = DateTime.Now;
                        }
                        else
                        {
                            bcabaSupvMeetings.FkBcabaSignatureId = empSignature;
                            bcabaSupvMeetings.BcabaSignDate = DateTime.Now;
                        }
                    }

                    _context.Update(bcabaSupvMeetings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BcabaSupvMeetingsExists(bcabaSupvMeetings.BcabaSupvMeetingsId))
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
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", bcabaSupvMeetings) });
        }

        // GET: BcabaSupvMeetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bcabaSupvMeetings = await _context.BcabaSupvMeetings
                .Include(b => b.FkBcabaSignature)
                .Include(b => b.FkSupervisor)
                .Include(b => b.FkSupervisorSignature)
                .FirstOrDefaultAsync(m => m.BcabaSupvMeetingsId == id);
            if (bcabaSupvMeetings == null)
            {
                return NotFound();
            }

            return View(bcabaSupvMeetings);
        }

        // POST: BcabaSupvMeetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bcabaSupvMeetings = await _context.BcabaSupvMeetings.FindAsync(id);
            _context.BcabaSupvMeetings.Remove(bcabaSupvMeetings);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool BcabaSupvMeetingsExists(int id)
        {
            return _context.BcabaSupvMeetings.Any(e => e.BcabaSupvMeetingsId == id);
        }
    }
}
