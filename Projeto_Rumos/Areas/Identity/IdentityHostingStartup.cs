using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Projeto_Rumos.Areas.Identity.IdentityHostingStartup))]
namespace Projeto_Rumos.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}