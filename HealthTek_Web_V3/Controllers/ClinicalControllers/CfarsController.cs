using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace HealthTek_Web_V3.Controllers.ClinicalControllers
{
    [Authorize]
    public class CfarsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists _externalList = new ExternalLists();
        private readonly UserManager<AppUser> _userManager;

        public CfarsController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cfars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cfars = _context.Cfars
                .Include(f => f.FkAppointments)
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.ClientsFacilities)
                .Include(f => f.FkAppointments) // Medications
                .ThenInclude(f => f.FkClients)
                .ThenInclude(f => f.Medications)
                .AsSplitQuery()
                .Include(f => f.FkAppointments) // Roles
                .ThenInclude(f => f.FkEmployees)
                .ThenInclude(f => f.EmployeesRoleNames)
                .ThenInclude(f => f.FkRoleNames)
                .AsSplitQuery()
                .Include(f => f.FkAppointments) // Service Codes
                .ThenInclude(f => f.FkServiceCodes)
                .AsSplitQuery()
                .FirstOrDefault(m => m.CfarsId == id);
            if (cfars == null)
            {
                return NotFound();
            }
            var newid = cfars.FkAppointments.FkClientsId;

            ViewData["FkEmployeeSignatureId"] = new SelectList(_context.ESignatures, "ESignaturesId", "ESignaturesId");
            return View(cfars);
        }

        // POST: Cfars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Cfars cfars)
        {
            if (id != cfars.CfarsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    var emp = _context.Employees.Include(m => m.FkESignatures).FirstOrDefault(m => m.EmployeesId == user.FkEmployeesId);
                    cfars.FkEmployeeSignatureId = emp.FkESignatures.ESignaturesId;
                    cfars.LastUpdateDate = DateTime.Now;
                    _context.Update(cfars);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CfarsNotesExists(cfars.CfarsId.Value))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "QualityAssurance");
            }

            ViewData["FkEmployeeSignatureId"] = new SelectList(_context.ESignatures, "ESignaturesId", "ESignaturesId");
            return View(cfars);
        }

        private bool CfarsNotesExists(int id)
        {
            return _context.Cfars.Any(e => e.CfarsId == id);
        }
    }
}
