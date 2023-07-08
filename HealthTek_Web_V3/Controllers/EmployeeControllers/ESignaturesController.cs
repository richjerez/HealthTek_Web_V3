using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class ESignaturesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _hostingEnv;

        public ESignaturesController(IdentityContext context, UserManager<AppUser> userManager, IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnv = hostingEnv;
        }

        // GET: ESignatures
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var identityContext = await _context.ESignatures.Where(m => m.FkEmployeesId == user.FkEmployeesId).FirstOrDefaultAsync();
            return View(identityContext);
        }

        // POST: ESignatures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ESignatures eSignatures)
        {
            var user = await _userManager.GetUserAsync(User);
            UserInformation information = new UserInformation(this.HttpContext.Request);
            eSignatures.FkEmployeesId = user.FkEmployeesId;
            eSignatures.IsAuthorized = true;
            eSignatures.ESignsIp = information.GetIpAddress();
            eSignatures.CreationDate = DateTime.Now;
            eSignatures.LastUpdateDate = DateTime.Now;
            var signatures = _context.ESignatures.Where(w => w.FkEmployeesId == user.FkEmployeesId).AsNoTracking().FirstOrDefault();
            if (signatures == null)
            {
                _context.ESignatures.Add(eSignatures);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                eSignatures.ESignaturesId = signatures.ESignaturesId;
                await Edit(signatures.ESignaturesId, eSignatures);
            }

            return RedirectToAction(nameof(Index));

        }

        // POST: ESignatures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ESignatures eSignatures)
        {

            try
            {
                _context.Update(eSignatures);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ESignaturesExists(eSignatures.ESignaturesId))
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

        // POST: ESignatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eSignatures = await _context.ESignatures.FindAsync(id);
            _context.ESignatures.Remove(eSignatures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ESignaturesExists(int id)
        {
            return _context.ESignatures.Any(e => e.ESignaturesId == id);
        }
    }
}
