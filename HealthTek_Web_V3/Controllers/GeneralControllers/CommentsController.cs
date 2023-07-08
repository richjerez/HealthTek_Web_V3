using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CommentsController(IdentityContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Comments/Create
        public IActionResult Create(int id)
        {
            ViewData["FkClientsId"] = id;
            return PartialView();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Comments comments)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                comments.FkUserId = user.FkEmployeesId;
                _context.Add(comments);
                await _context.SaveChangesAsync();
                return Redirect(Request.Headers["Referer"].ToString());
            }
            ViewData["FkClientsId"] = comments.FkClientsId;
            ViewData["FkUsersId"] = comments.FkUserId;
            ViewData["FkBatchesId"] = comments.FkBatchesId;
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", comments) });
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments.FindAsync(id);
            if (comments == null)
            {
                return NotFound();
            }
            ViewData["FkClientsId"] = comments.FkClientsId;
            ViewData["FkUsersId"] = comments.FkUserId;
            ViewData["FkBatchesId"] = comments.FkBatchesId;
            return PartialView(comments);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Comments comments)
        {
            if (id != comments.CommentsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    comments.FkUserId = user.FkEmployeesId;

                    _context.Update(comments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentsExists(comments.CommentsId))
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
            ViewData["FkClientsId"] = comments.FkClientsId;
            ViewData["FkUsersId"] = comments.FkUserId;
            ViewData["FkBatchesId"] = comments.FkBatchesId;
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Edit", comments) });
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comments = await _context.Comments
                .Include(c => c.FkClients)
                .FirstOrDefaultAsync(m => m.CommentsId == id);
            if (comments == null)
            {
                return NotFound();
            }

            return PartialView(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comments = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comments);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Clients", new { id = comments.FkClientsId });
        }

        private bool CommentsExists(int id)
        {
            return _context.Comments.Any(e => e.CommentsId == id);
        }
    }
}
