using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IdentityContext _identityContext;
        private readonly IWebHostEnvironment _hostEnv;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IdentityContext identityContext, IWebHostEnvironment hostEnv)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _identityContext = identityContext;
            _hostEnv = hostEnv;
        }
        [TempData]
        public string ReturnUrl { get; set; }

        public string Username { get; set; }

        public IFormFile FormFile { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public EmailModel EmailModel { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Username")]
            public string UserName { get; set; }
            public string Avatar { get; set; }
        }
        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);


            Input = new InputModel
            {
                UserName = userName,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            var employee = await _identityContext.Employees.FindAsync(user.FkEmployeesId);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                employee.PhoneNumber = phoneNumber;
                _identityContext.Employees.Update(employee);
                await _identityContext.SaveChangesAsync();

                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Error: Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }

            }
            await _signInManager.RefreshSignInAsync(user);

            var username = await _userManager.GetUserNameAsync(user);
            if (Input.UserName != username)
            {
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.UserName);
                if (!setUserNameResult.Succeeded)
                {
                    switch (setUserNameResult.Errors.Select(m => m.Code).FirstOrDefault())
                    {
                        case "DuplicateUserName":
                            StatusMessage = "Error: Username '" + Input.UserName + "' is already taken!";
                            return RedirectToPage();
                        case "InvalidUserName":
                            StatusMessage = "Error: Username '" + Input.UserName + "' is invalid. Usernames can only contain letters or digits!";
                            return RedirectToPage();

                    }

                }
            }
            await _signInManager.RefreshSignInAsync(user);

            if (FormFile != null)
            {
                if (FormFile.FileName != user.Avatar)
                {
                    UploadFileHelper uploadFile = new UploadFileHelper(_hostEnv);
                    await uploadFile.UploadFileAsync(FormFile, user.FkEmployeesId, true, "profile");
                    user.Avatar = "profile" + FormFile.FileName.Substring(FormFile.FileName.LastIndexOf("."));
                    var emp = await _identityContext.Employees.FindAsync(user.FkEmployeesId);
                    emp.AvatarUrl = "/files/" + emp.EmployeesId + "/" + user.Avatar;
                    _identityContext.Employees.Update(emp);
                    await _identityContext.SaveChangesAsync();
                    _identityContext.Users.Update(user);
                    await _identityContext.SaveChangesAsync();
                }

                await _signInManager.RefreshSignInAsync(user);

            }
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}
