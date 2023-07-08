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
    [Authorize(Policy = "ADMIN")]
    public class ServiceCodesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public ServiceCodesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: ServiceCodes
        public async Task<IActionResult> Index()
        {
            var identityContext = await _context.ServiceCodes.ToListAsync();
            return View(identityContext);
        }

        // GET: ServiceCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCodes = await _context.ServiceCodes
                .FirstOrDefaultAsync(m => m.ServiceCodesId == id);
            if (serviceCodes == null)
            {
                return NotFound();
            }

            return View(serviceCodes);
        }

        // GET: ServiceCodes/Create
        public IActionResult Create()
        {
            string[] codeRateTypes = { "Per Unit", "Per Hour" };
            ViewBag.CodeRateType = new SelectList(codeRateTypes);
            ViewData["Types"] = new SelectList(externalLists.ServiceCodeTypes);
            return PartialView();
        }

        // POST: ServiceCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ServiceCodes serviceCodes)
        {
            if (ModelState.IsValid)
            {
                serviceCodes.CreationDate = DateTime.Now;
                serviceCodes.LastUpdateDate = DateTime.Now;
                _context.Add(serviceCodes);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["Types"] = new SelectList(externalLists.ServiceCodeTypes, serviceCodes.ServiceCodeType);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", serviceCodes) });
        }

        // GET: ServiceCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCodes = await _context.ServiceCodes.FindAsync(id);
            if (serviceCodes == null)
            {
                return NotFound();
            }
            string[] codeRateTypes = { "Per Unit", "Per Hour" };
            ViewBag.CodeRateType = new SelectList(codeRateTypes);
            ViewData["Types"] = new SelectList(externalLists.ServiceCodeTypes, serviceCodes.ServiceCodeType);
            return PartialView(serviceCodes);
        }

        // POST: ServiceCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ServiceCodes serviceCodes)
        {
            if (id != serviceCodes.ServiceCodesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    serviceCodes.LastUpdateDate = DateTime.Now;
                    _context.Update(serviceCodes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceCodesExists(serviceCodes.ServiceCodesId))
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
            ViewData["Types"] = new SelectList(externalLists.ServiceCodeTypes, serviceCodes.ServiceCodeType);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", serviceCodes) });
        }

        // GET: ServiceCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCodes = await _context.ServiceCodes
                .FirstOrDefaultAsync(m => m.ServiceCodesId == id);
            if (serviceCodes == null)
            {
                return NotFound();
            }

            return PartialView(serviceCodes);
        }

        // POST: ServiceCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceCodes = await _context.ServiceCodes.FindAsync(id);
            _context.ServiceCodes.Remove(serviceCodes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceCodesExists(int id)
        {
            return _context.ServiceCodes.Any(e => e.ServiceCodesId == id);
        }
    }
}
