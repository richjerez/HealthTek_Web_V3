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
    public class EmployeesRoleNamesController : Controller
    {
        private readonly IdentityContext _context;

        public EmployeesRoleNamesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: EmployeesRoleNames/Create
        public IActionResult Create(string id)
        {
            EmployeesRoleNames employeesRoleNames = new EmployeesRoleNames();
            employeesRoleNames.FkEmployeesId = id;
            ViewData["FkRoleNamesId"] = new SelectList(_context.RoleNames, "RoleNamesId", "RoleName");
            return PartialView(employeesRoleNames);
        }

        // POST: EmployeesRoleNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EmployeesRoleNames employeesRoleNames)
        {
            if (ModelState.IsValid)
            {
                var roleCheck = employeesRoleNames.FkRoleNames.RoleName;
                var emp = employeesRoleNames.FkEmployeesId;
                var roledocs = _context.RoleDocsCatalog.Where(m => m.Roles.Contains(roleCheck)).ToList();
                foreach (var roledoc in roledocs)
                {
                    DocumentationProcess process = new DocumentationProcess();
                    process.FkEmployeesId = emp;
                    process.Role = roleCheck;
                    process.FkRoleDocsCatalogId = roledoc.RoleDocsCatalogId;
                    process.CreationDate = DateTime.Now;
                    process.LastUpdateDate = DateTime.Now;
                    _context.DocumentationProcess.Add(process);
                    await _context.SaveChangesAsync();
                }

                employeesRoleNames.CreationDate = DateTime.Now;
                employeesRoleNames.LastUpdateDate = DateTime.Now;
                _context.Add(employeesRoleNames);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkRoleNamesId"] = new SelectList(_context.RoleNames, "RoleNamesId", "RoleName", employeesRoleNames.FkRoleNamesId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", employeesRoleNames) });
        }

        // GET: EmployeesRoleNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeesRoleNames = await _context.EmployeesRoleNames.FindAsync(id);
            if (employeesRoleNames == null)
            {
                return NotFound();
            }
            ViewData["FkRoleNamesId"] = new SelectList(_context.RoleNames, "RoleNamesId", "RoleName", employeesRoleNames.FkRoleNamesId);
            return PartialView(employeesRoleNames);
        }

        // POST: EmployeesRoleNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] EmployeesRoleNames employeesRoleNames)
        {
            if (id != employeesRoleNames.EmployeesRoleNamesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var roleCheck = _context.RoleNames.Find(employeesRoleNames.FkRoleNamesId);
                    var emp = employeesRoleNames.FkEmployeesId;
                    var roledocs = _context.RoleDocsCatalog.Where(m => m.Roles.Contains(roleCheck.RoleName)).ToList();
                    var rolesTodelete = _context.DocumentationProcess.Where(m => m.RoleDocsCatalogs.Roles.Contains(roleCheck.RoleName)).AsNoTracking().ToList();
                    _context.DocumentationProcess.RemoveRange(rolesTodelete);
                    _context.Entry(employeesRoleNames).State = EntityState.Detached;

                    foreach (var roledoc in roledocs)
                    {
                        DocumentationProcess process = new DocumentationProcess();
                        process.Role = roleCheck.RoleName;
                        process.FkEmployeesId = emp;
                        process.FkRoleDocsCatalogId = roledoc.RoleDocsCatalogId;
                        process.CreationDate = DateTime.Now;
                        process.LastUpdateDate = DateTime.Now;
                        _context.DocumentationProcess.Add(process);
                        await _context.SaveChangesAsync();
                    }

                    employeesRoleNames.LastUpdateDate = DateTime.Now;
                    _context.Update(employeesRoleNames);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesRoleNamesExists(employeesRoleNames.EmployeesRoleNamesId))
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
            ViewData["FkRoleNamesId"] = new SelectList(_context.RoleNames, "RoleNamesId", "RoleName", employeesRoleNames.FkRoleNamesId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", employeesRoleNames) });
        }

        private bool EmployeesRoleNamesExists(int id)
        {
            return _context.EmployeesRoleNames.Any(e => e.EmployeesRoleNamesId == id);
        }
    }
}
