using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Controllers
{

    public class HomeController : Controller
    {
        // Variables
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IdentityContext _context;

        // Constructor 
        public HomeController(ILogger<HomeController> logger,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IdentityContext context)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        // Unathenticated Landing page
        public async Task<IActionResult> Index()
        {
            var sidebar = HttpContext.Request.Cookies["SidebarMenu"];
            if (sidebar == null)
            {
                Set("SidebarMenu", " ", 365);
                Set("Theme", "primary.css", 365);
            }
            return View();
        }

        // Get Privacy
        [Route("Privacy")]
        public IActionResult Privacy() => View();

        // Get Terms
        [Route("Terms")]
        public IActionResult Terms() => View();

        /// <summary>
        /// Display regular error page
        /// </summary>
        /// <returns>An error view model with the request id or trace identifier</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        /// <summary>
        ///  Display 404 error page
        /// </summary>
        /// <returns>An 404 error page</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error404() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        /// <summary>
        /// Logs the User out
        /// </summary>
        /// <returns>The login page</returns>
        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);
            var login = await _context.Logins.FindAsync(user.FkLoginId);
            login.LogoutDate = DateTime.Now;
            _context.Logins.Update(login);
            await _context.SaveChangesAsync();
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return LocalRedirect("~/Login");
        }

        #region Theme
        // Sidebar Menu Persistance 
        /// <summary>
        /// Sets the cookie for sidebar menu persistance
        /// </summary>
        public void SetSideBarCookie()
        {
            string cookieValueFromContext = HttpContext.Request.Cookies["SidebarMenu"];
            switch (cookieValueFromContext)
            {
                case " ":
                    Set("SidebarMenu", "toggled", 365);
                    break;
                case "toggled":
                    Set("SidebarMenu", " ", 365);
                    break;
            }
        }

        // Theme Persistance 
        /// <summary>
        /// Sets the cookie for theme persistance
        /// </summary>
        public void SetThemeCookie(string theme)
        {
            switch (theme)
            {
                case "primary":
                    Set("Theme", "primary.css", 365);
                    break;
                case "success":
                    Set("Theme", "success.css", 365);
                    break;
                case "dark":
                    Set("Theme", "dark.css", 365);
                    break;
            }
        }

        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            HttpContext.Response.Cookies.Append(key, value, option);
        }
        #endregion
    }
}
