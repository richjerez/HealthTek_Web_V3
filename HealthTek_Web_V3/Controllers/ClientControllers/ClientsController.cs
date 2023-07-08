using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.ClientControllers
{
    [Authorize(Policy = "ClientViews")]
    public class ClientsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();
        private readonly IWebHostEnvironment _hostEnv;
        private readonly UserManager<AppUser> _userManager;

        public ClientsController(IdentityContext context, IWebHostEnvironment hostEnv, UserManager<AppUser> userManager)
        {
            _context = context;
            _hostEnv = hostEnv;
            _userManager = userManager;
        }
        // GET: Clients
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var clients = new List<Clients>();
            if (await _userManager.IsInRoleAsync(user, "USER"))
            {
                clients = await _context.Assignments
                    .Include(m => m.FkClients)
                    .ThenInclude(m => m.ClientsFacilities)
                    .ThenInclude(m => m.FkFacilities)
                    .AsSplitQuery()
                .Include(c => c.FkClients)
                .ThenInclude(c => c.Authorizations)
                .ThenInclude(m => m.AuthorizationNotes)
                .Where(m => m.FkEmployeesId == user.FkEmployeesId && m.AssignmentPosition != null)
                .Select(m => m.FkClients)
                .ToListAsync();
            }
            else
            {
                clients = await _context.Clients
                .Include(c => c.ClientsFacilities)
                .ThenInclude(m => m.FkFacilities)
                .AsSplitQuery()
                .Include(c => c.Authorizations)
                .ThenInclude(m => m.AuthorizationNotes).ToListAsync();
            }
            ViewData["Status"] = new SelectList(externalLists.ClientStatuses);
            return View(clients);
        }

        [Route("Clients/Profile/{id}/{TabName?}")]
        public async Task<IActionResult> Details(int? id, string? TabName)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clients = new Clients();
            List<TabModel> model = new List<TabModel>();
            model.Add(new TabModel { Name = "Client Details", Active = "active" });
            model.Add(new TabModel { Name = "Files", Active = "" });
            model.Add(new TabModel { Name = "Authorizations", Active = "" });
            model.Add(new TabModel { Name = "Appointments", Active = "" });
            model.Add(new TabModel { Name = "Services", Active = "" });
            model.Add(new TabModel { Name = "Program", Active = "" });
            model.Add(new TabModel { Name = "Caregiver", Active = "" });
            model.Add(new TabModel { Name = "Comments", Active = "" });
            if (TabName == null || TabName == string.Empty)
            {
                TabName = "Client Details";
            }
            switch (TabName)
            {
                #region Client Details
                case "Client Details":
                    clients = await _context.Clients
                        .Include(c => c.Caregivers)
                        .Include(c => c.Diagnosis)
                        .Include(c => c.Medications)
                        .Include(c => c.Locations)
                        .Include(c => c.ClientInsurances)
                        .Include(c => c.Authorizations)
                        .Include(c => c.Intakes)
                        .Include(c => c.Assignments)
                        .ThenInclude(c => c.FkEmployees)
                        .AsSplitQuery()
                        .Include(c => c.ClientContacts)
                        .ThenInclude(c => c.FkLocations)
                        .AsSplitQuery()
                        .Include(c => c.ClientsFacilities)
                        .ThenInclude(c => c.FkFacilities)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(m => m.ClientsId == id);
                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Client Details").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Client Details";
                    ViewData["Table"] = clients;
                    break;
                #endregion
                #region Appointments
                case "Appointments":
                    clients = await _context.Clients
                        .Include(c => c.Caregivers)
                        .Include(c => c.Authorizations)
                        .Include(c => c.Intakes)
                        .Include(c => c.Locations)
                        .Include(c => c.ClientInsurances)
                        .Include(c => c.Assignments)
                        .ThenInclude(c => c.FkEmployees)
                        .AsSplitQuery()
                        .Include(c => c.Appointments)
                        .ThenInclude(c => c.FkServiceCodes)
                        .AsSplitQuery()
                        .Include(c => c.Appointments)
                        .ThenInclude(c => c.FkEmployees)
                        .AsSplitQuery()
                        .Include(c => c.ClientsFacilities)
                        .ThenInclude(c => c.FkFacilities)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(m => m.ClientsId == id);
                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Appointments").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Appointments";
                    ViewData["Table"] = clients.Appointments;
                    List<CalendarModel> Calendar = new List<CalendarModel>();
                    foreach (var item in clients.Appointments)
                    {
                        var serviceTitle = "";
                        if (item.AppointmentType == "Services")
                        {
                            serviceTitle = "Service: " + item.FkServiceCodes.CodeTitle +
                                "\nClient: " + item.FkClients.FullName + "\nStart: " + item.FkStartLocation.LocationName +
                                "\nEnd: " + item.FkEndLocation.LocationName;
                        }
                        Calendar.Add(new CalendarModel
                        {
                            id = item.AppointmentsId,
                            title = item.AppointmentType + "\n" + serviceTitle + "\n" + item.Description,
                            start = item.StartTime,
                            end = item.EndTime,
                            allDay = false,
                            className = item.ClassName,
                        });
                    }
                    ViewData["Calendar"] = Calendar;
                    break;
                #endregion
                #region Files
                case "Files":
                    clients = await _context.Clients
                        .Include(c => c.Assignments)
                        .ThenInclude(c => c.FkEmployees)
                        .AsSplitQuery()
                        .Include(c => c.Caregivers)
                        .Include(c => c.Documents)
                        .ThenInclude(c => c.FkUploadedBy)
                        .AsSplitQuery()
                        .Include(c => c.Locations)
                        .Include(c => c.ClientInsurances)
                        .Include(c => c.ClientsFacilities)
                        .ThenInclude(c => c.FkFacilities)
                        .AsSplitQuery()
                        .Include(c => c.Authorizations)
                        .Include(c => c.Intakes)
                        .FirstOrDefaultAsync(m => m.ClientsId == id);

                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Files").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Files";
                    ViewData["Table"] = clients.Documents;
                    break;
                #endregion
                #region Caregiver
                case "Caregiver":
                    clients = await _context.Clients
                        .Include(c => c.Assignments)
                        .ThenInclude(c => c.FkEmployees)
                        .AsSplitQuery()
                        .Include(c => c.Locations)
                        .Include(c => c.Caregivers)
                        .ThenInclude(c => c.Locations)
                        .AsSplitQuery()
                        .Include(c => c.ClientInsurances)
                        .Include(c => c.ClientsFacilities)
                        .ThenInclude(c => c.FkFacilities)
                        .AsSplitQuery()
                        .Include(c => c.Authorizations)
                        .Include(c => c.Intakes)
                        .FirstOrDefaultAsync(m => m.ClientsId == id);

                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Caregiver").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Caregiver";
                    ViewData["Table"] = clients.Caregivers;
                    break;
                #endregion
                #region Authorizations
                case "Authorizations":
                    clients = await _context.Clients
                        .Include(c => c.Assignments)
                        .ThenInclude(c => c.FkEmployees)
                        .AsSplitQuery()
                        .Include(c => c.Caregivers)
                        .Include(c => c.Locations)
                        .Include(c => c.ClientInsurances)
                        .Include(c => c.ClientsFacilities)
                        .ThenInclude(c => c.FkFacilities)
                        .AsSplitQuery()
                        .Include(c => c.Authorizations)
                        .ThenInclude(c => c.FkServiceCodes)
                        .AsSplitQuery()
                        .Include(c => c.Intakes)
                        .FirstOrDefaultAsync(m => m.ClientsId == id);

                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Authorizations").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Authorizations";
                    ViewData["Table"] = clients.Authorizations;
                    break;
                #endregion
                #region Program
                case "Program":
                    clients = await _context.Clients
                        .Include(c => c.Authorizations)
                        .Include(c => c.Intakes)
                        .Include(c => c.Locations)
                        .Include(c => c.ClientInsurances)
                        .Include(c => c.Caregivers)
                        .Include(c => c.Assignments)
                        .ThenInclude(c => c.FkEmployees)
                        .AsSplitQuery()
                        .Include(c => c.Maladaptives)
                        .ThenInclude(c => c.FkReplacements)
                        .AsSplitQuery()
                        .Include(c => c.Maladaptives)
                        .ThenInclude(c => c.LongTermObjectives)
                        .AsSplitQuery()
                        .Include(c => c.Maladaptives)
                        .ThenInclude(c => c.ShortTermObjectives)
                        .AsSplitQuery()
                        .Include(c => c.Maladaptives)
                        .ThenInclude(c => c.FunctionsList)
                        .AsSplitQuery()
                        .Include(c => c.Preferences)
                        .ThenInclude(c => c.FkReinforcersCatalog)
                        .AsSplitQuery()
                        .Include(c => c.ClientsFacilities)
                        .ThenInclude(c => c.FkFacilities)
                        .AsSplitQuery()
                        .FirstOrDefaultAsync(m => m.ClientsId == id);
                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Program").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Program";
                    ViewData["Table"] = clients;
                    ViewData["STOStatus"] = new SelectList(externalLists.BehaviorStatuses);
                    break;
                #endregion
                #region Comments
                case "Comments":
                    clients = await _context.Clients
                        .Include(c => c.Assignments)
                        .ThenInclude(c => c.FkEmployees)
                        .AsSplitQuery()
                        .Include(c => c.Caregivers)
                        .Include(c => c.Comments)
                        .ThenInclude(c => c.FkEmployees)
                        .AsSplitQuery()
                        .Include(c => c.Locations)
                        .Include(c => c.ClientInsurances)
                        .Include(c => c.ClientsFacilities)
                        .ThenInclude(c => c.FkFacilities)
                        .AsSplitQuery()
                        .Include(c => c.Authorizations)
                        .Include(c => c.Intakes)
                        .FirstOrDefaultAsync(m => m.ClientsId == id);
                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Comments").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Comments";
                    ViewData["Table"] = clients.Comments;
                    break;
                #endregion
                #region Services
                case "Services":
                    clients = await _context.Clients
                .Include(c => c.Caregivers)
                .Include(c => c.Locations)
                .Include(c => c.ClientInsurances)
                .Include(c => c.Authorizations)
                .Include(c => c.Intakes)
                .Include(c => c.Assignments)
                .ThenInclude(c => c.FkEmployees)
                .AsSplitQuery()
                .Include(c => c.ClientsFacilities)
                .ThenInclude(c => c.FkFacilities)
                .AsSplitQuery()
                .Include(e => e.Appointments)
                .ThenInclude(e => e.Supervisions)
                .ThenInclude(e => e.FkRbtCompetencies)
                .ThenInclude(e => e.RbtCompTrainings)
                .AsSplitQuery()
                .Include(e => e.Appointments)
                .ThenInclude(e => e.BaAssessments)
                .AsSplitQuery()
                .Include(e => e.Appointments)
                .ThenInclude(e => e.FkServiceCodes)
                .AsSplitQuery()
                .Include(e => e.Appointments)
                .ThenInclude(e => e.BaMonthlyReports)
                .AsSplitQuery()
                .Include(e => e.Appointments)
                .ThenInclude(e => e.BaProgressNotes)
                .ThenInclude(e => e.CaregiverCompetencies)
                .ThenInclude(e => e.CaregiverCompChecks)
                .ThenInclude(e => e.FkClients)
                .AsSplitQuery()
                .Include(e => e.Appointments)
                .ThenInclude(e => e.BaProgressNotes)
                .ThenInclude(e => e.CaregiverCompetencies)
                .ThenInclude(e => e.CaregiverCompChecks)
                .ThenInclude(e => e.FkCaregiverComptChecksCatalog)
                .AsSplitQuery()
    .FirstOrDefaultAsync(m => m.ClientsId == id);

                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Services").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Services";
                    ViewData["Table"] = clients;
                    break;
                    #endregion

            }
            if (clients == null)
            {
                return NotFound();
            }
            ViewData["Tabs"] = model;
            ViewData["ID"] = clients.ClientsId;
            ViewData["ClientStatus"] = new SelectList(externalLists.ClientStatuses);
            var key = "WP46C8DF276ND5931069BDE2E695D45E";
            var decrypt = clients.Ssn;
            clients.Ssn = DataEncryption.DecryptString(decrypt, key);

            return View(clients);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            ViewData["Languages"] = externalLists.Languages.ConvertAll(a => { return new SelectListItem() { Text = a.EnglishName.ToString(), Value = a.Name.ToString(), Selected = false }; });
            ViewData["Ethnicities"] = new SelectList(externalLists.Ethnicities);
            ViewData["SchoolLevel"] = new SelectList(externalLists.SchoolLevel);
            ViewData["Religions"] = new SelectList(externalLists.Religions);
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr");
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName");
            ViewData["Status"] = new SelectList(externalLists.ClientStatuses);
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Clients clients, ClientsFacilities facilities, Locations locations)
        {
            if (ModelState.IsValid)
            {
                // Set creation and lastupdate dates to now
                facilities.CreationDate = DateTime.Now;
                facilities.LastUpdateDate = DateTime.Now;
                clients.CreationDate = DateTime.Now;
                clients.LastUpdateDate = DateTime.Now;
                locations.CreationDate = DateTime.Now;
                locations.LastUpdateDate = DateTime.Now;
                locations.LocationName = "Home";
                if (clients.ListLanguages != null)
                    clients.Languages = String.Join(",", clients.ListLanguages.ToArray());
                locations.LocationName = "Home";

                // Add Location to Clients Locations
                _context.Locations.Add(locations);
                await _context.SaveChangesAsync();

                // Add Clients to DB
                _context.Clients.Add(clients);
                await _context.SaveChangesAsync();

                facilities.FkClientsId = clients.ClientsId;

                // Add facilities to Clients ClientFacilities
                if (facilities.ClientsFacilitiesId != 0)
                {
                    _context.ClientsFacilities.Add(facilities);
                    await _context.SaveChangesAsync();

                }
                clients.Locations.Add(locations);
                if (clients.customFile != null)
                {
                    UploadProfileImageHelper uploadFile = new UploadProfileImageHelper(_hostEnv);
                    var id = Guid.NewGuid().ToString();
                    await uploadFile.UploadFileAsync(clients.customFile, true, id);
                    clients.AvatarUrl = "/profileImgs/" + id + clients.customFile.FileName.Substring(clients.customFile.FileName.LastIndexOf("."));
                }
                var key = "WP46C8DF276ND5931069BDE2E695D45E";
                var encrypt = clients.Ssn;
                clients.Ssn = DataEncryption.EncryptString(encrypt, key);

                _context.Clients.Update(clients);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            ExternalLists externalLists = new ExternalLists();
            var langs = externalLists.Languages.ConvertAll(a => { return new SelectListItem() { Text = a.EnglishName.ToString(), Value = a.Name.ToString(), Selected = false }; });
            ViewData["Ethnicities"] = new SelectList(externalLists.Ethnicities, clients.Ethnicity);
            ViewData["SchoolLevel"] = new SelectList(externalLists.SchoolLevel, clients.SchoolLevel);
            ViewData["Religions"] = new SelectList(externalLists.Religions, clients.Religion);
            ViewData["Countries"] = new SelectList(externalLists.Countries, locations.Country);
            ViewData["States"] = new SelectList(externalLists.States, locations.State);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", facilities.FkFacilitiesId);
            ViewData["Status"] = new SelectList(externalLists.ClientStatuses, clients.ClientStatus);
            ViewData["States"] = new SelectList(externalLists.States, locations.State);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities, locations.City);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", locations.County);
            foreach (var item in langs)
            {
                if (clients.Languages != null)
                {
                    if (clients.Languages.Contains(item.Value) && item.Value != string.Empty)
                    {
                        item.Selected = true;
                    }

                }
            }
            ViewData["Languages"] = langs;
            return View(clients);
        }

        // GET: Clients/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = _context.Clients.Include(m => m.Locations).Include(f => f.ClientsFacilities).FirstOrDefault(i => i.ClientsId == id);
            if (clients == null)
            {
                return NotFound();
            }
            var key = "WP46C8DF276ND5931069BDE2E695D45E";
            var decrypt = clients.Ssn;
            clients.Ssn = DataEncryption.DecryptString(decrypt, key);

            var langs = externalLists.Languages.ConvertAll(a => { return new SelectListItem() { Text = a.EnglishName.ToString(), Value = a.Name.ToString(), Selected = false }; });
            ViewData["Languages"] = externalLists.Languages.ConvertAll(a => { return new SelectListItem() { Text = a.EnglishName.ToString(), Value = a.Name.ToString(), Selected = false }; });
            ViewData["Ethnicities"] = new SelectList(externalLists.Ethnicities);
            ViewData["SchoolLevel"] = new SelectList(externalLists.SchoolLevel);
            ViewData["Religions"] = new SelectList(externalLists.Religions);
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Countries"] = new SelectList(externalLists.Countries);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName");
            ViewData["Status"] = new SelectList(externalLists.ClientStatuses);
            if (clients.Languages != null && clients.Languages != string.Empty)
            {
                foreach (var item in langs)
                {
                    if (clients.Languages.Contains(item.Value) && item.Value != string.Empty)
                    {
                        item.Selected = true;
                    }
                }
            }
            ViewData["Languages"] = langs;
            if (clients.Locations.Count > 0)
            {
                ViewData["States"] = new SelectList(externalLists.States, clients.Locations.FirstOrDefault().State);
                ViewData["Cities"] = new SelectList(externalLists.FloridaCities, clients.Locations.FirstOrDefault().City);
                ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", clients.Locations.FirstOrDefault().County);
            }
            else
            {
                ViewData["States"] = new SelectList(externalLists.States);
                ViewData["Cities"] = new SelectList(externalLists.FloridaCities);
                ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr");
            }
            return View(clients);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Clients clients, ClientsFacilities facilities, Locations locations)
        {
            if (id != clients.ClientsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Set creation and lastupdate dates to now
                    facilities.LastUpdateDate = DateTime.Now;
                    clients.LastUpdateDate = DateTime.Now;
                    locations.LastUpdateDate = DateTime.Now;
                    if(clients.ListLanguages != null)
                    {
                        clients.Languages = String.Join(",", clients.ListLanguages.ToArray());
                    }
                    facilities.FkClientsId = clients.ClientsId;
                    locations.FkClientsId = clients.ClientsId;
                    var key = "WP46C8DF276ND5931069BDE2E695D45E";
                    var decrypt = clients.Ssn;
                    clients.Ssn = DataEncryption.EncryptString(decrypt, key);

                    if (clients.customFile != null)
                    {
                        UploadProfileImageHelper uploadFile = new UploadProfileImageHelper(_hostEnv);
                        var newid = Guid.NewGuid().ToString();
                        await uploadFile.UploadFileAsync(clients.customFile, true, newid);
                        clients.AvatarUrl = "/profileImgs/" + newid + clients.customFile.FileName.Substring(clients.customFile.FileName.LastIndexOf("."));
                    }
                    if (locations.LocationsId != 0)
                    {
                        _context.Locations.Update(locations);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        locations.CreationDate = DateTime.Now;
                        locations.LocationName = "Home";
                        _context.Locations.Add(locations);
                        await _context.SaveChangesAsync();
                    }
                    if (facilities.ClientsFacilitiesId != 0)
                    {
                        _context.ClientsFacilities.Update(facilities);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        facilities.CreationDate = DateTime.Now;
                        _context.ClientsFacilities.Add(facilities);
                        await _context.SaveChangesAsync();
                    }
                    _context.Clients.Update(clients);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsExists(clients.ClientsId))
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
            ExternalLists externalLists = new ExternalLists();
            var langs = externalLists.Languages.ConvertAll(a => { return new SelectListItem() { Text = a.EnglishName.ToString(), Value = a.Name.ToString(), Selected = false }; });
            ViewData["Ethnicities"] = new SelectList(externalLists.Ethnicities);
            ViewData["SchoolLevel"] = new SelectList(externalLists.SchoolLevel);
            ViewData["Religions"] = new SelectList(externalLists.Religions);
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Countries"] = new SelectList(externalLists.Countries);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName");
            ViewData["Status"] = new SelectList(externalLists.ClientStatuses);
            foreach (var item in langs)
            {
                if (clients.Languages != null)
                {
                    if (clients.Languages.Contains(item.Value) && item.Value != string.Empty)
                    {
                        item.Selected = true;
                    }

                }
            }
            ViewData["Languages"] = langs;
            ViewData["States"] = new SelectList(externalLists.States, locations.State);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities, locations.City);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr");
            return View(clients);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clients = await _context.Clients
                .FindAsync(id);
            if (clients == null)
            {
                return NotFound();
            }

            return PartialView(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clients = _context.Clients.Include(m => m.ClientsFacilities).Include(m => m.Authorizations).ThenInclude(a => a.AuthorizationNotes).Include(m => m.Locations).FirstOrDefault(m => m.ClientsId == id);
            _context.Clients.Remove(clients);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientsExists(int id)
        {
            return _context.Clients.Any(e => e.ClientsId == id);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeClientStatus(int? ClientsId, string ClientStatus)
        {
            if (ClientsId == null)
            {
                return NotFound();
            }
            var clients = await _context.Clients.FindAsync(ClientsId);

            if (ModelState.IsValid)
            {
                try
                {
                    // Set creation and lastupdate dates to now
                    clients.LastUpdateDate = DateTime.Now;
                    clients.ClientStatus = ClientStatus;
                    // Add Update to DB
                    _context.Clients.Update(clients);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Clients", new { id = clients.ClientsId });

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsExists(clients.ClientsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["Status"] = new SelectList(externalLists.EmployeeStatuses, clients.ClientStatus);
            return View(clients);
        }

    }
}
