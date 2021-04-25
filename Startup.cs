using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace MartenTest
{
    public class Startup
    {


        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        private readonly IHostEnvironment _environment;


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MartenTest", Version = "v1" });
            });

            var connectionString = Configuration.GetConnectionString("postgres");

            services.AddMarten(opts =>
            {
                opts.Connection(connectionString);

                // Use the more permissive schema auto create behavior
                // while in development
                if (_environment.IsDevelopment())
                {
                    opts.AutoCreateSchemaObjects = AutoCreate.All;
                }

                opts.CreateDatabasesForTenants(c =>
                {
                    c.ForTenant()
                        .CheckAgainstPgDatabase()
                        .WithOwner("fitogram")
                        .WithEncoding("UTF-8")
                        .ConnectionLimit(-1)
                        .OnDatabaseCreated(_ =>
                        {
                            Console.WriteLine("DB created");
                        });
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MartenTest v1"));
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
