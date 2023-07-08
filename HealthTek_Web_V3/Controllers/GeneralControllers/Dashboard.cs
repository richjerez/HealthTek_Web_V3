using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.GeneralControllers
{
    [Authorize]
    public class Dashboard : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;

        public Dashboard(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            // get current user
            var user = await _userManager.GetUserAsync(User);

            var dashboards = _context.Dashboards
                .Where(u => u.FkUserId == user.Id && u.MainView == true)
                .Include(m => m.FkDashboardWidgets
                .OrderBy(c => c.HierarchySlot))
                .ThenInclude(m => m.FkWidget)
                .FirstOrDefault();
            if (dashboards == null)
            {
                dashboards = new Dashboards();
            }
            TempData["Toast"] = "Welcome back this is your dashboard. You can modify these widgets in the Dashboard Settings in the Mega Menu above!";
            return View(dashboards);
        }

        //Returns Active Clients
        public JsonResult GetActiveClients()
        {
            var clients = _context.Clients.Where(m => m.ClientStatus == "Active").Count();
            var maximum = _context.Clients.Count();
            return Json(new { body = clients, max = maximum });
        }

        // Returns number of clients with expiring referrals
        public JsonResult ExpiringReferrals()
        {
            var referral = _context.Clients.Where(e => e.ReferralDate >= DateTime.Now.AddDays(320)).Count();

            return Json(new { body = referral });
        }

        // Returns number of clients with expired referrals
        public JsonResult ExpiredReferrals()
        {
            var referral = _context.Clients.Where(e => e.ReferralDate >= DateTime.Now).Count();

            return Json(new { body = referral });
        }

        //Returns Active Employees
        public JsonResult GetActiveEmployees()
        {
            var emp = _context.Employees.Where(m => m.EmployeeStatus == "Active").Count();
            var maximum = _context.Employees.Count();
            return Json(new { body = emp, max = maximum });
        }

        //Returns Expired Authorizations
        public JsonResult ExpiredAuthorizations()
        {
            var auths = _context.Authorizations;
            var expired = auths.Where(m => m.ExpirationDate < DateTime.Now).Count();
            var max = auths.Count();
            return Json(new { body = expired, max = max });
        }

        // Returns number of appointments sitting at QA
        public JsonResult NeedsQa()
        {
            var qa = _context.Appointments.Where(e => e.QaStatus == "Received").Count();

            return Json(new { body = qa });
        }

        // Returns number of policies which need to change
        public JsonResult COINs()
        {
            var pols = _context.ClientInsurances.Where(e => e.PolicyStatus == "COIN").Count();

            return Json(new { body = pols });
        }

        // Returns number of users with expired HR docs------------------------
        public JsonResult ExpiringHRDocs()
        {
            var date_exp = DateTime.Now.AddDays(45).Date;
            var docs = _context.Documents.Where(e => e.DocumentExpirationDate <= date_exp).Count();

            return Json(new { body = docs });
        }

        // Returns number of auths pending
        public JsonResult PendingAuths()
        {
            var auths = _context.Authorizations.Where(e => e.AuthorizationStatus == "Pending").Count();

            return Json(new { body = auths });
        }

        // Returns number of users with expired HR docs
        public JsonResult ExpiringAuths()
        {
            var date_exp = DateTime.Now.AddDays(45).Date;
            var auths = _context.Authorizations.Where(e => e.ExpirationDate <= date_exp).Count();

            return Json(new { body = auths });
        }

        // Returns number of users with expired tasks------------------------
        public JsonResult ExpiredTasks()
        {
            var tasks = _context.Tasks.Where(e => e.DueDate <= DateTime.Now).Count();

            return Json(new { body = tasks });
        }

        // Returns number of users currently logged in
        public JsonResult ActiveLogins()
        {
            var users = _userManager.Users.ToList();
            int body = 0;

            foreach (var item in users)
            {
                var currentLoginId = item.FkLoginId;
                var login = _context.Logins.Find(currentLoginId);
                if (login != null && login.LogoutDate != null)
                {
                    body++;
                }
            }

            return Json(new { body = body, max = users.Count });
        }

        // Returns number of assignments listed as needing attention
        public JsonResult FlaggedAssignments()
        {
            var flags = _context.Assignments.Where(a => a.NeedsAttention == true).Include(n => n.FkClients).ToList();
            var names = "";
            foreach (var item in flags)
            {
                names += "<a href='/Clients/Profile/" + item.FkClientsId + "'>" + item.FkClients.FullName + ", </a>";
            }
            return Json(new { body = names, route = true });
        }

        //Returns list of Roles by Role Names
        public async Task<JsonResult> GetEmployeesRoles()
        {
            var roles = await _context.RoleNames.ToListAsync();
            List<string> returnList = new List<string>();
            var max = 0;
            foreach (var role in roles)
            {
                var emproles = _context.EmployeesRoleNames.Where(m => m.FkRoleNames.RoleName == role.RoleName).Select(e => e.FkEmployees).Count();
                returnList.Add(emproles.ToString());
                max += emproles;
            }
            return Json(new { body = returnList, max = max, roles = roles.Select(m => m.RoleName).ToList() });
        }

        public JsonResult GetAllClientsLastSixMonths()
        {
            var nextMonth = DateTime.Now.AddMonths(1).Month;
            var sixmonths = DateTime.Now.AddMonths(1).AddMonths(-6);
            var clients = _context.Clients.Where(m => m.CreationDate.Month < nextMonth && m.CreationDate > sixmonths).ToList();
            var max = clients.Count();
            List<string> returnList = new List<string>();
            for (int i = 5; i > -1; i--)
            {
                var num = clients.Where(m => m.CreationDate.Month == DateTime.Now.AddMonths(-i).Month).Count();
                returnList.Add(num.ToString());
            }
            return Json(new { body = returnList, max = max });
        }
        public JsonResult MonthlyTotalsByService(bool lastmonth)
        {
            var Month = DateTime.Now.Month;
            if (lastmonth)
            {
                Month = DateTime.Now.AddMonths(-1).Month;
            }
            var appointments = _context.Appointments.Where(m => m.AppointmentType.Contains("Services") && m.StartTime.Month == Month)
                .Include(m => m.FkServiceCodes).AsNoTracking().ToList();
            List<BarGraph> returnList = new List<BarGraph>();
            decimal max = 0;
            foreach (var item in appointments)
            {
                var name = item.FkServiceCodes.CodeTitle;
                var tr = returnList.Where(n => n.Name.Contains(name)).Count();
                if (tr <= 0)
                {
                    var graph = new BarGraph
                    {
                        Name = item.FkServiceCodes.CodeTitle,
                        Total = (decimal)item.FkServiceCodes.CodeRate.Value
                    };
                    max += graph.Total;
                    returnList.Add(graph);
                }
                else
                {
                    var graph = returnList.Where(n => n.Name.Contains(name)).FirstOrDefault();
                    graph.Total += (decimal)item.FkServiceCodes.CodeRate.Value;
                    max += (decimal)item.FkServiceCodes.CodeRate.Value;
                }
            }
            return Json(new { body = returnList.Select(m => m.Total).ToList(), max = max, labels = returnList.Select(m => m.Name).ToList() });
        }

        public class BarGraph
        {
            public string Name { get; set; }
            public decimal Total { get; set; }
        }
    }
}
