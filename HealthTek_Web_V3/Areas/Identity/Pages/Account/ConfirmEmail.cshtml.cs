using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IdentityContext _context;

        public ConfirmEmailModel(UserManager<AppUser> userManager, SignInManager<AppUser> signinManager, IdentityContext context)
        {
            _userManager = userManager;
            _signInManager = signinManager;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("./Login");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            await _signInManager.SignOutAsync();
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            // Get current ip
            UserInformation userInformation = new UserInformation(this.Request);

            // create new user info
            var localaddress = userInformation.GetIpAddress();

            //get list of existing info
            var useInfo = _context.UserInfo.Where(m => m.Holder == user.Id).AsNoTracking().ToList();

            foreach (var addressinfo in useInfo)
            {
                //If the device exists
                var key = "WP46C8DF276ND5931069BDE2E695D45E";
                var decryption = DataEncryption.DecryptString(addressinfo.LocalAddress, key);
                if (decryption.Contains(localaddress))
                {
                    addressinfo.Status = true;
                    _context.UserInfo.Update(addressinfo);
                    await _context.SaveChangesAsync();
                }
            }
            StatusMessage = result.Succeeded ? "Thank you for verifying your email." : "Error verifying your email.";
            return RedirectToPage("./Login");
        }
    }
}
