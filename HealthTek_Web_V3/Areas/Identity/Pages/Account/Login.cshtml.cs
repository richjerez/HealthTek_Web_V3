using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ReCaptcha _captcha;
        private readonly IdentityContext _context;
        private readonly EmailSender _emailSender;

        public LoginModel(SignInManager<AppUser> signInManager,
            ILogger<LoginModel> logger, ReCaptcha captcha, EmailSender emailSender, UserManager<AppUser> userManager, IdentityContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _captcha = captcha;
            _userManager = userManager;
            _context = context;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content(@"~/Dashboard");

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
                        var user = await _userManager.FindByNameAsync(Input.UserName);
                        var check = await _userManager.CheckPasswordAsync(user, Input.Password);
                        if (check == true)
                        {
                            //get the http context to process user information
                            UserInformation userInformation = new UserInformation(this.Request);

                            // create new user info
                            UserInfo userInfo = new UserInfo();
                            userInfo.Agent = userInformation.getAgent();
                            userInfo.Holder = user.Id;
                            userInfo.Browser = userInformation.getBrowser();
                            var localaddress = userInformation.GetIpAddress();
                            userInfo.LocalAddress = localaddress;
                            userInfo.DateCreated = System.DateTime.Now;

                            // Encrypt Local Address
                            var key = "WP46C8DF276ND5931069BDE2E695D45E";
                            var encrypt = userInfo.LocalAddress;
                            userInfo.LocalAddress = DataEncryption.EncryptString(encrypt, key);

                            //get list of existing info
                            var useInfo = _context.UserInfo.Where(m => m.Holder == user.Id).AsNoTracking().ToList();

                            // Global List Variable for user information
                            List<UserInfo> info = new List<UserInfo>();

                            ///<summary>                            
                            ///If user has no devices create the first device
                            ///else check whether the device exists or not
                            ///if device doesnt exhist - create device login - lockuser - and warn the user of a new login
                            ///Send Email and redirect with message:: We noticed a new device login! Please check your email to proceed!
                            ///if device exists check wheter is blocked or allowed
                            ///if blocked redirect to login: 
                            ///if allowed allow signin
                            ///</ summary>
                            if (useInfo.Count == 0)
                            {
                                userInfo.Status = true;
                                info.Add(userInfo);
                                _context.UserInfo.Add(userInfo);
                                await _context.SaveChangesAsync();
                                // Create the new Login
                                Logins logins = new Logins();
                                logins.FkDeviceId = userInfo.Id;
                                logins.FkUserId = user.Id;
                                logins.LoginDate = DateTime.Now;
                                // Save the new Login
                                _context.Logins.Add(logins);
                                await _context.SaveChangesAsync();
                                // Update the user with the latest login
                                user.FkLoginId = logins.LoginId;
                                _context.Users.Update(user);
                                await _context.SaveChangesAsync();
                                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                                if (result.Succeeded)
                                {
                                    _logger.LogInformation("User logged in.");
                                    return LocalRedirect(returnUrl);
                                }
                            }
                            else
                            {
                                var decryption = "";
                                var item = new UserInfo();
                                foreach (var addressinfo in useInfo)
                                {
                                    //If the device exists
                                    var temp = DataEncryption.DecryptString(addressinfo.LocalAddress, key);
                                    if (temp.Contains(localaddress))
                                    {
                                        decryption = temp;
                                        item = addressinfo;
                                    }
                                }
                                if (decryption != "")
                                {
                                    //If device is allowed login
                                    if (item.Status)
                                    {
                                        try
                                        {
                                            var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                                            if (result.Succeeded)
                                            {
                                                // Create the new Login
                                                Logins logins = new Logins();
                                                logins.FkDeviceId = item.Id;
                                                logins.FkUserId = user.Id;
                                                logins.LoginDate = DateTime.Now;
                                                // Save the new Login
                                                _context.Logins.Add(logins);
                                                await _context.SaveChangesAsync();
                                                // Update the user with the latest login
                                                user.FkLoginId = logins.LoginId;
                                                _context.Users.Update(user);
                                                await _context.SaveChangesAsync();
                                                _logger.LogInformation("User logged in.");
                                                return LocalRedirect(returnUrl);
                                            }
                                            if (result.RequiresTwoFactor)
                                            {
                                                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                                            }
                                            if (result.IsLockedOut)
                                                ModelState.AddModelError(string.Empty, "Your account is locked out. Kindly wait for 5 minutes and try again");
                                            else
                                            {
                                                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                                                return Page();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            _logger.LogInformation(ex.Message.ToString());
                                        }
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("Error:", "Invalid login attempt.");
                                        return Page();
                                    }
                                }
                                else
                                {
                                    //Add blocked device
                                    userInfo.Status = false;
                                    info.Add(userInfo);
                                    _context.UserInfo.Add(userInfo);
                                    await _context.SaveChangesAsync();

                                    //Send message to unlock user
                                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                                    var callbackUrl = Url.Page(
                                        "/Account/ConfirmEmail",
                                        pageHandler: null,
                                        values: new { userId = user.Id, code = code },
                                        protocol: Request.Scheme);
                                    Messages emailModel = new Messages();
                                    emailModel.ToEmail = user.Email;
                                    emailModel.Title = "Confirm your account";
                                    emailModel.Message = $"<img src='https://i.ibb.co/PWd0zVT/undraw-unexpected-friends-tg6k.png' alt='HealthTek - Confirm Email' style='width: 300px;margin: auto;display: block; '/> We noticed a new device login! Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                                    await _emailSender.SendMessage(emailModel);

                                    ModelState.AddModelError(string.Empty, "We noticed a new device login! Please check your email to proceed!");
                                    return Page();

                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid Login Wrong Password!");
                            return Page();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
