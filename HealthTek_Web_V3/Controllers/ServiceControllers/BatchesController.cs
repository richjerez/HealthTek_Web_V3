using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthTek_Web_V3.Controllers.ServiceControllers
{
    [Authorize]
    public class BatchesController : Controller
    {
        public BatchesController()
        {
        }

        // GET: Batches
        public IActionResult Index() => PartialView();

    }
}
