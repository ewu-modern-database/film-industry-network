using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Dnx.Runtime;
using Swashbuckle.Swagger;

namespace FilmIndustryNetwork
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var configurationBuilder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile($"config.{env.EnvironmentName}.json");

            configurationBuilder.AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddMvc();

            services.AddCors();

            services.AddSwagger();

            services.ConfigureSwaggerSchema(o =>
            {
                o.DescribeAllEnumsAsStrings = true;
            });

            services.ConfigureSwaggerDocument(o =>
            {
                o.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Film Industry Network API",
                    Description = "Documentation of the API for interfacing with the Film Industry Network",
                    TermsOfService = "Use at your own risk"
                });
            });

            services.AddRouting();

            //services.AddWebApiConventions();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();
            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
