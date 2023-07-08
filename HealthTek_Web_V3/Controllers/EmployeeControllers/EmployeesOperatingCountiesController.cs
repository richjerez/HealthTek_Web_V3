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

namespace HealthTek_Web_V3.Controllers.EmployeeControllers
{
    [Authorize]
    public class EmployeesOperatingCountiesController : Controller
    {
        private readonly IdentityContext _context;

        public EmployeesOperatingCountiesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: EmployeesOperatingCounties/Create
        public IActionResult Create(string id)
        {
            EmployeesOperatingCounties operatingCounties = new EmployeesOperatingCounties();
            operatingCounties.FkEmployeesId = id;
            ViewData["FkOperatingCountiesId"] = new SelectList(_context.OperatingCounties, "OperatingCountiesId", "County");
            return PartialView(operatingCounties);
        }

        // POST: EmployeesOperatingCounties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EmployeesOperatingCounties employeesOperatingCounties)
        {
            if (ModelState.IsValid)
            {
                employeesOperatingCounties.CreationDate = DateTime.Now;
                employeesOperatingCounties.LastUpdateDate = DateTime.Now;
                _context.Add(employeesOperatingCounties);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkOperatingCountiesId"] = new SelectList(_context.OperatingCounties, "OperatingCountiesId", "County", employeesOperatingCounties.FkOperatingCountiesId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", employeesOperatingCounties) });
        }

        // GET: EmployeesOperatingCounties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesOperatingCounties = await _context.EmployeesOperatingCounties.FindAsync(id);
            if (employeesOperatingCounties == null)
            {
                return NotFound();
            }
            ViewData["FkOperatingCountiesId"] = new SelectList(_context.OperatingCounties, "OperatingCountiesId", "County", employeesOperatingCounties.FkOperatingCountiesId);
            return PartialView(employeesOperatingCounties);
        }

        // POST: EmployeesOperatingCounties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] EmployeesOperatingCounties employeesOperatingCounties)
        {
            if (id != employeesOperatingCounties.EmployeesOperatingCountiesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeesOperatingCounties);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesOperatingCountiesExists(employeesOperatingCounties.EmployeesOperatingCountiesId))
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
            ViewData["FkOperatingCountiesId"] = new SelectList(_context.OperatingCounties, "OperatingCountiesId", "County", employeesOperatingCounties.FkOperatingCountiesId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", employeesOperatingCounties) });
        }
        private bool EmployeesOperatingCountiesExists(int id)
        {
            return _context.EmployeesOperatingCounties.Any(e => e.EmployeesOperatingCountiesId == id);
        }
    }
}
