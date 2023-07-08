using HealthTek_Shared_Libraries;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailSender _emailSender;
        private readonly ReCaptcha _captcha;
        private readonly ILogger<ForgotPasswordModel> _logger;

        public ForgotPasswordModel(UserManager<AppUser> userManager, EmailSender emailSender, ILogger<ForgotPasswordModel> logger, ReCaptcha captcha)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _captcha = captcha;
            _logger = logger;
        }
        [TempData]
        public string StatusMessage { get; set; }
        [TempData]
        public string ReturnUrl { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                // For when email confrimation is required || !(await _userManager.IsEmailConfirmedAsync(user))
                if (user == null)
                {
                    StatusMessage = "Create a new account with us!";

                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./Register");
                }

                // ReCaptcha
                if (!Request.Form.ContainsKey("g-recaptcha-response")) return Page();
                var captcha = Request.Form["g-recaptcha-response"].ToString();
                var captured = !await _captcha.IsValid(captcha);
                if (captured)
                {
                    // For more information on how to enable account confirmation and password reset please 
                    // visit https://go.microsoft.com/fwlink/?LinkID=532713
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code },
                        protocol: Request.Scheme);

                    Messages emailModel = new Messages();
                    emailModel.ToEmail = Input.Email;
                    emailModel.Title = "Reset Password";
                    emailModel.Message = $"<img src='https://i.ibb.co/RStBSKm/undraw-Forgot-password-re-hxwm.png' alt='Neo-DevOps-App Forgot Password' style='width: 300px;margin: auto;display: block;'/>This things happen to everyone. You can reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                    _emailSender.SendMessage(emailModel);
                    StatusMessage = "Password reset codes sent!";
                    return RedirectToPage("./Login");

                }
                else
                {
                    _logger.LogInformation("Invalid password reset No captcha");
                    ModelState.AddModelError(string.Empty, "Invalid attempt.");
                    return Page();
                }


            }

            return Page();
        }
    }
}
