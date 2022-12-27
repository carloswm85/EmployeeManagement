using EmployeeManagementCore22.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementCore22
{
    /*
     * MAIN PIECES:
     *  (1) ConfigureServices()
     *      - For application services set up.
     *  (2) Configure()
     *      - For application request processing pipeline set up.
     */
    public class Startup
    {
        // Store the injected service
        private IConfiguration _config;


        // CTOR: Inject IConfigurationService, dependency injection
        public Startup(IConfiguration config)
        {
            // Different configuration providers are set here.
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //  AddMvc internally calls AddMvcCore (AddMvc internally calls AddJsonFormatters)
            //  https://github.com/aspnet/Mvc/blob/release/2.2/src/Microsoft.AspNetCore.Mvc/MvcServiceCollectionExtensions.cs
            //services.AddMvc(); // This is a default service from the framework Microsoft.Extensions.DependencyInjection
            services.AddMvc().AddXmlSerializerFormatters();


            /* For custom servicesL
             * How to register with Dependency Injection Container: Use AddSingleton, AddTransient or AddScoped
             */

            // SINGLETON: Interface + Implementation = Singleton
            /* What does this do? If someone (Eg: HomeController) request IEmployeeRepository service, then create an
             * instance of MockEmployeeRepository and inject that instance to HomeController.
             * Why do it like this? This way, in case I use IEmployeeRepository in 100 controllers, I'll have to change
             * only this one line of code, change MockEmployeeRepository class for, let's say, SqlEmployeeRepository class
             */
            services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();

            // Other examples:
            //services.AddScoped<ILogger, ILogger>();
            //services.AddTransient<ILogger, ILogger>();
        }

        /* ================= PIPELINE CONFIGURATION ================= */
        // This method gets called by the runtime. Use this method to configure the HTTP request processing pipeline.
        // MIDDLEWARE SERV
        // Every MW is registered in the pipeline. 
        /*
         * app: 
         * env: Environment: Development, Staging, Production
         * logger: 
         */
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger) // Services injection
        {
            // logger is initially set in the WebHost class, inside Program.cs See: https://github.com/aspnet/MetaPackages/blob/release/2.2/src/Microsoft.AspNetCore/WebHost.cs

            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions depo = new DeveloperExceptionPageOptions()
                {
                    SourceCodeLineCount = 10 // display the selected amount of source code in the browser.
                };

                // MIDDLEWARE
                //app.UseDeveloperExceptionPage(depo);
                app.UseDeveloperExceptionPage();

            }
            else if (env.IsStaging() || env.IsProduction() || env.IsEnvironment("UAT"))
            {
                app.UseExceptionHandler("/Error");
            }

            // MIDDLEWARE
            // app.UseDefaultFiles(); // This MW must be registered before UseStaticFiles
            // Overload
            DefaultFilesOptions dfo = new DefaultFilesOptions();
            dfo.DefaultFileNames.Clear();
            dfo.DefaultFileNames.Add("foo.html"); // Change the default from index.html or default.html to custom foo.html
            //app.UseDefaultFiles(dfo);

            // MIDDLEWARE
            //app.UseDirectoryBrowser(); // Allows directory view on page, it requires UseStaticFiles

            // MIDDLEWARE
            FileServerOptions fso = new FileServerOptions();
            fso.DefaultFilesOptions.DefaultFileNames.Clear();
            fso.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            // Used for displaying the directory. Disable UseDefaultFiles and UseStaticFiles before using it.
            // It combines, UseStaticFiles, UseDefaultFiles, and UseDirectoryBrowser
            //app.UseFileServer();
            //app.UseFileServer(fso);


            // MIDDLEWARE, static files are NOT served by default
            app.UseStaticFiles(); // Register it before other MW, it serves CSS, JS and images from wwwroot, and any other document

            // MIDDLEWARE, before UseStaticFiles(), see method implementation
            // It redirects to controller of the form: '{controller=Home}/{action=Index}/{id?}'
            // If the controller does not exist, it will redirect to the terminal MW
            //app.UseMvcWithDefaultRoute(); // Incoming url or request handling

            // MIDDLEWARE, "conventional routing"
            //app.UseMvc(); // This alone, without routing, returns 404
            app.UseMvc(routes =>
            {
                //routes.MapRoute("default", "{controller}/{action}/{id}");
                routes.MapRoute("default", "pragim/{controller=Home}/{action=Index}/{id?}"); // this change to routing will break the code without using tag-helpers
                //routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); // optional id
            });

            // MIDDLEWARE
            //app.Use(async (context, next) => // next variable calls next MW in the pipeline
            //{
            //    logger.LogInformation("Incoming Request"); // INTO THE PIPELINE
            //    await next(); // GO NEXT MW
            //    logger.LogInformation("Outgoing Response"); // OUT OF THE PIPELINE
            //});


            //// MIDDLEWARE
            //app.Use(async (context, next) => // next variable calls next MW in the pipeline
            //{
            //    string value1 = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //    string value2 = _config["MyTestKey"];

            //    string myString = $"Hello World! \n{value1} \n{value2}";                

            //    await context.Response.WriteAsync(myString);
            //    await next();
            //});


            // MIDDLEWARE, terminal
            //app.Run(async (context) => // This last MW is a terminal MW in the pipeline
            //{
            //    //throw new Exception("Exception thrown."); // sed in UseDeveloperExceptionPage
            //    await context.Response.WriteAsync("\nTerminal Middleware  + " + env.EnvironmentName);
            //    //logger.LogInformation("Request handled and response produced.");

            //});
        }
    }
}
