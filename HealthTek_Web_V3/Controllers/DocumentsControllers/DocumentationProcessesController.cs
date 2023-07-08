using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.DocumentsControllers
{
    [Authorize]
    public class DocumentationProcessesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _hostEnv;

        public DocumentationProcessesController(IdentityContext context, UserManager<AppUser> userManager, IWebHostEnvironment hostEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostEnv = hostEnv;
        }

        [Authorize(Policy = "SUPERUSER")]
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.DocumentationProcess.Include(d => d.FkDocuments).Include(d => d.FkEmployees).Include(d => d.FkUploadedBy);
            return View(await identityContext.ToListAsync());
        }

        // GET: DocumentationProcesses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocumentationProcesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] DocumentationProcess documentationProcess)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documentationProcess);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(documentationProcess);
        }

        // GET: DocumentationProcesses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentationProcess = await _context.DocumentationProcess
                .Include(m => m.FkDocuments).Include(m => m.RoleDocsCatalogs).FirstOrDefaultAsync(i => i.DocumentationProcessId == id);
            if (documentationProcess == null)
            {
                return NotFound();
            }
            if (documentationProcess.FkDocuments == null)
            {
                Documents documents = new Documents();
                documents.FkEmployeesId = documentationProcess.FkEmployeesId;
                documents.DocumentTitle = documentationProcess.RoleDocsCatalogs.Title;
                documents.DocumentType = "HR";
                documentationProcess.FkDocuments = documents;

            }
            ViewData["Status"] = new SelectList(externalLists.DocumentStatuses);
            return PartialView(documentationProcess);
        }

        // POST: DocumentationProcesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] DocumentationProcess documentationProcess)
        {
            if (id != documentationProcess.DocumentationProcessId)
            {
                return NotFound();
            }
            ModelState["customFiles"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    var documents = documentationProcess.FkDocuments;
                    documents.DocumentType = "HR";
                    var doc = _context.RoleDocsCatalog.Where(m => m.RoleDocsCatalogId == documentationProcess.FkRoleDocsCatalogId).AsNoTracking().FirstOrDefault();
                    var exp = doc.Expiration;
                    foreach (var docs in documents.customFiles)
                    {
                        if (documents.customFiles != null)
                        {
                            UploadFileHelper uploadFile = new UploadFileHelper(_hostEnv);
                            await uploadFile.UploadFileAsync(docs, documentationProcess.FkEmployeesId, false);
                            documents.CreationDate = DateTime.Now;
                            documents.LastUpdateDate = DateTime.Now;
                            documents.FkUploadedById = user.FkEmployeesId;
                            documents.IsRequired = doc.IsRequired;
                            documents.IsAttached = true;
                            documents.IsSorted = true;
                            documents.FkIntakesId = null;
                            documents.DocumentsId = 0;
                            documents.DocumentUrl = docs.FileName;
                            switch (exp)
                            {
                                case "One years":
                                    documents.DocumentExpirationDate = DateTime.Now.AddYears(1);
                                    break;
                                case "Two years":
                                    documents.DocumentExpirationDate = DateTime.Now.AddYears(2);
                                    break;
                                case "Three years":
                                    documents.DocumentExpirationDate = DateTime.Now.AddYears(3);
                                    break;
                                case "Varied":
                                    break;
                                case "Never Expires":
                                    documents.DocumentExpirationDate = null;
                                    break;
                            }
                            _context.Documents.Add(documents);
                            await _context.SaveChangesAsync();
                        }

                    }
                    documentationProcess.LastUpdateDate = DateTime.Now;
                    documentationProcess.FkUploadedById = user.FkEmployeesId;
                    _context.Update(documentationProcess);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentationProcessExists(documentationProcess.DocumentationProcessId))
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
            return View(documentationProcess);
        }

        // GET: DocumentationProcesses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documentationProcess = await _context.DocumentationProcess
                .Include(d => d.FkDocuments)
                .Include(d => d.FkEmployees)
                .Include(d => d.FkUploadedBy)
                .FirstOrDefaultAsync(m => m.DocumentationProcessId == id);
            if (documentationProcess == null)
            {
                return NotFound();
            }

            return View(documentationProcess);
        }

        // POST: DocumentationProcesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documentationProcess = await _context.DocumentationProcess.FindAsync(id);
            _context.DocumentationProcess.Remove(documentationProcess);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentationProcessExists(int id)
        {
            return _context.DocumentationProcess.Any(e => e.DocumentationProcessId == id);
        }
    }
}
