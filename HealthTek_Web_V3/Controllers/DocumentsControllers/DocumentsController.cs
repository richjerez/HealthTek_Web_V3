using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Documents = HealthTek_Shared_Libraries.Documents;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "FileDropboxViews")]
    public class DocumentsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _hostEnv;
        private readonly ExternalLists externalLists = new ExternalLists();
        public DocumentsController(IdentityContext context, UserManager<AppUser> userManager, IWebHostEnvironment hostEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostEnv = hostEnv;
        }
        public async Task<JsonResult> GetLists(string id, string type, string role)
        {
            switch (type)
            {
                case "HR Chart":
                    var docs = new List<RoleDocsCatalog>();
                    if (role == null)
                    {
                        var roles = _context.Employees.Where(m => m.EmployeesId == id).Select(m => m.EmployeesRoleNames.FirstOrDefault()).Select(n => n.FkRoleNames.RoleName).FirstOrDefault();
                        docs = _context.RoleDocsCatalog.Where(m => m.Roles.Contains(roles)).ToList();
                    }
                    else
                    {
                        var roles = _context.Employees.Where(m => m.EmployeesId == id).Include(m => m.EmployeesRoleNames)
                            .ThenInclude(m => m.FkRoleNames).Select(m => m.EmployeesRoleNames.Where(e => e.FkRoleNames.RoleName.Contains(role))
                            .FirstOrDefault()).Select(n => n.FkRoleNames).FirstOrDefault();
                        docs = _context.RoleDocsCatalog.Where(m => m.Roles.Contains(roles.RoleName)).ToList();
                    }
                    return Json(new SelectList(docs, "Title", "Title"));
                case "Intake Chart":
                    var clientId = Int32.Parse(id);
                    var intakes = await _context.Intakes.Include(m => m.IntakeDocumentation).FirstOrDefaultAsync(i => i.FkClientsId == clientId);
                    if (intakes != null && intakes.IntakeDocumentation != null)
                    {
                        return Json(new SelectList(intakes.IntakeDocumentation, "DocumentTitle", "DocumentTitle"));
                    }
                    var errorModel = new { error = "There was an error" };
                    return new JsonResult(errorModel, HttpStatusCode.InternalServerError);
                case "Clinical Chart":
                    return Json(new { data = "ok" });
            }
            return Json(new { });

        }

        // GET: Documents
        [Route("File-Inbox")]
        [Authorize(Policy = "FileInboxViews")]
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.Documents.Where(d => d.IsSorted == false && d.DocumentUrl != null).Include(d => d.FkUploadedBy);
            return View(await identityContext.ToListAsync());
        }
        public async Task<IActionResult> Sort(int id)
        {
            var docs = await _context.Documents.FindAsync(id);
            docs.IsSorted = true;
            _context.Documents.Update(docs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documents = await _context.Documents
                .Include(d => d.FkClients)
                .Include(d => d.FkUploadedBy)
                .FirstOrDefaultAsync(m => m.DocumentsId == id);
            if (documents == null)
            {
                return NotFound();
            }
            UploadFileHelper readfile = new UploadFileHelper(_hostEnv);
            string file = await readfile.ReadFile(documents.DocumentUrl);
            var url = documents.DocumentUrl.Split(".")[1];
            switch (url)
            {
                case "docx":
                    var html = await new DocxToPdf().Write(file);
                    ViewData["DocumentUrl"] = html;
                    break;
                case "xlsx":
                    ViewData["DocumentUrl"] = new ReadExcel().ReadExcelFile(file);
                    break;
            }

            return View(documents);
        }

        [Authorize(Policy = "FileDropboxViews")]
        public IActionResult FileDropbox(int id)
        {
            Documents documents = new Documents();
            documents.FkClientsId = id;
            documents.DocumentType = "Client";
            ViewData["Status"] = new SelectList(externalLists.DocumentStatuses);

            return PartialView(documents);
        }

        // GET: Documents/Create
        [Route("File-Dropbox")]
        [Authorize(Policy = "FileDropboxViews")]
        public async Task<IActionResult> Create()
        {
            UploadFileHelper uploadFile = new UploadFileHelper(_hostEnv);
            var user = await _userManager.GetUserAsync(User);
            var identityContext = _context.Documents.Where(d => d.DocumentStatus == "Uploaded").Include(d => d.FkUploadedBy);
            ViewData["Types"] = new SelectList(externalLists.DocumentTypes);
            return View(await identityContext.ToListAsync());
        }
        public async Task<IActionResult> UploadDocuments([FromForm] Documents documents)
        {
            foreach (var file in documents.customFiles)
            {
                var ext = Path.GetExtension(file.FileName);
                if (!externalLists.AllowedDocs.Contains(ext))
                {
                    ModelState.AddModelError("customFiles", "This document(s) is(are) not allowed to upload!");
                    foreach (var ModelState in ViewData.ModelState.Values)
                    {
                        foreach (var ModelErrors in ModelState.Errors)
                        {
                            string errormessage = ModelErrors.ErrorMessage;
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                var statusId = "Uploaded";
                var user = await _userManager.GetUserAsync(User);
                foreach (var docs in documents.customFiles)
                {
                    if (documents.customFiles != null)
                    {
                        UploadFileHelper uploadFile = new UploadFileHelper(_hostEnv);
                        var path = await uploadFile.UploadFileAsync(docs, "unsorted", false);
                        documents.CreationDate = DateTime.Now;
                        documents.LastUpdateDate = DateTime.Now;
                        documents.FkUploadedById = user.FkEmployeesId;
                        documents.IsRequired = false;
                        if (documents.DocumentTitle == null)
                        {
                            documents.DocumentTitle = path;
                        }
                        documents.DocumentStatus = statusId;
                        documents.FkIntakesId = null;
                        documents.DocumentsId = 0;
                        documents.DocumentUrl = path;
                        _context.Documents.Add(documents);
                        await _context.SaveChangesAsync();
                    }

                }

                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", documents) });
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddIntakes([FromForm] Documents documents)
        {
            ModelState["customFiles"].Errors.Clear();
            ModelState["customFiles"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                documents.CreationDate = DateTime.Now;
                documents.LastUpdateDate = DateTime.Now;
                var user = await _userManager.GetUserAsync(User);
                var client = await _context.Clients.FindAsync(documents.FkClientsId);
                documents.FkUploadedById = user.FkEmployeesId;
                if (documents.customFiles != null)
                {
                    documents.IsAttached = true;
                    UploadFileHelper uploadFile = new UploadFileHelper(_hostEnv);
                    await uploadFile.UploadFileAsync(documents.customFiles.FirstOrDefault(), client.DocumentIdentifier, true, documents.DocumentTitle);
                    documents.DocumentUrl = documents.customFiles.FirstOrDefault().FileName;
                }
                documents.DocumentType = "Intake";
                _context.Documents.Add(documents);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkClientsId"] = new SelectList(_context.Clients, "ClientsId", "LastName", documents.FkClientsId);
            ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "EmployeesId", documents.FkEmployeesId);
            ViewData["Status"] = new SelectList(externalLists.AuthorizationStatuses, documents.DocumentStatus);
            ViewData["Types"] = new SelectList(externalLists.DocumentTypes, documents.DocumentType);
            ViewData["FkUploadedById"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "EmployeesId", documents.FkUploadedById);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "AddIntakes", documents) });
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documents = await _context.Documents.Where(s => s.DocumentsId == id).FirstOrDefaultAsync();
            if (documents == null)
            {
                return NotFound();
            }
            ViewData["FkUploadedById"] = new SelectList(_context.Employees, "EmployeesId", "EmployeeLabel", documents.FkUploadedById);
            ViewData["ActiveEmployees"] = new SelectList(_context.Employees.Where(m => m.EmployeeStatus == "Active").ToList(), "EmployeesId", "EmployeeLabel", documents.FkEmployeesId);
            ViewData["ActivePatients"] = new SelectList(_context.Clients.Where(m => m.ClientStatus == "Active").ToList(), "ClientsId", "FullName", documents.FkClientsId);
            ViewData["Status"] = new SelectList(externalLists.AuthorizationStatuses, documents.DocumentStatus);
            ViewData["Types"] = new SelectList(externalLists.DocumentTypes, documents.DocumentType);
            UploadFileHelper readfile = new UploadFileHelper(_hostEnv);
            string file = await readfile.ReadFile(documents.DocumentUrl);
            var url = documents.DocumentUrl.Split(".")[1];
            switch (url)
            {
                case "docx":
                    var html = await new DocxToPdf().Write(file);
                    ViewData["DocumentUrl"] = html;
                    break;
                case "xlsx":
                    html = new ReadExcel().ReadExcelFile(file);
                    ViewData["DocumentUrl"] = html;
                    break;
            }
            return View(documents);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Documents documents)
        {
            if (id != documents.DocumentsId)
            {
                return NotFound();
            }
            if (documents.customFiles == null)
            {
                ModelState.Clear();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    documents.LastUpdateDate = DateTime.Now;
                    if (documents.DocumentUrl != String.Empty)
                    {

                    }
                    _context.Update(documents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentsExists(documents.DocumentsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                switch (documents.DocumentType)
                {
                    case "HR":
                        documents.IsSorted = true;
                        var emp = _context.Employees.Find(documents.FkEmployeesId);
                        if (emp != null)
                        {
                            emp.DocumentationProcess.Where(m => m.RoleDocsCatalogs.Title.Contains(documents.DocumentTitle)).FirstOrDefault().FkDocuments = documents;
                            _context.Employees.Update(emp);
                            _context.SaveChanges();
                        }
                        break;
                    case "Intake":
                        var client = _context.Clients.Find(documents.FkClientsId);
                        if (client != null)
                        {
                            documents = client.Intakes.Select(m => m.IntakeDocumentation.Where(m => m.DocumentTitle.Contains(documents.DocumentTitle)).FirstOrDefault()).FirstOrDefault();
                            documents.LastUpdateDate = DateTime.Now;
                            documents.IsSorted = true;
                        }
                        break;
                    case "Client":
                        documents.IsSorted = true;
                        break;
                }
                _context.Documents.Update(documents);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["FkClientsId"] = new SelectList(_context.Clients, "ClientsId", "LastName", documents.FkClientsId);
            ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "EmployeesId", documents.FkEmployeesId);
            ViewData["Status"] = new SelectList(externalLists.AuthorizationStatuses, documents.DocumentStatus);
            ViewData["Types"] = new SelectList(externalLists.DocumentTypes, documents.DocumentType);
            ViewData["FkUploadedById"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "EmployeesId", documents.FkUploadedById);
            return View(documents);
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var documents = await _context.Documents
                .FirstOrDefaultAsync(m => m.DocumentsId == id);
            if (documents == null)
            {
                return NotFound();
            }

            return PartialView(documents);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var documents = await _context.Documents.FindAsync(id);
            UploadFileHelper deleteFile = new UploadFileHelper(_hostEnv);
            await deleteFile.DeleteFile(documents.DocumentUrl);
            _context.Documents.Remove(documents);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> DownloadDocument(int id)
        {
            var documents = await _context.Documents.FindAsync(id);
            UploadFileHelper deleteFile = new UploadFileHelper(_hostEnv);
            await deleteFile.DownloadFileAsync(documents.DocumentUrl);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool DocumentsExists(int id)
        {
            return _context.Documents.Any(e => e.DocumentsId == id);
        }
    }
}
