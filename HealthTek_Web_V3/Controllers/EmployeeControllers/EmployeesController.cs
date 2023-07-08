using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{

    public class EmployeesController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<EmployeesController> _logger;
        private readonly EmailSender _emailSender;
        private readonly ExternalLists externalLists = new ExternalLists();
        private readonly IWebHostEnvironment _hostEnv;

        public EmployeesController(IdentityContext context, UserManager<AppUser> userManager,
            ILogger<EmployeesController> logger, EmailSender emailSender, IWebHostEnvironment hostEnv)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _hostEnv = hostEnv;
        }
        [Authorize(Policy = "EmployeeViews")]
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.Employees.Include(e => e.Locations);
            return View(await identityContext.ToListAsync());
        }

        [Route("Employee/Profile/{id}/{table?}")]
        [Authorize]
        public async Task<IActionResult> Details(string? id, string table)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = new Employees();
            var user = _context.Users.Where(m => m.FkEmployeesId == id).AsNoTracking().FirstOrDefault();

            List<TabModel> model = new List<TabModel>();
            model.Add(new TabModel { Name = "HR Chart", Active = "active" });
            model.Add(new TabModel { Name = "Appointments", Active = "" });
            model.Add(new TabModel { Name = "Tasks", Active = "" });
            model.Add(new TabModel { Name = "Batches", Active = "" });
            if (table == null || table == string.Empty)
            {
                table = "HR Chart";
            }
            switch (table)
            {
                #region HR Chart
                case "HR Chart":
                    employees = _context.Employees
                .Include(e => e.FkESignatures)
                .Include(e => e.TasksFkAssignedTo)
                .Include(e => e.Assignments)
                .ThenInclude(e => e.FkClients)
                .AsSplitQuery()
                .Include(e => e.Appointments)
                .ThenInclude(e => e.FkBatches)
                .AsSplitQuery()
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.RoleDocsCatalogs)
                .AsSplitQuery()
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.FkDocuments)
                .AsSplitQuery()
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.FkEmployees)
                .AsSplitQuery()
                .Include(e => e.Locations)
                .Include(e => e.EmployeesFacilities)
                .ThenInclude(e => e.FkFacilities)
                .AsSplitQuery()
                .Include(e => e.EmployeesRoleNames)
                .ThenInclude(e => e.FkRoleNames)
                .AsSplitQuery()
                .Include(e => e.EmployeesOperatingCounties)
                .ThenInclude(e => e.FkOperatingCounties)
                .AsSplitQuery()
                .FirstOrDefault(m => m.EmployeesId == id);
                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "HR Chart").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "HR Chart";
                    ViewData["Table"] = employees;
                    break;
                #endregion
                #region Batches
                case "Batches":
                    employees = await _context.Employees
                .Include(e => e.FkESignatures)
                .Include(e => e.Appointments)
                .ThenInclude(e => e.FkBatches)
                .AsSplitQuery()
                .Include(e => e.Appointments)
                .ThenInclude(e => e.FkServiceCodes)
                .AsSplitQuery()
                .Include(e => e.Assignments)
                .ThenInclude(e => e.FkClients)
                .Include(e => e.TasksFkAssignedTo)
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.RoleDocsCatalogs)
                .AsSplitQuery()
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.FkDocuments)
                .AsSplitQuery()
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.FkEmployees)
                .AsSplitQuery()
                .Include(e => e.Locations)
                .Include(e => e.EmployeesFacilities)
                .ThenInclude(e => e.FkFacilities)
                .Include(e => e.EmployeesRoleNames)
                .ThenInclude(e => e.FkRoleNames)
                .Include(e => e.EmployeesOperatingCounties)
                .ThenInclude(e => e.FkOperatingCounties)
                .FirstOrDefaultAsync(m => m.EmployeesId == id);
                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Batches").FirstOrDefault().Active = "active";
                    var batches = employees.Appointments.ToList();
                    ViewData["TableName"] = "Batches";
                    ViewData["Table"] = batches;
                    break;
                #endregion
                #region Tasks
                case "Tasks":
                    employees = await _context.Employees
                .Include(e => e.FkESignatures)
                .Include(e => e.Assignments)
                .ThenInclude(e => e.FkClients)
                .AsSplitQuery()
                .Include(e => e.Appointments)
                .ThenInclude(e => e.FkBatches)
                .AsSplitQuery()
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.RoleDocsCatalogs)
                .AsSplitQuery()
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.FkDocuments)
                .AsSplitQuery()
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.FkEmployees)
                .AsSplitQuery()
                .Include(e => e.TasksFkAssignedTo)
                .Include(e => e.Locations)
                .Include(e => e.EmployeesFacilities)
                .ThenInclude(e => e.FkFacilities)
                .Include(e => e.EmployeesRoleNames)
                .ThenInclude(e => e.FkRoleNames)
                .Include(e => e.EmployeesOperatingCounties)
                .ThenInclude(e => e.FkOperatingCounties)
                .FirstOrDefaultAsync(m => m.EmployeesId == id);
                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Tasks").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Tasks";
                    ViewData["Table"] = employees.TasksFkAssignedBy;
                    break;
                #endregion
                #region Appointments
                case "Appointments":
                    employees = await _context.Employees
                    .Include(e => e.FkESignatures)
                    .Include(e => e.TasksFkAssignedTo)
                    .Include(e => e.Assignments)
                    .ThenInclude(e => e.FkClients)
                    .AsSplitQuery()
                .Include(e => e.Appointments)
                .ThenInclude(e => e.FkBatches)
                .AsSplitQuery()
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.RoleDocsCatalogs)
                .AsSplitQuery()
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.FkDocuments)
                .AsSplitQuery()
                .Include(e => e.DocumentationProcess)
                .ThenInclude(e => e.FkEmployees)
                .AsSplitQuery()
                    .Include(e => e.Appointments)
                    .ThenInclude(e => e.FkServiceCodes)
                    .Include(e => e.Appointments)
                    .ThenInclude(e => e.FkEndLocation)
                    .Include(e => e.Appointments)
                    .ThenInclude(e => e.FkClients)
                    .Include(e => e.Appointments)
                    .ThenInclude(e => e.FkStartLocation)
                    .Include(e => e.Locations)
                    .Include(e => e.EmployeesFacilities)
                    .ThenInclude(e => e.FkFacilities)
                    .Include(e => e.EmployeesRoleNames)
                    .ThenInclude(e => e.FkRoleNames)
                    .Include(e => e.EmployeesOperatingCounties)
                    .ThenInclude(e => e.FkOperatingCounties)
                    .FirstOrDefaultAsync(m => m.EmployeesId == id);
                    foreach (var i in model)
                    {
                        i.Active = "";
                    }
                    model.Where(m => m.Name == "Appointments").FirstOrDefault().Active = "active";
                    ViewData["TableName"] = "Appointments";
                    ViewData["Table"] = employees.Appointments;
                    List<CalendarModel> Calendar = new List<CalendarModel>();
                    foreach (var item in employees.Appointments)
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
            }
            #region Financial
            decimal avg = 0;
            decimal total = 0;
            if (employees.Appointments.Count > 0)
            {
                foreach (var batch in employees.Appointments)
                {
                    total += (decimal)batch.BillingUnits * employees.EmployeeRate.Value;
                }
                avg = (decimal)(total / employees.Appointments.Count);
            }
            #endregion
            #region HR Requierements
            var exp = 0;
            var expDoc = 0;
            var uploaded = "";
            if (employees.DocumentationProcess.Count > 0)
            {
                exp = employees.DocumentationProcess.Where(m => m.FkDocuments != null
                && m.FkDocuments.DocumentExpirationDate != null
                && m.FkDocuments.DocumentExpirationDate < DateTime.Now
                && m.RoleDocsCatalogs.NeverExpires == false).Count();
                expDoc = employees.DocumentationProcess.Where(m => m.FkDocuments != null
                && m.RoleDocsCatalogs.NeverExpires == false).Count();
                uploaded = employees.DocumentationProcess.Where(m => m.FkDocuments != null).Count() + " / " + employees.DocumentationProcess.Count();
            }
            #endregion
            #region SelectLists
            var langs = externalLists.Languages.ConvertAll(a => { return new SelectListItem() { Text = a.EnglishName.ToString(), Value = a.Name.ToString(), Selected = false }; });
            List<string> list = new List<string>();
            foreach (var item in langs)
            {
                if (employees.Languages.Contains(item.Value) && item.Value != string.Empty)
                {
                    list.Add(item.Text);
                }
            }
            ViewData["OpenTasks"] = employees.TasksFkAssignedTo.Where(m => m.TaskStatus != "Archived" && m.TaskStatus != "Completed" && m.CompletedDate == null).Count();
            ViewData["ExpiredDocs"] = exp;
            ViewData["ExpiringDocs"] = expDoc;
            ViewData["UploadedDocs"] = uploaded;
            ViewData["Tabs"] = model;
            ViewData["Avg"] = avg;
            ViewData["Total"] = total;
            ViewData["Status"] = new SelectList(externalLists.EmployeeStatuses, employees.EmployeeStatus);
            #endregion

            var key = "WP46C8DF276ND5931069BDE2E695D45E";
            var decrypt = employees.Ssn;
            employees.Ssn = DataEncryption.DecryptString(decrypt, key);
            employees.Languages = string.Join(", ", list.ToArray());
            return View(employees);
        }

        [Authorize(Policy = "EmployeeViews")]
        public IActionResult Create()
        {
            ViewData["Ethnicities"] = new SelectList(externalLists.Ethnicities);
            ViewData["SchoolLevel"] = new SelectList(externalLists.SchoolLevel);
            ViewData["Religions"] = new SelectList(externalLists.Religions);
            ViewData["States"] = new SelectList(externalLists.States, "Florida");
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities);
            ViewData["Facilities"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName");
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr");
            ViewData["RoleNames"] = new SelectList(_context.Set<RoleNames>(), "RoleNamesId", "RoleName");
            ViewData["Status"] = new SelectList(externalLists.EmployeeStatuses);
            ViewData["Languages"] = externalLists.Languages.ConvertAll(a => { return new SelectListItem() { Text = a.EnglishName.ToString(), Value = a.Name.ToString(), Selected = false }; });
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "EmployeeViews")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Employees employees, EmployeesFacilities facilities)
        {
            if (ModelState.IsValid)
            {
                // Set creation and lastupdate dates to todays date 
                employees.CreationDate = DateTime.Now;
                employees.LastUpdateDate = DateTime.Now;
                employees.Locations.CreationDate = DateTime.Now;
                employees.Locations.LastUpdateDate = DateTime.Now;
                employees.Locations.LocationName = "Home";
                employees.EmployeesId = Guid.NewGuid().ToString();
                employees.Languages = String.Join(",", employees.ListLanguages.ToArray());

                var key = "WP46C8DF276ND5931069BDE2E695D45E";
                var encrypt = employees.Ssn;
                employees.Ssn = DataEncryption.EncryptString(encrypt, key);

                // Add Clients to DB
                _context.Employees.Add(employees);
                await _context.SaveChangesAsync();
                if (employees.CustomFile != null)
                {
                    UploadProfileImageHelper uploadFile = new UploadProfileImageHelper(_hostEnv);
                    await uploadFile.UploadFileAsync(employees.CustomFile, true, employees.EmployeesId);
                    employees.AvatarUrl = "/profileImgs/" + employees.EmployeesId + employees.CustomFile.FileName.Substring(employees.CustomFile.FileName.LastIndexOf("."));

                }

                // Create User with FkEmployeeId
                var user = new AppUser
                {
                    UserName = employees.FirstName + employees.LastName,
                    Email = employees.Email,
                    FkEmployeesId = employees.EmployeesId,
                    LockoutEnabled = true,
                    EmailConfirmed = true,
                    PhoneNumber = employees.PhoneNumber,
                    Avatar = employees.AvatarUrl ?? "../../img/profile.svg"
                };
                // User password is a new guid 
                var result = await _userManager.CreateAsync(user, "H" + Guid.NewGuid().ToString());
                if (result.Succeeded)
                {
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code },
                        protocol: Request.Scheme);

                    Messages emailModel = new Messages();
                    emailModel.ToEmail = employees.Email;
                    emailModel.Title = "Account Confirmation";
                    emailModel.Message = $"<img src='https://i.ibb.co/PWd0zVT/undraw-unexpected-friends-tg6k.png' alt='HealthTek' style='width: 300px;margin: auto;display: block; '/> <span style='text-align:center'> Please confirm your account by clicking the link below and proceed to change your password.</span> <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' style='display: inline-block;text-align: center;border: solid #5300ff .01rem;padding: 10px;text-decoration: none;border-radius: 5px;background: #5300ff;color: white;'>Click Here</a>.";
                    await _emailSender.SendMessage(emailModel);

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                // Add facilities to Employees EmployeesFacilities
                if (employees.Facilities != null)
                {
                    foreach (var item in employees.Facilities)
                    {
                        EmployeesFacilities facility = new EmployeesFacilities();
                        facility.FkEmployeesId = employees.EmployeesId;
                        facility.FkFacilitiesId = item;
                        facility.CreationDate = DateTime.Now;
                        facility.LastUpdateDate = DateTime.Now;
                        _context.EmployeesFacilities.Add(facility);
                        employees.EmployeesFacilities.Add(facility);
                    }
                }
                // Add roles to Employee EmployeeRoleNames
                if (employees.Roles != null)
                {
                    foreach (var item in employees.Roles)
                    {
                        var roleCheck = _context.RoleNames.FirstOrDefault(m => m.RoleNamesId == item);
                        EmployeesRoleNames roleName = new EmployeesRoleNames();

                        if (roleCheck != null)
                        {
                            switch (roleCheck.RoleName.ToLower())
                            {
                                default:
                                    var super = await _userManager.AddToRoleAsync(user, "USER");
                                    break;
                                case "supervisor":
                                    roleName.SupervisorNumber = employees.SupervisorNumber;
                                    roleName.SupervisionEndDate = employees.SupervisionEndDate.Value;
                                    roleName.SupervisorNumber = employees.SupervisorNumber;
                                    roleName.SupervisorRate = employees.EmployeeRate.Value;
                                    employees.IsSupervisor = true;
                                    super = await _userManager.AddToRoleAsync(user, "SUPERVISOR");
                                    break;
                                case "admin":
                                    super = await _userManager.AddToRoleAsync(user, "ADMIN");
                                    break;
                            }
                        }
                        roleName.FkRoleNamesId = item;
                        roleName.FkEmployeesId = employees.EmployeesId;
                        roleName.CreationDate = DateTime.Now;
                        roleName.LastUpdateDate = DateTime.Now;
                        _context.EmployeesRoleNames.Add(roleName);
                        await _context.SaveChangesAsync();
                        employees.EmployeesRoleNames.Add(roleName);
                        var roledocs = _context.RoleDocsCatalog.Where(m => m.Roles.Contains(roleCheck.RoleName)).ToList();
                        foreach (var roledoc in roledocs)
                        {
                            DocumentationProcess process = new DocumentationProcess();
                            process.FkEmployeesId = employees.EmployeesId;
                            process.FkRoleDocsCatalogId = roledoc.RoleDocsCatalogId;
                            process.CreationDate = DateTime.Now;
                            process.LastUpdateDate = DateTime.Now;
                            _context.DocumentationProcess.Add(process);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                // Add roles to Op EmployeeOperatingCounties
                if (employees.OpCounties != null)
                {
                    foreach (var item in employees.OpCounties)
                    {
                        EmployeesOperatingCounties opcounty = new EmployeesOperatingCounties();
                        opcounty.FkEmployeesId = employees.EmployeesId;
                        opcounty.FkOperatingCountiesId = item;
                        opcounty.CreationDate = DateTime.Now;
                        opcounty.LastUpdateDate = DateTime.Now;
                        _context.EmployeesOperatingCounties.Add(opcounty);
                        await _context.SaveChangesAsync();
                        employees.EmployeesOperatingCounties.Add(opcounty);
                    }
                }

                // Add Update to DB
                _context.Employees.Update(employees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["Ethnicities"] = new SelectList(externalLists.Ethnicities);
            ViewData["SchoolLevel"] = new SelectList(externalLists.SchoolLevel);
            ViewData["Religions"] = new SelectList(externalLists.Religions);
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities);
            ViewData["Facilities"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName");
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr");
            ViewData["RoleNames"] = new SelectList(_context.Set<RoleNames>(), "RoleNamesId", "RoleName");
            ViewData["Status"] = new SelectList(externalLists.EmployeeStatuses, employees.EmployeeStatus);
            var langs = externalLists.Languages.ConvertAll(a => { return new SelectListItem() { Text = a.EnglishName.ToString(), Value = a.Name.ToString(), Selected = false }; });
            foreach (var item in langs)
            {
                if (employees.Languages != null && employees.Languages.Contains(item.Value) && item.Value != string.Empty)
                {
                    item.Selected = true;
                }
            }
            ViewData["Languages"] = langs;

            return View(employees);
        }

        [Authorize(Policy = "EmployeeViews")]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .Include(m => m.Locations)
                .Include(m => m.EmployeesRoleNames)
                .Include(m => m.EmployeesOperatingCounties)
                .ThenInclude(m => m.FkOperatingCounties)
                .Include(m => m.EmployeesFacilities)
                .Where(m => m.EmployeesId == id)
                .FirstOrDefaultAsync();
            if (employees == null)
            {
                return NotFound();
            }
            var key = "WP46C8DF276ND5931069BDE2E695D45E";
            var decrypt = employees.Ssn;
            employees.Ssn = DataEncryption.DecryptString(decrypt, key);

            ViewData["Ethnicities"] = new SelectList(externalLists.Ethnicities);
            ViewData["SchoolLevel"] = new SelectList(externalLists.SchoolLevel);
            ViewData["Religions"] = new SelectList(externalLists.Religions);
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities);
            ViewData["Facilities"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName");
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr");
            ViewData["RoleNames"] = new SelectList(_context.Set<RoleNames>(), "RoleNamesId", "RoleName");
            ViewData["Status"] = new SelectList(externalLists.EmployeeStatuses);
            var langs = externalLists.Languages.ConvertAll(a => { return new SelectListItem() { Text = a.EnglishName.ToString(), Value = a.Name.ToString(), Selected = false }; });
            foreach (var item in langs)
            {
                if (employees.Languages != null && employees.Languages.Contains(item.Value) && item.Value != string.Empty)
                {
                    item.Selected = true;
                }
            }
            ViewData["Languages"] = langs;

            employees.Roles = employees.EmployeesRoleNames.Select(m => m.FkRoleNamesId).ToList();
            employees.OpCounties = employees.EmployeesOperatingCounties.Select(m => m.FkOperatingCountiesId).ToList();
            employees.Facilities = employees.EmployeesFacilities.Select(m => m.FkFacilitiesId).ToList();
            if (employees.EmployeesRoleNames.Count > 0)
            {
                foreach (var role in employees.EmployeesRoleNames)
                {
                    var roleCheck = _context.RoleNames.FirstOrDefault(m => m.RoleNamesId == role.FkRoleNamesId);

                    if (roleCheck != null && roleCheck.RoleName.ToLower() == "supervisor")
                    {
                        var supvrole = _context.EmployeesRoleNames.FirstOrDefault(m => m.EmployeesRoleNamesId == role.EmployeesRoleNamesId);
                        employees.SupervisorNumber = supvrole.SupervisorNumber;
                        employees.SupervisionEndDate = supvrole.SupervisionEndDate;
                        employees.SupervisionStartDate = supvrole.SupervisionStartDate;
                    }
                }

            }
            if (employees.Locations == null)
            {
                Locations locations = new Locations();
                employees.Locations = locations;
            }
            return View(employees);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "EmployeeViews")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] Employees employees)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (employees.CustomFile != null)
                    {
                        UploadProfileImageHelper uploadFile = new UploadProfileImageHelper(_hostEnv);
                        await uploadFile.UploadFileAsync(employees.CustomFile, true, employees.EmployeesId);
                        employees.AvatarUrl = "/profileImgs/" + employees.EmployeesId + employees.CustomFile.FileName.Substring(employees.CustomFile.FileName.LastIndexOf("."));
                    }
                    var user = _context.Users.Where(m => m.FkEmployeesId == employees.EmployeesId).AsNoTracking().FirstOrDefault();

                    user.Avatar = employees.AvatarUrl ?? "../../img/profile.svg";
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();

                    employees.Languages = String.Join(",", employees.ListLanguages.ToArray());

                    // Set creation and lastupdate dates to now
                    employees.LastUpdateDate = DateTime.Now;
                    employees.Locations.LastUpdateDate = DateTime.Now;

                    var employeeCheck = _context.Employees.AsNoTracking().Where(m => m.EmployeesId == employees.EmployeesId)
                        .Include(m => m.EmployeesFacilities).Include(m => m.EmployeesRoleNames)
                        .ThenInclude(m => m.FkRoleNames).Include(m => m.EmployeesOperatingCounties).AsNoTracking().FirstOrDefault();

                    var facilicityCheck = employeeCheck.EmployeesFacilities;
                    var roleCheck = employeeCheck.EmployeesRoleNames;
                    var OpCheck = employeeCheck.EmployeesOperatingCounties;
                    _context.EmployeesFacilities.RemoveRange(facilicityCheck);
                    _context.EmployeesRoleNames.RemoveRange(roleCheck);
                    _context.EmployeesOperatingCounties.RemoveRange(OpCheck);
                    _context.Entry(employeeCheck).State = EntityState.Detached;
                    // Add facilities to Employees EmployeesFacilities
                    if (employees.Facilities != null)
                    {
                        foreach (var item in employees.Facilities)
                        {
                            EmployeesFacilities facility = new EmployeesFacilities();
                            facility.FkEmployeesId = employees.EmployeesId;
                            facility.FkFacilitiesId = item;
                            facility.CreationDate = DateTime.Now;
                            facility.LastUpdateDate = DateTime.Now;
                            _context.EmployeesFacilities.Add(facility);
                            employees.EmployeesFacilities.Add(facility);
                        }
                    }
                    if (employees.Roles != null)
                    {
                        foreach (var item in employees.Roles)
                        {
                            EmployeesRoleNames roleName = new EmployeesRoleNames();
                            var sup = roleCheck.FirstOrDefault(m => m.FkRoleNamesId == item);
                            if (sup != null && sup.FkRoleNames.RoleName.ToLower() == "supervisor")
                            {
                                roleName.SupervisorNumber = employees.SupervisorNumber;
                                roleName.SupervisionEndDate = employees.SupervisionEndDate.Value;
                                roleName.SupervisionStartDate = employees.SupervisionStartDate.Value;
                                roleName.SupervisorRate = employees.EmployeeRate.Value;
                                employees.IsSupervisor = true;
                            }
                            roleName.FkRoleNamesId = item;
                            roleName.FkEmployeesId = employees.EmployeesId;
                            roleName.CreationDate = DateTime.Now;
                            roleName.LastUpdateDate = DateTime.Now;
                            _context.EmployeesRoleNames.Add(roleName);
                            await _context.SaveChangesAsync();
                            employees.EmployeesRoleNames.Add(roleName);
                            await _context.SaveChangesAsync();
                            var roledocs = _context.RoleDocsCatalog.Where(m => m.Roles.Contains(sup.FkRoleNames.RoleName)).ToList();
                            var docprocess = _context.DocumentationProcess.Where(m => m.FkEmployeesId == employees.EmployeesId).AsNoTracking().ToList();
                            if (docprocess.Count != 0)
                            {
                                foreach (var roledoc in docprocess)
                                {
                                    if (!roledocs.Any(m => m.Roles.Contains(sup.FkRoleNames.RoleName)))
                                    {
                                        DocumentationProcess process = new DocumentationProcess();
                                        process.FkEmployeesId = employees.EmployeesId;
                                        process.FkRoleDocsCatalogId = roledoc.FkRoleDocsCatalogId;
                                        process.CreationDate = DateTime.Now;
                                        process.LastUpdateDate = DateTime.Now;
                                        process.Role = sup.FkRoleNames.RoleName;
                                        _context.DocumentationProcess.Add(process);
                                        await _context.SaveChangesAsync();
                                        employees.DocumentationProcess.Add(process);
                                    }
                                }
                            }
                        }
                    }
                    // Add roles to Op EmployeeOperatingCounties
                    if (employees.OpCounties != null)
                    {
                        foreach (var item in employees.OpCounties)
                        {
                            EmployeesOperatingCounties opcounty = new EmployeesOperatingCounties();
                            opcounty.FkEmployeesId = employees.EmployeesId;
                            opcounty.FkOperatingCountiesId = item;
                            opcounty.CreationDate = DateTime.Now;
                            opcounty.LastUpdateDate = DateTime.Now;
                            _context.EmployeesOperatingCounties.Add(opcounty);
                            await _context.SaveChangesAsync();
                            employees.EmployeesOperatingCounties.Add(opcounty);
                        }
                    }
                    employees.Locations.LastUpdateDate = DateTime.Now;
                    var key = "WP46C8DF276ND5931069BDE2E695D45E";
                    var encrypt = employees.Ssn;
                    employees.Ssn = DataEncryption.EncryptString(encrypt, key);
                    // Add Update to DB
                    _context.Employees.Update(employees);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["Ethnicities"] = new SelectList(externalLists.Ethnicities, employees.Ethnicity);
            ViewData["SchoolLevel"] = new SelectList(externalLists.SchoolLevel, employees.HighestEducation);
            ViewData["Religions"] = new SelectList(externalLists.Religions, employees.Religion);
            ViewData["States"] = new SelectList(externalLists.States, employees.Locations.State);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities, employees.Locations.City);
            ViewData["Facilities"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", employees.EmployeesFacilities.FirstOrDefault().FkFacilitiesId);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", employees.EmployeesOperatingCounties.Select(m => m.FkOperatingCountiesId));
            ViewData["RoleNames"] = new SelectList(_context.Set<RoleNames>(), "RoleNamesId", "RoleName", employees.EmployeesRoleNames.Select(m => m.FkRoleNamesId));
            ViewData["Status"] = new SelectList(externalLists.EmployeeStatuses, employees.EmployeeStatus);
            var langs = externalLists.Languages.ConvertAll(a => { return new SelectListItem() { Text = a.EnglishName.ToString(), Value = a.Name.ToString(), Selected = false }; });
            foreach (var item in langs)
            {
                if (employees.Languages != null && employees.Languages.Contains(item.Value) && item.Value != string.Empty)
                {
                    item.Selected = true;
                }
            }
            ViewData["Languages"] = langs;
            return View(employees);
        }

        [Authorize(Policy = "EmployeeViews")]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeesId == id);
            if (employees == null)
            {
                return NotFound();
            }

            return PartialView(employees);
        }

        [Authorize(Policy = "EmployeeViews")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var employees = await _context.Employees
                .Include(m => m.Locations)
                .Include(m => m.TasksFkAssignedTo)
                .Include(m => m.TasksFkAssignedBy)
                .Include(m => m.EmployeesRoleNames)
                .Include(m => m.EmployeesOperatingCounties)
                .Include(m => m.EmployeesFacilities)
                .Where(m => m.EmployeesId == id).FirstOrDefaultAsync();

            _context.Employees.Remove(employees);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool EmployeesExists(string id)
        {
            return _context.Employees.Any(e => e.EmployeesId == id);
        }
        [HttpPost]
        [Authorize(Policy = "EmployeeViews")]
        public async Task<IActionResult> ChangeEmploymentStatus(string EmployeesId, string EmployeeStatus, bool IsHrReady)
        {
            if (EmployeesId == null)
            {
                return NotFound();
            }
            var employees = await _context.Employees.FindAsync(EmployeesId);

            if (ModelState.IsValid)
            {
                try
                {
                    // Set creation and lastupdate dates to now
                    employees.LastUpdateDate = DateTime.Now;
                    employees.IsHrReady = IsHrReady;
                    employees.EmployeeStatus = EmployeeStatus;
                    if (IsHrReady)
                    {
                        employees.HrReadySince = DateTime.Now;
                    }
                    // Add Update to DB
                    _context.Employees.Update(employees);
                    await _context.SaveChangesAsync();
                    return Redirect(Request.Headers["Referer"].ToString());

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.EmployeesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["Status"] = new SelectList(externalLists.EmployeeStatuses, employees.EmployeeStatus);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "ChangeEmploymentStatus", employees) });
        }

    }
}
