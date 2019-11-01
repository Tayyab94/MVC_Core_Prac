using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VenketCorePracticeCore.Models;
using VenketCorePracticeCore.Models.DataContext;

namespace VenketCorePracticeCore
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<DemoContext>(option => option.UseSqlServer(_config.GetConnectionString("VenketConnectionString")));

            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DemoContext>();

            // if we want to add new Custom property into UserTable then Add this reference. this appliction user class is our Custom Class that we have created in model folder.
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DemoContext>();

            services.AddMvc();
            //   services.AddMvcCore().AddJsonFormatters(s=>s.Formatting=s.Formatting);

            services.AddScoped<IEmployeeRepository, SqlEmployeeRepositories>();

            //services.AddScoped<IEmployeeRepository, EmployeeMockRepository>();
            //services.AddTransient<IEmployeeRepository, EmployeeMockRepository>();


            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "248981517183-u4pm1ms12dcp1rg5ndoauv58vonvjrsp.apps.googleusercontent.com";
                    options.ClientSecret="RyB5CPJAKtucmFH3rG-rqPb4";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {

           

            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions()
                {
                    SourceCodeLineCount = 25
                };
                app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            }
            else
            {
                /*  app.UseStatusCodePagesWithRedirects("/Error/{0}");*/  //This will Redirect to Error Controller


                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            FileServerOptions defaultFiles = new FileServerOptions();
            defaultFiles.DefaultFilesOptions.DefaultFileNames.Clear();

            defaultFiles.DefaultFilesOptions.DefaultFileNames.Add("fo.html");

            //app.UseDefaultFiles();
            app.UseStaticFiles();

            //app.UseFileServer(defaultFiles);
            //app.UseMvcWithDefaultRoute();

            app.UseMvc(route =>
            {
                route.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=ShowAllEmployee}/{id?}"
                        );
            });

           // app.UseFileServer();
            //app.Run(async (context) =>
            //{
            //    //await context.Response.WriteAsync("Hello World!");
            //   // throw new Exception("Some thing is Wrong Pleae Try Again ");

            //    //await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);

            //    logger.LogInformation("Message before the  Envirenemt verable calling");
            //    await context.Response.WriteAsync(_config["MyKey"]);

            //    logger.LogInformation("Message after the  Envirenemt verable calling");

            //});
        }
    }
}
