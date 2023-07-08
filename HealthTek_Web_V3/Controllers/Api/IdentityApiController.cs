using HealthTek_Shared_Libraries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthTek_Web_V3.Controllers.Api
{
    [Authorize]
    [Route("api/identitylogin")]
    [ApiController]
    public class IdentityApiController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IdentityApiController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Get api/username/password
        [HttpGet]
        public async Task<ActionResult<AppUser>> GetIdentity(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            var check = await _userManager.CheckPasswordAsync(user, password);
            if (check)
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, true, lockoutOnFailure: true);
                if (!result.Succeeded)
                {
                    var token = _userManager.GetAuthenticatorKeyAsync(user);
                    return user;
                }
            }
            return NoContent();
        }
    }
}
