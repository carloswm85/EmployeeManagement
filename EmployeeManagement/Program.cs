using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    #region BUILDER

    // What is `builder`?
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddRazorPages();

    builder.Services.AddDbContextPool<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDBConnection")
            ?? throw new InvalidOperationException("Connection string 'EmployeeDBConnection' not found.")));


    /**
     * IDENTITY
     * https://youtu.be/kC9qrUcy2Js?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&t=199
     */

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 10;
    }).AddEntityFrameworkStores<AppDbContext>();

    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequiredUniqueChars = 3;
    });

    //builder.Services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
    builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

    // Add services to the container. This is where you configure services to be used by the app.
    builder.Services
        .AddControllersWithViews(options =>
        {
            // GLOBAL AUTHORIZATION is implemented for all user
            var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

            options.Filters.Add(new AuthorizeFilter(policy));

        }) // In this case, MVC services with views are added.
        .AddXmlDataContractSerializerFormatters() //
        ;

    //---------------------------------------- CLAIMS POLICY BASED AUTHORIZATION
    builder.Services.AddAuthorizationBuilder();

    builder.Services.AddAuthorization(options =>
    {
        // User for protecting controllers or routes
        // NOTE: The DeleteRolePolicy requires this 2 claims
        options.AddPolicy("DeleteRolePolicy",
            policy => policy.RequireClaim("Delete Role"));

        options.AddPolicy("EditRolePolicy",
            policy => policy.RequireClaim("Edit Role"));

        // Require multiple roles
        options.AddPolicy("AdminRolePolicy",
            policy => policy.RequireRole("Admin", "Verano", "Role 23"));

        options.AddPolicy("SuperAdminPolicy", policy =>
                  policy.RequireRole("Admin", "User", "Manager"));
    });

    //--------------------------------------------------------------------- NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    #endregion

    // What is `app`?
    var app = builder.Build();

    #region justSomeText

    // Sample text and application-specific information are concatenated here.
    var text = "Hello World!";
    var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName; // Gets the name of the current process.
    var mySettings = new MySettings();
    builder.Configuration.GetRequiredSection(nameof(MySettings)).Bind(mySettings); // Binds the configuration section to the MySettings object.
    var environment = app.Environment.EnvironmentName; // Gets the current environment name (e.g., Development, Production).

    var name = mySettings.Name; // Example: "Bob"
    var counter = mySettings.Counter; // Example: 100

    // Combine text and variables into a single string.
    var justSomeText = $"JUST SOME TEXT: {text} {processName} {name} {counter} {environment}";

    #endregion

    if (app.Environment.IsDevelopment())
    {
        // Enable the Developer Exception Page in the development environment.
        app.UseDeveloperExceptionPage();
    }
    else
    {
        // Use a custom error handling page for production.
        // app.UseExceptionHandler("/Home/Error");
        app.UseExceptionHandler("/Error");

        // REMEMBER THIS WORKS ONLY WITH:
        // "ASPNETCORE_ENVIRONMENT": "Production"

        // Returns error as plain text
        // app.UseStatusCodePages(); // (1)

        // Intersect error and return a view
        // {0} is a placeholder for the status code
        // app.UseStatusCodePagesWithRedirects("/Error/{0}"); // (2) Redirect to the string controller
        app.UseStatusCodePagesWithReExecute("/Error/{0}"); // (3) Re-executes the pipeline


        // Enable HTTP Strict Transport Security (HSTS) for enhanced security in production.
        // The default duration is 30 days; you can adjust this value based on your requirements.
        app.UseHsts();
    }

    // Redirect HTTP requests to HTTPS to enforce secure communication.
    app.UseHttpsRedirection();

    // The FileServer middleware combines DefaultFiles, StaticFiles, and optionally DirectoryBrowser functionalities.
    // It serves files from the wwwroot folder (or another specified folder) with default settings for static file handling.
    app.UseFileServer();

    
    //app.UseDefaultFiles(); // 1: Enables serving a default file (e.g., index.html) when a directory is accessed.
                           //    This middleware doesn't serve the file; it only rewrites the URL to include the default file.

    app.UseStaticFiles();  // 2: Serves static files (e.g., CSS, JS, images) from the wwwroot folder (or another specified folder).
                           //    This middleware is required to actually serve the files to the client.

    //app.UseDirectoryBrowser(); // 3: Enables directory browsing, allowing users to view the directory's file listing in the browser.
    //    This feature is disabled by default and must be explicitly enabled for security reasons.

    app.UseAuthentication();

    // Note: When using app.UseFileServer(), it includes the functionality of the above three middlewares:
    // 1. DefaultFiles is enabled by default.
    // 2. StaticFiles is always enabled.
    // 3. DirectoryBrowser is optional and must be explicitly configured via FileServerOptions if needed.

    // Uncomment the following line to simulate an exception for testing purposes.
    // app.MapGet("/", () => { throw new Exception("hola"); });

    // Add middleware for routing to endpoints (e.g., controllers, actions).
    app.UseRouting();

    // Add middleware for authorization. This is used to enforce security policies on requests.
    app.UseAuthorization();

    //
    // app.UseMvcWithDefaultRoute();
    //
    // app.UseMvc();

    // Define the default route for controllers.
    // It maps URLs to controllers and actions with an optional "id" parameter.
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    // Example of an outdated `UseMvc` block (NOT RECOMMENDED in .NET Core 8):
    /*
    app.UseMvc(routes => {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}"
        );
    });
    */


    // Define a route for the root path, which returns the justSomeText variable.
    //app.MapGet("/", () => justSomeText);

    // Start the application and listen for incoming requests.
    app.Run();

}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}


