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
    public class ClientContactsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly ExternalLists externalLists = new ExternalLists();

        public ClientContactsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: ClientContacts/Create
        public IActionResult Create(int id)
        {
            ViewData["FkClientsId"] = id;
            ViewData["States"] = new SelectList(externalLists.States);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr");
            return PartialView();
        }

        // POST: ClientContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ClientContacts clientContacts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientContacts);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkClientsId"] = clientContacts.FkClientsId;
            ViewData["States"] = new SelectList(externalLists.States, clientContacts.FkLocations.State);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities, clientContacts.FkLocations.City);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", clientContacts.FkLocations.County);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", clientContacts) });
        }

        // GET: ClientContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientContacts = _context.ClientContacts.Include(m => m.FkLocations).FirstOrDefault(m => m.FkLocationsId == id);
            if (clientContacts == null)
            {
                return NotFound();
            }
            ViewData["FkClientsId"] = clientContacts.FkClientsId;
            ViewData["States"] = new SelectList(externalLists.States, clientContacts.FkLocations.State);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities, clientContacts.FkLocations.City);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", clientContacts.FkLocations.County);
            return PartialView(clientContacts);
        }

        // POST: ClientContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ClientContacts clientContacts)
        {
            if (id != clientContacts.ClientContactsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientContacts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientContactsExists(clientContacts.ClientContactsId))
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
            ViewData["FkClientsId"] = clientContacts.FkClientsId;
            ViewData["States"] = new SelectList(externalLists.States, clientContacts.FkLocations.State);
            ViewData["Cities"] = new SelectList(externalLists.FloridaCities, clientContacts.FkLocations.City);
            ViewData["OperatingCounties"] = new SelectList(_context.Set<OperatingCounties>(), "OperatingCountiesId", "OPAbbr", clientContacts.FkLocations.County);
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", clientContacts) });
        }

        // GET: ClientContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientContacts = await _context.ClientContacts
                .Include(c => c.FkClients)
                .FirstOrDefaultAsync(m => m.ClientContactsId == id);
            if (clientContacts == null)
            {
                return NotFound();
            }

            return PartialView(clientContacts);
        }

        // POST: ClientContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientContacts = await _context.ClientContacts.FindAsync(id);
            _context.ClientContacts.Remove(clientContacts);
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers["Referer"].ToString());
        }

        private bool ClientContactsExists(int id)
        {
            return _context.ClientContacts.Any(e => e.ClientContactsId == id);
        }
    }
}
