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
    public class FunctionsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public FunctionsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: Functions
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.Functions.Include(m => m.FkMaladaptives);
            return View(await identityContext.ToListAsync());
        }

        // GET: Functions/Details/5
        public IActionResult Details()
        {
            return PartialView();
        }

        // GET: Functions/Create
        public IActionResult Create(int? id)
        {
            Functions functions = new Functions();
            if (id != null)
            {
                functions.FkMaladaptivesId = id;
            }
            ViewData["Functions"] = new SelectList(externalLists.Functions);
            return PartialView(functions);
        }

        // POST: Functions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Functions Functions)
        {
            if (ModelState.IsValid)
            {
                Functions.CreationDate = DateTime.Now;
                Functions.LastUpdateDate = DateTime.Now;
                _context.Add(Functions);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["Functions"] = new SelectList(externalLists.Functions);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", Functions) });
        }

        // GET: Functions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Functions = await _context.Functions.FindAsync(id);
            if (Functions == null)
            {
                return NotFound();
            }
            ViewData["Functions"] = new SelectList(externalLists.Functions, Functions.FunctionName);
            return PartialView(Functions);
        }

        // POST: Functions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Functions Functions)
        {
            if (id != Functions.FunctionsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Functions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FunctionsExists(Functions.FunctionsId))
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
            ViewData["Functions"] = new SelectList(externalLists.Functions, Functions.FunctionName);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", Functions) });
        }

        // GET: Functions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Functions = await _context.Functions
                .FirstOrDefaultAsync(m => m.FunctionsId == id);
            if (Functions == null)
            {
                return NotFound();
            }

            return PartialView(Functions);
        }

        // POST: Functions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var functions = await _context.Functions.FindAsync(id);
            _context.Functions.Remove(functions);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool FunctionsExists(int id)
        {
            return _context.Functions.Any(e => e.FunctionsId == id);
        }
    }
}
