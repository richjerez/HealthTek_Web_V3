using HealthTek_Shared_Libraries;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailSender _emailSender;
        private readonly ReCaptcha _captcha;

        public ResendEmailConfirmationModel(UserManager<AppUser> userManager, EmailSender emailSender, ReCaptcha captcha)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _captcha = captcha;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // ReCaptcha
                if (!Request.Form.ContainsKey("g-recaptcha-response")) return Page();
                var captcha = Request.Form["g-recaptcha-response"].ToString();
                if (captcha != String.Empty)
                {
                    var captured = !await _captcha.IsValid(captcha);
                    if (captured)
                    {
                        var user = await _userManager.FindByEmailAsync(Input.Email);
                        if (user == null)
                        {
                            ModelState.AddModelError(string.Empty, "Incorrect Email Verify Entry!");
                            return Page();
                        }

                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { userId = userId, code = code },
                            protocol: Request.Scheme);
                        Messages emailModel = new Messages();
                        emailModel.ToEmail = Input.Email;
                        emailModel.Title = "Confirm your email";
                        emailModel.Message = $"<img src='https://i.ibb.co/PWd0zVT/undraw-unexpected-friends-tg6k.png' alt='Neo-DevOps-App Confirm Email' style='width: 300px;margin: auto;display: block; '/> Thank you for registering with us! Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                        await _emailSender.SendMessage(emailModel);

                        StatusMessage = "Check your email a confirmation email has been sent.";
                        return RedirectToPage("./Login");


                    }
                }
            }
            return Page();
        }
    }
}
