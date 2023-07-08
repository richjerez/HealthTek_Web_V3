using DinkToPdf;
using DinkToPdf.Contracts;
using HealthTek_Shared_Libraries;
using HealthTek_Shared_Libraries.Data;
using HealthTek_Web_V3.Areas.Identity;
using HealthTek_Web_V3.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HealthTek_Web_V3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region Database

            var identityConnectString = Configuration.GetConnectionString("IdentityContextConnection");
            services.AddDbContextPool<IdentityContext>(_contextPool =>
            {
                _contextPool.UseSqlServer(identityConnectString, options =>
                {
                    options.CommandTimeout(180); // 3 minutes
                    options.EnableRetryOnFailure(); // Enable Browser Retry On Failure
                });
            });
            services.AddDatabaseDeveloperPageExceptionFilter();

            #endregion

            #region Identity

            // Adding Identity. This configuration must be set before other services
            services.AddIdentity<AppUser, UserRoles>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

            #endregion

            #region Cookies

            // Cookie consent-configuration
            services.Configure<CookiePolicyOptions>(options =>
            {
                //// This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.ConsentCookie.Name = "HealthTek_Web_V3";
                options.ConsentCookie.Expiration = TimeSpan.FromDays(360);
            });

            // Cookie session-configuration 
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(360);
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = "HealthTek_Web_V3";
            });

            // Cookie application-configuration
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Login";
                options.AccessDeniedPath = "/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(360);
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = ".HealthTek_Web_V3IdentityCookie";
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.IsEssential = true;
            });
            #endregion

            #region Policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthorizationViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "INSURANCE CLERK", "BILLER", "SUPERVISOR", "FILE CLERK", "V-AUTHORIZATIONS"));
                options.AddPolicy("IntakeViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "INSURANCE CLERK", "COMREL", "V-INTAKES"));
                options.AddPolicy("AppointmentViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "INSURANCE CLERK", "COMREL", "SUPERVISOR", "FILE CLERK", "V-APPOINTMENTS", "USER", "RRC", "HR", "BILLER"));
                options.AddPolicy("EmployeeViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "RRC", "SUPERVISOR", "HR", "COMREL", "BILLER", "FILE CLERK", "INSURANCE CLERK", "V-EMPLOYEES"));
                options.AddPolicy("ClientViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "RRC", "SUPERVISOR", "HR", "COMREL", "BILLER", "FILE CLERK", "INSURANCE CLERK", "USER", "V-CLIENTS"));
                options.AddPolicy("TaskViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "RRC", "SUPERVISOR", "HR", "COMREL", "BILLER", "FILE CLERK", "INSURANCE CLERK", "USER", "V-TASKS"));
                options.AddPolicy("AssingmentViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "RRC", "SUPERVISOR", "V-ASSIGNMENTS"));
                options.AddPolicy("InsuranceViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "RRC", "INSURANCE CLERK", "FILE CLERK", "V-INSURANCE CHECKS"));
                options.AddPolicy("QAViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "RRC", "SUPERVISOR", "V-QUALITY ASSURANCE"));
                options.AddPolicy("SupervisionViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "RRC", "SUPERVISOR", "RRC", "V-SUPERVISIONS"));
                options.AddPolicy("FileDropboxViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "RRC", "SUPERVISOR", "HR", "COMREL", "BILLER", "FILE CLERK", "INSURANCE CLERK", "USER", "V-FILE DROPBOX"));
                options.AddPolicy("FileInboxViews", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN", "HR", "BILLER", "FILE CLERK", "INSURANCE CLERK", "V-FILE INBOX"));
                options.AddPolicy("SUPERUSER", policy =>
                      policy.RequireRole("SUPERUSER"));
                options.AddPolicy("ADMIN", policy =>
                      policy.RequireRole("SUPERUSER", "ADMIN"));
            });
            #endregion

            #region PDF Reporting

            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddScoped<IReportService, ReportService>();

            #endregion

            // Google Identity Captcha  
            services.AddHttpClient<ReCaptcha>(x =>
            {
                x.BaseAddress = new Uri("https://www.google.com/recaptcha/api/siteverify");
            });

            // Email Configuration
            services.AddSingleton<EmailSender>();

            #region MVC

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home/Error404";
                    await next();
                }
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Restrict Register Pages
                endpoints.MapGet("/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Login", true, true)));
                endpoints.MapPost("/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Login", true, true)));

                endpoints.MapRazorPages();
            });
        }
    }
}
