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
    public class Startup
    {
        // Store the injected service
        private IConfiguration _config;


        // Inject IConfigurationService, dependency injection
        public Startup(IConfiguration  config)
        {
            // Different configuration providers are set here.
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // PIPELINE CONFIGURATION
        // This method gets called by the runtime. Use this method to configure the HTTP request processing pipeline.
        // MIDDLEWARE SERV
        // Every MW is registered in the pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger) // Services injection
        {
            // logger is initially set in the WebHost class, inside Program.cs See: https://github.com/aspnet/MetaPackages/blob/release/2.2/src/Microsoft.AspNetCore/WebHost.cs

            if (env.IsDevelopment())
            {
                // MIDDLEWARE
                DeveloperExceptionPageOptions depo = new DeveloperExceptionPageOptions()
                {
                    SourceCodeLineCount = 10
                };

                app.UseDeveloperExceptionPage(depo);
                //app.UseDeveloperExceptionPage( ); 
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
            app.UseFileServer();
            //app.UseFileServer(fso);
            
            
            // MIDDLEWARE, static files are NOT served by default
            //app.UseStaticFiles(); // Register it before other MW, it serves CSS, JS and images from wwwroot, and any other document


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
            app.Run(async (context) => // This last MW is a terminal MW in the pipeline
            {
                throw new Exception("Exception thrown."); // sed in UseDeveloperExceptionPage
                await context.Response.WriteAsync("\nTerminal Middleware.");
                logger.LogInformation("Request handled and response produced.");

            });
        }
    }
}
