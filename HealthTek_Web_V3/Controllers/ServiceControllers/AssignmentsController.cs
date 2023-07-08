using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "AssingmentViews")]
    public class AssignmentsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        ExternalLists externalLists = new ExternalLists();

        public AssignmentsController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            var identityContext = await _context.Assignments
.Include(a => a.FkEmployees).Include(a => a.FkFacilities).Include(a => a.FkClients).ThenInclude(m => m.Locations)
.Include(a => a.FkClients).ThenInclude(m => m.ClientsFacilities).ToListAsync();

            return View(identityContext);
        }
        public async Task<string> GetAllAssignments(string? role = null)
        {
            var identityContext = await _context.Assignments.Where(m => m.AssignmentStatus != "Archived")
    .Include(a => a.FkEmployees).Include(a => a.FkFacilities).Include(a => a.FkClients).ThenInclude(m => m.Locations)
    .Include(a => a.FkClients).ThenInclude(m => m.ClientsFacilities).ToListAsync();
            var list = await _context.Employees.AsNoTracking().Include(m => m.Locations).AsNoTracking().Include(m => m.EmployeesRoleNames).ThenInclude(m => m.FkRoleNames).AsNoTracking().ToListAsync();
            if (role != null)
            {
                list = _context.Employees.Where(e => e.EmployeesRoleNames.Any(m => m.FkRoleNames.RoleName.ToLower() == role.ToLower())).Include(m => m.EmployeesRoleNames).ThenInclude(m => m.FkRoleNames).Include(m => m.Locations).ToList();
            }
            var clients = identityContext.Where(m => m.NeedsAttention == true).Select(m => m.FkClients).ToList();
            List<MapModel> markers = new List<MapModel>();
            foreach (var client in list)
            {
                var loc = client.Locations;

                if(loc != null)
                {
                    markers.Add(new MapModel
                    {
                        icon = 1,
                        lat = loc.GpsLatitude.ToString(),
                        lng = loc.GpsLongitude.ToString(),
                        html = "<button type=\"button\" onclick=\"AddEditElements(0 , 'Assignments',false,'" + client.EmployeesId + "',0)\" " +
                            "class=\"btn btn-success\" style=\"margin: auto;display: block;\">Assign</button><p class=\"text-secondary\"" +
                            " style=\"margin-top:10px\"><strong style=\"display:block\">"
                            + client.EmployeesRoleNames.FirstOrDefault().FkRoleNames.RoleName + " - " + client.FullName
                            + "</strong>" + loc.FullPrimaryAddress + "</p>"
                    });
                }
            }
            foreach (var client in clients)
            {
                var loc = client.Locations.FirstOrDefault();

                markers.Add(new MapModel
                {
                    icon = 0,
                    lat = loc.GpsLatitude.ToString(),
                    lng = loc.GpsLongitude.ToString(),
                    html = "<button type=\"button\" onclick=\"AddEditElements(0 , 'Assignments',false,0," + client.ClientsId + ")\" " +
                    "class=\"btn btn-success\" style=\"margin: auto;display: block;\">Assign</button><p class=\"text-secondary\"" +
                    " style=\"margin-top:10px\"><strong style=\"display:block\">"
                    + client.FullName + "</strong>" + loc.FullPrimaryAddress + "</p>"
,
                });
            }
            string json = JsonConvert.SerializeObject(markers, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return json;
        }
        public async Task<string> getEmployees(string role)
        {
            var list = await GetAllAssignments(role);
            return list;
        }
        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments
                .Include(a => a.FkClients)
                .Include(a => a.FkEmployees)
                .Include(a => a.FkFacilities)
                .FirstOrDefaultAsync(m => m.AssignmentsId == id);
            if (assignments == null)
            {
                return NotFound();
            }

            return View(assignments);
        }

        // GET: Assignments/Create
        public IActionResult Create(string? id, int? client)
        {
            if (id != null && id != "undefined")
            {
                ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "EmployeeLabel", id);
                ViewData["AssignmentPosition"] = new SelectList(_context.Set<RoleNames>().Where(m => m.EmployeesRoleNames.Any(m => m.FkEmployeesId == id)), "RoleName", "RoleName");
            }
            else
            {
                ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "EmployeeLabel");
                ViewData["AssignmentPosition"] = new SelectList(_context.Set<RoleNames>(), "RoleName", "RoleName");
            }
            if (client != null)
            {
                ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", client);
            }
            else
            {
                ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName");
            }
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName");
            ViewData["Status"] = new SelectList(externalLists.AssignmentStatuses);
            return PartialView();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Assignments assignments, DateTime DueDate)
        {
            if (ModelState.IsValid)
            {
                var userid = await _userManager.GetUserAsync(User);
                var assignmentType = "Assignment";
                var awaitingAssignment = "Awaiting";
                //var openAssignment = "Open";

                assignments.CreationDate = DateTime.Now;
                assignments.LastUpdateDate = DateTime.Now;
                assignments.AssignmentStatus = awaitingAssignment;
                _context.Add(assignments);
                await _context.SaveChangesAsync();

                var client = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(m => m.ClientsId == assignments.FkClientsId);

                //Create Task 
                Tasks tasks = new Tasks();
                tasks.CreationDate = DateTime.Now;
                tasks.LastUpdateDate = DateTime.Now;
                tasks.DueDate = DueDate;
                tasks.FkAssignedToId = assignments.FkEmployeesId;
                tasks.TaskDescription = "<a href='/Clients/Profile/" + assignments.FkClientsId + "'>" + client.FullName + "</a>";
                tasks.TaskType = assignmentType;
                tasks.TaskStatus = awaitingAssignment;
                tasks.TaskSubject = "Client Assignment";
                tasks.TaskNote = assignments.AssignmentNote;
                tasks.FkAssignedById = userid.FkEmployeesId;
                tasks.TaskIdentifier = assignments.AssignmentsId.ToString();
                _context.Tasks.Add(tasks);
                await _context.SaveChangesAsync();
                assignments.AssignmentStatus = awaitingAssignment;
                _context.Update(assignments);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", assignments.FkClientsId);
            ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "EmployeeLabel", assignments.FkEmployeesId);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", assignments.FkFacilitiesId);
            ViewData["Status"] = new SelectList(externalLists.AssignmentStatuses, assignments.AssignmentStatus);
            ViewData["FkRoleNamesId"] = new SelectList(_context.Set<RoleNames>(), "RoleNamesId", "RoleName", assignments.AssignmentPosition);
            return View(assignments);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments.FindAsync(id);
            if (assignments == null)
            {
                return NotFound();
            }
            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", assignments.FkClientsId);
            if (assignments.FkEmployeesId != null)
            {
                ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "EmployeeLabel", assignments.FkEmployeesId);
            }
            else
            {
                ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "EmployeeLabel");
            }
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilityName", assignments.FkFacilitiesId);
            ViewData["Status"] = new SelectList(externalLists.AssignmentStatuses, assignments.AssignmentStatus);
            return PartialView(assignments);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Assignments assignments, DateTime DueDate)
        {
            if (id != assignments.AssignmentsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldAssignmentPos = _context.Assignments.Where(m => m.AssignmentsId == id).Select(m => m.AssignmentPosition).AsNoTracking().FirstOrDefault();
                    var client = await _context.Clients.AsNoTracking().FirstOrDefaultAsync(m => m.ClientsId == assignments.FkClientsId);

                    assignments.LastUpdateDate = DateTime.Now;
                    assignments.AssignmentPosition = oldAssignmentPos;
                    var userid = await _userManager.GetUserAsync(User);
                    var assignmentType = "Assignment";
                    var awaiting = "Awaiting";
                    //Create Task 
                    Tasks tasks = new Tasks();
                    tasks.CreationDate = DateTime.Now;
                    tasks.LastUpdateDate = DateTime.Now;
                    tasks.DueDate = DueDate;
                    tasks.FkAssignedToId = assignments.FkEmployeesId;
                    tasks.TaskDescription = "<a href='/Clients/Profile/" + assignments.FkClientsId + "'>" + client.FullName + "</a>";
                    tasks.TaskType = assignmentType;
                    tasks.TaskStatus = awaiting;
                    tasks.TaskSubject = "Client Assignment";
                    tasks.TaskNote = assignments.AssignmentNote;
                    tasks.FkAssignedById = userid.FkEmployeesId;
                    tasks.TaskIdentifier = assignments.AssignmentsId.ToString();
                    _context.Tasks.Add(tasks);
                    await _context.SaveChangesAsync();
                    assignments.AssignmentStatus = awaiting;
                    _context.Update(assignments);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentsExists(assignments.AssignmentsId))
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
            ViewData["FkClientsId"] = new SelectList(_context.Set<Clients>(), "ClientsId", "FullName", assignments.FkClientsId);
            ViewData["FkEmployeesId"] = new SelectList(_context.Set<Employees>(), "EmployeesId", "EmployeeLabel", assignments.FkEmployeesId);
            ViewData["FkFacilitiesId"] = new SelectList(_context.Set<Facilities>(), "FacilitiesId", "FacilicityName", assignments.FkFacilitiesId);
            ViewData["Status"] = new SelectList(externalLists.AssignmentStatuses, assignments.AssignmentStatus);
            return PartialView(assignments);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignments = await _context.Assignments
                .Include(a => a.FkClients)
                .Include(a => a.FkEmployees)
                .Include(a => a.FkFacilities)
                .FirstOrDefaultAsync(m => m.AssignmentsId == id);
            if (assignments == null)
            {
                return NotFound();
            }

            return View(assignments);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignments = await _context.Assignments.FindAsync(id);
            _context.Assignments.Remove(assignments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentsExists(int id)
        {
            return _context.Assignments.Any(e => e.AssignmentsId == id);
        }
        public class MapModel
        {
            public string lat { get; set; }
            public string lng { get; set; }
            public int icon { get; set; }
            public string html { get; set; }
        }
    }
}
