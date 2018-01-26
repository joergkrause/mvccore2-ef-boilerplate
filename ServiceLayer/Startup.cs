using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using JoergIsAGeek.Workshop.BusinessLogicLayer;
using JoergIsAGeek.Workshop.DataAccessLayer;
using JoergIsAGeek.Workshop.DomainModel;
using JoergIsAGeek.Workshop.Repository;
using Swashbuckle.AspNetCore.Swagger;

namespace servicelayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DbContextOptions<PersonalManagerContext> options
            // services.AddDbContext<PersonalManagerContext>(options => {
            //     options.UseSqlServer(connString);
            // });
            services.AddTransient<IConfiguration>(s => Configuration);
            services.AddTransient<PersonalManagerContext>();
            services.AddTransient(typeof(IGenericRepository<CompanyUser>), typeof(GenericRepository<CompanyUser>));
            services.AddTransient(typeof(ICompanyUserManager), typeof(CompanyUserManager));

            services.AddSwaggerGen(cfg => {
                cfg.SwaggerDoc("v1", new Info { Title = "Workshop API", Version = "v1" });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(cfg => {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "Workshop API v1");
                // cfg.RoutePrefix = "openapi";
            });
            app.UseMvc();
        }
    }
}

// Client: Autorest, npm/nodejs, Microsoft.Rest.ClientRuntime.2.2.0 erforderlich