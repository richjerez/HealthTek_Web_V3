using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{
    [Authorize(Policy = "ADMIN")]
    public class UsersController : Controller
    {
        #region Variables
        private readonly IdentityContext _context;
        private RoleManager<UserRoles> _roleManager;
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signinManager;
        private readonly EmailSender _emailSender;

        [TempData]
        public bool IsInRole { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        #endregion

        #region Constructor
        // Class Constructor
        public UsersController(IdentityContext context, RoleManager<UserRoles> roleManager, UserManager<AppUser> userManager, EmailSender emailSender, SignInManager<AppUser> signinManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _signinManager = signinManager;
        }
        #endregion

        #region CRUD
        // GET: Users
        public async Task<ActionResult> Index()
        {
            var user = await _userManager.Users.ToListAsync();
            return View(user);
        }

        // GET: UsersController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return PartialView(users);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return PartialView(users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [FromForm] AppUser user)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = await _userManager.FindByIdAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    var phoneNumber = await _userManager.GetPhoneNumberAsync(users);
                    var token = await _userManager.GenerateChangePhoneNumberTokenAsync(users, user.PhoneNumber);

                    if (user.PhoneNumber != phoneNumber)
                    {
                        var setPhoneResult = await _userManager.ChangePhoneNumberAsync(users, user.PhoneNumber, token);
                        if (!setPhoneResult.Succeeded)
                        {
                            StatusMessage = "Error: Unexpected error when trying to set phone number.";
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            StatusMessage = "User has been updated!";
                            return RedirectToAction(nameof(Index));
                        }

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUsersExists(id))
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
            return PartialView(user);
        }
        // GET: UsersController1/Delete/5
        public async Task<IActionResult> Delete(string id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.alert = "warning";
            }
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return PartialView(users);
        }

        // POST: UsersController1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var users = await _context.Users.FindAsync(id);
            _context.Remove(users);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private bool AppUsersExists(string id) => _context.Users.Any(e => e.Id == id);

        #endregion

        #region UsersRoleAction
        //[Authorize(Roles = "SUPERUSER")]
        public async Task<IActionResult> UserRoles(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            AppUser user = (AppUser)await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = _roleManager.Roles.Where(m => m.NormalizedName != "SUPERUSER").ToList();
            List<AppRolesModel> names = new List<AppRolesModel>();
            foreach (var role in roles)
            {
                var flag = await _userManager.IsInRoleAsync(user, role.Name);
                if (user != null && flag == true)
                {
                    names.Add(new AppRolesModel
                    {
                        Id = role.Id,
                        Role = role.Name,
                        Flag = true
                    });
                }
                else
                {
                    names.Add(new AppRolesModel
                    {
                        Id = role.Id,
                        Role = role.Name,
                        Flag = false
                    });
                }
            }
            ViewBag.UserId = id;
            return PartialView(names);
        }
        public async Task<IActionResult> ResetPassword(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            AppUser user = (AppUser)await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ResetPassword",
                pageHandler: null,
                values: new { area = "Identity", code },
                protocol: Request.Scheme);

            Messages emailModel = new Messages();
            emailModel.ToEmail = user.Email;
            emailModel.Title = "Reset Password";
            emailModel.Message = $"<img src='https://i.ibb.co/RStBSKm/undraw-Forgot-password-re-hxwm.png' alt='Neo-DevOps-App Forgot Password' style='width: 300px;margin: auto;display: block;'/>This things happen to everyone. You can reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
            await _emailSender.SendMessage(emailModel);
            StatusMessage = "Password reset codes sent!";

            return RedirectToAction(nameof(Index));
        }
        //[Authorize(Roles = "SUPERUSER")]
        public async Task<IActionResult> TogglePermission([FromBody] AppRolesModel userModel)
        {
            AppUser user = await _userManager.FindByIdAsync(userModel.Id);
            var wait = await _userManager.IsInRoleAsync(user, userModel.Role);
            if (wait == false)
            {
                await _userManager.AddToRoleAsync(user, userModel.Role);
                return Json(new { ok = true });
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, userModel.Role);
                return Json(new { ok = true });
            }
        }
        #endregion
        public async Task<JsonResult> ToggleDeviceAccess(int id)
        {
            var devices = await _context.UserInfo.FindAsync(id);
            var status = "";
            switch (devices.Status)
            {
                case true:
                    devices.Status = false;
                    status = "Blocked";
                    break;
                case false:
                    devices.Status = true;
                    status = "Allowed";
                    break;
            }
            _context.UserInfo.Update(devices);
            await _context.SaveChangesAsync();
            var key = "WP46C8DF276ND5931069BDE2E695D45E";
            var decrypt = devices.LocalAddress;
            devices.LocalAddress = DataEncryption.DecryptString(decrypt, key);

            var device = devices.Agent + " - " + devices.Browser + " [" + devices.LocalAddress + "] - " + status;
            return Json(new { ok = devices.Status, device = device });
        }

        public bool UnlockUser(string id)
        {
            var userTask = _userManager.FindByIdAsync(id);
            userTask.Wait();
            var user = userTask.Result;

            var lockDisabledTask = _userManager.SetLockoutEnabledAsync(user, false);
            lockDisabledTask.Wait();

            var setLockoutEndDateTask = _userManager.SetLockoutEndDateAsync(user, DateTime.Now - TimeSpan.FromMinutes(1));
            setLockoutEndDateTask.Wait();

            return setLockoutEndDateTask.Result.Succeeded && lockDisabledTask.Result.Succeeded;
        }
    }

}
