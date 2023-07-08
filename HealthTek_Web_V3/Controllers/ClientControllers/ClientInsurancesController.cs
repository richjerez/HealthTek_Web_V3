using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.ClientControllers
{
    [Authorize(Policy = "InsuranceViews")]
    public class ClientInsurancesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();
        private readonly EmailSender _emailSender;

        public ClientInsurancesController(IdentityContext context, EmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: ClientInsurances
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.ClientInsurances.Include(c => c.FkClients).ThenInclude(c => c.ClientsFacilities);
            return View(await identityContext.ToListAsync());
        }
        public async Task<IActionResult> Verify(int id)
        {
            var insurance = _context.ClientInsurances.Where(m => m.ClientInsurancesId == id)
                .Include(f => f.FkClients)
                .ThenInclude(m => m.Assignments)
                .ThenInclude(m => m.FkEmployees)
                .FirstOrDefault();
            var assignments = insurance.FkClients.Assignments
                .Where(m => m.AssignmentPosition != null
                && !m.AssignmentPosition.Contains("Rejected")
                && m.AssignmentStatus != "Archived").ToList();

            insurance.IsVerified = true;
            _context.ClientInsurances.Update(insurance);
            await _context.SaveChangesAsync();

            // Send Message
            Messages emailModel = new Messages();
            emailModel.ToEmail = insurance.FkClients.Email;
            emailModel.Title = "Insurance Verification";
            emailModel.Message = $"The policy " + insurance.PolicyIdentifier + " from " + insurance.PolicyName + " has been updated!";
            await _emailSender.SendMessage(emailModel);
            foreach (var assignment in assignments)
            {
                emailModel.ToEmail = assignment.FkEmployees.Email;
                await _emailSender.SendMessage(emailModel);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ClientInsurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientInsurances = await _context.ClientInsurances
                .Include(c => c.FkClients)
                .FirstOrDefaultAsync(m => m.ClientInsurancesId == id);
            if (clientInsurances == null)
            {
                return NotFound();
            }

            return View(clientInsurances);
        }

        // GET: ClientInsurances/Create
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewData["FkClientsId"] = new SelectList(_context.Clients.ToList(), "ClientsId", "FullName", id);
            }
            else
            {
                ViewData["FkClientsId"] = new SelectList(_context.Clients.ToList(), "ClientsId", "FullName");
            }
            ViewData["Policies"] = new SelectList(_context.Set<ClientInsurancesCatalog>(), "PolicyName", "PolicyName");
            ViewData["Status"] = new SelectList(externalLists.PolicyStatuses);
            return PartialView();
        }

        // POST: ClientInsurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ClientInsurances clientInsurances)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientInsurances);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkClientsId"] = clientInsurances.FkClientsId;
            ViewData["Status"] = new SelectList(externalLists.PolicyStatuses, clientInsurances.PolicyStatus);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", clientInsurances) });
        }

        // GET: ClientInsurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientInsurances = await _context.ClientInsurances.Include(s => s.FkClients).ThenInclude(m => m.ClientsFacilities).FirstOrDefaultAsync(i => i.ClientInsurancesId == id);
            if (clientInsurances == null)
            {
                return NotFound();
            }
            ViewData["Policies"] = new SelectList(_context.ClientInsurancesCatalog, "PolicyName", "PolicyName", clientInsurances.PolicyName);
            ViewData["FkClientsId"] = new SelectList(_context.Clients, "ClientsId", "FullName", clientInsurances.FkClientsId);
            ViewData["Status"] = new SelectList(externalLists.PolicyStatuses, clientInsurances.PolicyStatus);
            return PartialView(clientInsurances);
        }

        // POST: ClientInsurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ClientInsurances clientInsurances)
        {
            if (id != clientInsurances.ClientInsurancesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    clientInsurances.CreationDate = DateTime.Now;
                    clientInsurances.LastUpdateDate = DateTime.Now;

                    var insurance = _context.ClientInsurances
                        .Where(m => m.ClientInsurancesId == id)
                        .AsNoTracking()
                        .Include(f => f.FkClients)
                        .ThenInclude(m => m.Assignments)
                        .ThenInclude(m => m.FkEmployees)
                        .FirstOrDefault();
                    var assignments = insurance.FkClients.Assignments
                        .Where(m => m.AssignmentPosition != null
                        && !m.AssignmentPosition.Contains("Rejected")
                        && m.AssignmentStatus != "Archived").ToList();

                    // Send Message
                    Messages emailModel = new Messages();
                    emailModel.ToEmail = insurance.FkClients.Email;
                    emailModel.Title = "Insurance Verification";
                    emailModel.Message = $"The policy " + insurance.PolicyIdentifier + " from " + insurance.PolicyName + " has been updated!";
                    await _emailSender.SendMessage(emailModel);
                    foreach (var assignment in assignments)
                    {
                        emailModel.ToEmail = assignment.FkEmployees.Email;
                        await _emailSender.SendMessage(emailModel);
                    }

                    _context.Update(clientInsurances);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientInsurancesExists(clientInsurances.ClientInsurancesId))
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
            ViewData["Policies"] = new SelectList(_context.ClientInsurancesCatalog, "PolicyName", "PolicyName", clientInsurances.PolicyName);
            ViewData["FkClientsId"] = new SelectList(_context.Clients, "ClientsId", "FullName", clientInsurances.FkClientsId);
            ViewData["Status"] = new SelectList(externalLists.PolicyStatuses, clientInsurances.PolicyStatus);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", clientInsurances) });
        }

        // GET: ClientInsurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientInsurances = await _context.ClientInsurances
                .Include(c => c.FkClients)
                .FirstOrDefaultAsync(m => m.ClientInsurancesId == id);
            if (clientInsurances == null)
            {
                return NotFound();
            }

            return PartialView(clientInsurances);
        }

        // POST: ClientInsurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientInsurances = await _context.ClientInsurances.FindAsync(id);
            _context.ClientInsurances.Remove(clientInsurances);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool ClientInsurancesExists(int id)
        {
            return _context.ClientInsurances.Any(e => e.ClientInsurancesId == id);
        }
    }
}
