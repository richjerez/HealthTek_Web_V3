using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "ADMIN")]
    public class ClientEventsController : Controller
    {
        private readonly IdentityContext _context;

        public ClientEventsController(IdentityContext context)
        {
            _context = context;
        }

        // GET: ClientEvents
        public async Task<IActionResult> Index()
        {
            var identityContext = _context.ClientEvents.Include(m => m.FkMaladaptives);
            return View(await identityContext.ToListAsync());
        }

        // GET: ClientEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientEvents = await _context.ClientEvents
                .Include(m => m.FkMaladaptives)
                .FirstOrDefaultAsync(m => m.ClientEventsId == id);
            if (clientEvents == null)
            {
                return NotFound();
            }

            return View(clientEvents);
        }

        // GET: ClientEvents/Create
        public IActionResult Create()
        {
            ViewData["FkMaladaptivesId"] = new SelectList(_context.Set<Maladaptives>(), "MaladaptivesId", "MaladaptivesId");
            return View();
        }

        // POST: ClientEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ClientEvents clientEvents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientEvents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkMaladaptivesId"] = new SelectList(_context.Set<Maladaptives>(), "MaladaptivesId", "MaladaptivesId", clientEvents.FkMaladaptivesId);
            return View(clientEvents);
        }

        // GET: ClientEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientEvents = await _context.ClientEvents.FindAsync(id);
            if (clientEvents == null)
            {
                return NotFound();
            }
            ViewData["FkMaladaptivesId"] = new SelectList(_context.Set<Maladaptives>(), "MaladaptivesId", "MaladaptivesId", clientEvents.FkMaladaptivesId);
            return View(clientEvents);
        }

        // POST: ClientEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] ClientEvents clientEvents)
        {
            if (id != clientEvents.ClientEventsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientEvents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientEventsExists(clientEvents.ClientEventsId))
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
            ViewData["FkMaladaptivesId"] = new SelectList(_context.Set<Maladaptives>(), "MaladaptivesId", "MaladaptivesId", clientEvents.FkMaladaptivesId);
            return View(clientEvents);
        }

        // GET: ClientEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ClientEvents = await _context.ClientEvents
                .Include(m => m.FkMaladaptives)
                .FirstOrDefaultAsync(m => m.ClientEventsId == id);
            if (ClientEvents == null)
            {
                return NotFound();
            }

            return View(ClientEvents);
        }

        // POST: ClientEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ClientEvents = await _context.ClientEvents.FindAsync(id);
            _context.ClientEvents.Remove(ClientEvents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientEventsExists(int id)
        {
            return _context.ClientEvents.Any(e => e.ClientEventsId == id);
        }
    }
}
