using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace HealthTek_Web_V3.Controllers
{
    public class MessagesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailSender _emailSender;
        public MessagesController(IdentityContext context, UserManager<AppUser> userManager, EmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: Messages/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return PartialView();
            }
            Messages emailModel = new Messages();
            emailModel.FromName = user.UserName;
            emailModel.FromEmail = user.Email;
            return PartialView(emailModel);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Messages messages, string returnUrl = null, string contact = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                if (contact != null)
                {
                    switch (contact)
                    {
                        case "Contact":
                            messages.Message = "User " + messages.FromName + " sent message with email " + messages.FromEmail + " Message: " + messages.Message + ".";
                            await _emailSender.SendMessage(messages);
                            break;
                    }
                }
                return Redirect(Request.Headers["Referer"].ToString());
            }
            return Json(new { isValid = false, html = ModalHelper.RenderRazorViewToString(this, "Create", messages) });
        }

    }
}
