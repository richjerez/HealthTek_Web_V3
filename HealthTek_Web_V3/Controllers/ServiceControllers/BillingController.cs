using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.ServiceControllers
{
    [Authorize(Roles = "SUPERUSER,ADMIN,BILLER,V-FINANCE")]
    public class BillingController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;
        public BillingController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var identityContext = await _context.Appointments
                .Where(m => m.BillingStatus != null)
                .Include(m => m.FkEmployees)
                .Include(m => m.FkBatches)
                .Include(m => m.FkServiceCodes)
                .Include(m => m.FkClients)
                .ThenInclude(m => m.ClientsFacilities)
                .AsSplitQuery()
                .Include(m => m.FkClients)
                .ThenInclude(m => m.Authorizations)
                .AsSplitQuery()
                .Include(m => m.FkClients)
                .ThenInclude(m => m.Diagnosis)
                .AsSplitQuery()
                .Include(m => m.FkStartLocation)
                .Include(m => m.FkEndLocation).ToListAsync();
            return View(identityContext);
        }

        public async Task<IActionResult> ExportBillingData()
        {
            var user = await _userManager.GetUserAsync(User);
            //code to get employee list
            var employeeData = user.FkEmployeesId;
            var fileDownloadName = "Billing.csv";
            return new CSVExporter(employeeData, fileDownloadName, _context, "Billing");
        }


    }
}
