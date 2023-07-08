using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Areas.Identity.Pages.Account.Manage
{
    public class AllowedDevicesModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IdentityContext _context;
        public List<UserInfo> userInfo;
        public AllowedDevicesModel(UserManager<AppUser> userManager, IdentityContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> OnGet()
        {
            // get current user
            var user = await _userManager.GetUserAsync(User);
            // get list of existing info
            userInfo = _context.UserInfo.Where(m => m.Holder == user.Id).ToList();
            foreach (var item in userInfo)
            {
                var key = "WP46C8DF276ND5931069BDE2E695D45E";
                var decrypt = item.LocalAddress;
                item.LocalAddress = DataEncryption.DecryptString(decrypt, key);
            }

            return Page();

        }
    }
}
