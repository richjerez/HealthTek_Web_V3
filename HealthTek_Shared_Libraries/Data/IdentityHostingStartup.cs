using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(HealthTek_Shared_Libraries.Data.IdentityHostingStartup))]
namespace HealthTek_Shared_Libraries.Data
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}