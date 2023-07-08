using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers.ClientControllers
{
    [Authorize]
    public class ClientsFacilitiesController : Controller
    {
        private readonly IdentityContext _context;

        public ClientsFacilitiesController(IdentityContext context)
        {
            _context = context;
        }

        // GET: ClientsFacilities/Create
        public IActionResult Create(int id)
        {
            ClientsFacilities clientsFacilities = new ClientsFacilities();
            clientsFacilities.FkClientsId = id;
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilityName");
            return PartialView(clientsFacilities);
        }

        // POST: ClientsFacilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ClientsFacilities clientsFacilities)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientsFacilities);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilityName", clientsFacilities.FkFacilitiesId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", clientsFacilities) });
        }

        // GET: ClientsFacilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientsFacilities = await _context.ClientsFacilities.FindAsync(id);
            if (clientsFacilities == null)
            {
                return NotFound();
            }
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilityName", clientsFacilities.FkFacilitiesId);
            return PartialView(clientsFacilities);
        }

        // POST: ClientsFacilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ClientsFacilities clientsFacilities)
        {
            if (id != clientsFacilities.ClientsFacilitiesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientsFacilities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsFacilitiesExists(clientsFacilities.ClientsFacilitiesId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkFacilitiesId"] = new SelectList(_context.Facilities, "FacilitiesId", "FacilityName", clientsFacilities.FkFacilitiesId);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", clientsFacilities) });
        }

        private bool ClientsFacilitiesExists(int id)
        {
            return _context.ClientsFacilities.Any(e => e.ClientsFacilitiesId == id);
        }
    }
}
