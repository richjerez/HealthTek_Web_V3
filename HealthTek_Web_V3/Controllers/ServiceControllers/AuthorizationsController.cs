using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "AuthorizationViews")]
    public class AuthorizationsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ExternalLists externalLists = new ExternalLists();

        public AuthorizationsController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Authorizations
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.Authorizations.Include(a => a.FkClients).ThenInclude(a => a.ClientsFacilities)
                .Include(a => a.FkFacilities).Include(a => a.FkServiceCodes).Include(a => a.AuthorizationNotes);
            return View(await identityContext.ToListAsync());
        }

        // GET: Authorizations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizations = await _context.Authorizations
                .Include(a => a.AuthorizationNotes)
                .ThenInclude(a => a.FkEmployees)
                .Include(a => a.FkClients)
                .ThenInclude(a => a.ClientsFacilities)
                .Include(a => a.FkFacilities)
                .Include(a => a.FkServiceCodes)
                .FirstOrDefaultAsync(m => m.AuthorizationsId == id);
            if (authorizations == null)
            {
                return NotFound();
            }
            foreach (var note in authorizations.AuthorizationNotes)
            {
                note.EmployeeName = note.FkEmployees.EmployeeLabel;
            }
            return PartialView(authorizations);
        }

        // GET: Authorizations/Create
        public IActionResult Create(int? id, int? BaAssessmentId)
        {
            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName");
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName");
            ViewData["FkServiceCodesId"] = new SelectList(_context.Set<ServiceCodes>(), "ServiceCodesId", "CodeTitle");
            ViewData["Status"] = new SelectList(externalLists.AuthorizationStatuses);
            Authorizations auth = new Authorizations();
            if (id != null)
            {
                ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", id);
            }
            if (BaAssessmentId != null)
            {
                auth.FkBaAssessmentsId = BaAssessmentId;
            }
            return PartialView(auth);
        }

        // POST: Authorizations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Authorizations authorizations, bool initial)
        {
            if (ModelState.IsValid)
            {
                var code = _context.ServiceCodes.Where(m => m.CodeTitle.Contains("Ba Assessment")).Select(m => m.ServiceCodesId).FirstOrDefault();
                if (code == authorizations.FkServiceCodesId)
                {
                    var exists = AuthorizationsExists(authorizations.EffectiveDate.Value, authorizations.FkClientsId, code);
                    if (exists == true)
                    {
                        ModelState.AddModelError("", "There is already an Authorization for this user in this period.");
                        foreach (var ModelState in ViewData.ModelState.Values)
                        {
                            foreach (var ModelErrors in ModelState.Errors)
                            {
                                string errormessage = ModelErrors.ErrorMessage;
                            }
                        }
                        ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", authorizations.FkClientsId);
                        ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", authorizations.FkFacilitiesId);
                        ViewData["FkServiceCodesId"] = new SelectList(_context.Set<ServiceCodes>(), "ServiceCodesId", "CodeTitle", authorizations.FkServiceCodesId);
                        ViewData["Status"] = new SelectList(externalLists.AuthorizationStatuses, authorizations.AuthorizationStatus);
                        return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", authorizations) });
                    }
                }
                else
                {
                    var user = await _userManager.GetUserAsync(User);
                    // Set creation and lastupdate dates to now
                    authorizations.CreationDate = DateTime.Now;
                    authorizations.LastUpdateDate = DateTime.Now;
                    AuthorizationNotes notes = new AuthorizationNotes();
                    notes.CreationDate = DateTime.Now;
                    notes.LastUpdateDate = DateTime.Now;
                    notes.NoteDate = DateTime.Now;
                    notes.Notes = authorizations.Notes;
                    notes.FkEmployeesId = user.FkEmployeesId;
                    // Add Notes to Authorizations AuthorizationNotes
                    authorizations.AuthorizationNotes.Add(notes);
                    await _context.SaveChangesAsync();

                    // Add Clients to DB
                    _context.Authorizations.Add(authorizations);
                    await _context.SaveChangesAsync();
                    if (initial == true)
                    {
                        // Create Ananlyst Assignment 
                        Assignments assignment = new Assignments();
                        assignment.FkClientsId = authorizations.FkClientsId;
                        assignment.FkFacilitiesId = authorizations.FkFacilitiesId;
                        assignment.AssignmentStatus = "Open";
                        assignment.AssignmentPosition = "Client needs an Analyst for initial assesment!";
                        assignment.NeedsAttention = true;
                        assignment.AssignmentNote = "Initial Analyst Assignment";
                        assignment.CreationDate = DateTime.Now;
                        assignment.LastUpdateDate = DateTime.Now;
                        // Add Clients to DB
                        _context.Assignments.Add(assignment);
                        await _context.SaveChangesAsync();

                    }
                    var temp = "A new Authorization has been created with PA# " + authorizations.AuthorizationNumber;
                    return Json(new { isValid = true, body = temp });
                }

            }


            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", authorizations.FkClientsId);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", authorizations.FkFacilitiesId);
            ViewData["FkServiceCodesId"] = new SelectList(_context.Set<ServiceCodes>(), "ServiceCodesId", "CodeTitle", authorizations.FkServiceCodesId);
            ViewData["Status"] = new SelectList(externalLists.AuthorizationStatuses, authorizations.AuthorizationStatus);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", authorizations) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNote([FromForm] int FkAuthorizationsId, string Notes)
        {
            var user = await _userManager.GetUserAsync(User);
            AuthorizationNotes notes = new AuthorizationNotes();
            notes.CreationDate = DateTime.Now;
            notes.LastUpdateDate = DateTime.Now;
            notes.NoteDate = DateTime.Now;
            notes.Notes = Notes;
            notes.FkAuthorizationsId = FkAuthorizationsId;
            notes.FkEmployeesId = user.FkEmployeesId;
            _context.AuthorizationNotes.Add(notes);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        // GET: Authorizations/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizations = _context.Authorizations.Include(m => m.AuthorizationNotes).FirstOrDefault(i => i.AuthorizationsId == id);
            if (authorizations.AuthorizationNotes.Count != 0)
            {
                authorizations.Notes = authorizations.AuthorizationNotes.FirstOrDefault().Notes;
            }
            else
            {
                authorizations.Notes = null;
            }
            if (authorizations == null)
            {
                return NotFound();
            }
            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", authorizations.FkClientsId);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", authorizations.FkFacilitiesId);
            ViewData["FkServiceCodesId"] = new SelectList(_context.Set<ServiceCodes>(), "ServiceCodesId", "CodeTitle", authorizations.FkServiceCodesId);
            ViewData["Status"] = new SelectList(externalLists.AuthorizationStatuses, authorizations.AuthorizationStatus);
            return PartialView(authorizations);
        }

        // POST: Authorizations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Authorizations authorizations)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    var authnote = _context.AuthorizationNotes.FirstOrDefault(m => m.FkAuthorizationsId == authorizations.AuthorizationsId);
                    if (authnote != null)
                    {
                        authnote.Notes = authorizations.Notes;
                        authnote.LastUpdateDate = DateTime.Now;
                        _context.AuthorizationNotes.Update(authnote);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        AuthorizationNotes authorizationNotes = new AuthorizationNotes();
                        authorizationNotes.Notes = authorizations.Notes;
                        authorizationNotes.FkAuthorizationsId = authorizations.AuthorizationsId;
                        authorizationNotes.FkEmployeesId = user.FkEmployeesId;
                        authorizationNotes.NoteDate = DateTime.Now;
                        authorizationNotes.CreationDate = DateTime.Now;
                        authorizationNotes.LastUpdateDate = DateTime.Now;
                        _context.AuthorizationNotes.Add(authorizationNotes);
                        await _context.SaveChangesAsync();

                    }

                    authorizations.LastUpdateDate = DateTime.Now;
                    _context.Update(authorizations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                }
                var temp = "An Authorization with PA# " + authorizations.AuthorizationNumber + " has been updated";
                return Json(new { isValid = true, body = temp });
            }
            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", authorizations.FkClientsId);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilitiesId", authorizations.FkFacilitiesId);
            ViewData["FkServiceCodesId"] = new SelectList(_context.Set<ServiceCodes>(), "ServiceCodesId", "CodeTitle", authorizations.FkServiceCodesId);
            ViewData["Status"] = new SelectList(externalLists.AuthorizationStatuses, authorizations.AuthorizationStatus);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", authorizations) });
        }

        // GET: Authorizations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authorizations = await _context.Authorizations.Include(m => m.FkClients)
                .FirstOrDefaultAsync(m => m.AuthorizationsId == id);
            if (authorizations == null)
            {
                return NotFound();
            }
            return PartialView(authorizations);
        }

        // POST: Authorizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromForm] Authorizations authorizations)
        {
            var name = authorizations.AuthorizationNumber;
            _context.Authorizations.Remove(authorizations);
            await _context.SaveChangesAsync();
            var temp = "An Authorization with PA# " + name + " has been deleted";
            TempData["Toast"] = temp;
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool AuthorizationsExists(DateTime start, int id, int? codeId)
        {
            return _context.Authorizations.Any(e => e.FkClientsId == id && e.EffectiveDate.Value < start && e.ExpirationDate.Value > start && e.FkServiceCodesId == codeId);
        }
    }
}
