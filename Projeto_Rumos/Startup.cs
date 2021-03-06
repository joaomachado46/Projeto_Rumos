using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Models;
using Projeto_Rumos.ApiConector;
using Projeto_Rumos.Areas.Identity.Pages.Account.UserData;
using WebApiFrutaria.DataContext;

namespace Projeto_Rumos
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
            //DEFINIR A CONNECTION STRING
            services.AddDbContext<ContextApplication>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ApiConnectionString")));


            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ContextApplication>();

            services.AddTransient<DadosStorage>();
            services.AddTransient<AuthenticatedUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ApiConnector>();

            //AUTENTICAÇÃO GOOGLE
            services.AddAuthentication()
            .AddGoogle(options =>
                {
                    options.ClientId = "442308774417-f1a23siosvt9ogpejbvnk6t3f99dfemt.apps.googleusercontent.com";
                    options.ClientSecret = "qNOhgoMq2RcUL1PkA-K73odz";
                });
            //AUTENTICAÇÃO FACEBOOK
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "714895086064263";
                options.AppSecret = "1ae106909b1d2aa9bdf5f201f16a6602";
                options.AccessDeniedPath = "/AccessDeniedPathInfo";
            });
            //services.AddApiVersioning();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //PARA ACEITAR VARIOS FORMATOS(necessario instalar o nuget: Microsoft.AspNetCore.Mvc.Formatters.Xml)
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
                .AddXmlSerializerFormatters();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
