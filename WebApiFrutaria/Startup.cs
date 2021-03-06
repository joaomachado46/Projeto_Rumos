using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Models;
using System;
using WebApiFrutaria.Business;
using WebApiFrutaria.Business.Implementation;
using WebApiFrutaria.DataContext;
using WebApiFrutaria.Repository.GenericRepository;

namespace WebApiFrutaria
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

            //CONECÇÃO A BASE DE DADOS
            var connection = Configuration.GetConnectionString("ApiConnectionString");

            services.AddDbContext<ContextApplication>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("WebApiFrutaria")));
           
  
            //PARA ACEITAR VARIOS FORMATOS(necessario instalar o nuget: Microsoft.AspNetCore.Mvc.Formatters.Xml)
            services.AddMvc(option =>
            {
                option.RespectBrowserAcceptHeader = true;
                option.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));
                option.FormatterMappings.SetMediaTypeMappingForFormat("json", MediaTypeHeaderValue.Parse("application/json"));
            })
                .AddXmlSerializerFormatters();

            services.AddControllers();

            //ÍNJEÇÃO DA DEPENDENDIA DE VERSIONAMENTO DA API
            services.AddApiVersioning();

            //ÍNJEÇÃO DA DEPENDENDIA DE ACESSO A AZURE STORAGE
            services.AddTransient<DadosStorage>();
            services.AddTransient<AuthenticatedUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddMemoryCache();

            //INJEÇÃO DO REPOSITORIO GENERICO DE ACESSO A BD
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //INJEÇÃO DA CAMADA BUSINESS DAS CLASS
            services.AddScoped<IProductBusiness, ProductBusinessImplementation>();
            services.AddScoped<IFuncionarioBusiness, FuncionarioBusinessImplementation>();
            services.AddScoped<IUsuarioBusiness, UsuarioBusinessImplementation>();
            services.AddScoped<ICarrinhoComprasBusiness, CarrinhoComprasBusinessImplementation>();
            //SWAGGER
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FrutariaApi",
                    Version = "v1",
                    Description = "Api developed in course",
                    Contact = new OpenApiContact
                    {
                        Name = "João Machado",
                        Url = new Uri("https://github.com/joaomachado46"),
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiFrutaria v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
