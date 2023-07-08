using HealthTek_Shared_Libraries;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HealthTek_Web_V3.Controllers
{
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly UserManager<AppUser> _userManager;
        public ReportController(IReportService reportService, UserManager<AppUser> userManager)
        {
            _reportService = reportService;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Get(string ClassName, string? EmployeeId)
        {
            if (EmployeeId == "GETUSERFROMCONTROLLER")
            {
                EmployeeId = _userManager.GetUserAsync(User).Result.Id;
            }
            else
            {
                EmployeeId = _userManager.GetUserAsync(User).Result.FkEmployeesId;
            }
            var pdfFile = _reportService.GeneratePdfReport(ClassName, EmployeeId);
            return File(pdfFile,
            "application/octet-stream", ClassName + ".pdf");
        }
    }
}
