- [Kudvenkat](#kudvenkat)
  - [Topics](#topics)
    - [DOTNET](#dotnet)
      - [Web Server Hosting](#web-server-hosting)
      - [Other](#other)
    - [C#](#c)

---

# Kudvenkat

1. [Creating asp net core web application 🔗](https://www.youtube.com/watch?v=4IgC2Q5-yDE&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
   1. The course is for NET Core 2.2

## Topics

### DOTNET

#### Web Server Hosting

![Web Server](images/web-server.png)

- InProcess Hosting [🔗](https://youtu.be/ydR2jd3ZaEA?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - `WebApplication.CreateBuilder(args);` - One web server: Kestrel or IIS Express
    - IIS worker process (_w3wp.exe_ or _iisexpress.exe_)
    - No proxy request penalties
- OutOfProcess Hosting (_default_) [🔗](https://youtu.be/QsXsOX6qq2c?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - `dotnet.exe` process
  - Internal server: Kestrel
  - External web server (or _reverse proxy server_): IIS (Express), Nginx or Apache

#### Other

- `IConfiguration` service:
  - `appsettings.json` [🔗](https://youtu.be/m_BevGi7zBw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - Reading order: `appsettings.json`, `appsettings.{Environment}.json`, User secrets, Environment variables, lastly, CLI arguments.
- Middlewares introduction [🔗](https://youtu.be/ALu4jtvjSYw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - Pipeline configuration [🔗](https://youtu.be/nt6anXAwfYI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
- Static files and default files [🔗](https://youtu.be/yt6bzZoovgM?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
- Development environments [🔗](https://youtu.be/x8jNX1nb_og?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - _Development_, _Staging_, _Production_
  - `ASPNETCORE_ENVIRONMENT` for selecting the run environment.
- MVC Design [🔗](https://youtu.be/f72ookCWhsQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - MVC Model = Model class and class repository
- NET Core MVC project set up (2.2 version) [🔗](https://youtu.be/KQH51Yip0K0?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
- MVC Implementation:
  - Model [🔗](https://youtu.be/KXPbJ9I4ce0?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
    - And simple use case for Dependency Injection
  - Controller [🔗](https://youtu.be/-O0UYM0ZIIc?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
    - Using Telerik Fiddler, receiving XML format
  - View [🔗](https://youtu.be/SWIcHLBnJUg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
    - Contains logic to display the Model data
    - Customize view discovery [🔗](https://youtu.be/gXiYrUoiinY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
- Dependency Injection [🔗](https://youtu.be/BPGtVpu81ek?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - Benefits: Loose coupling, Easier unit testing
  - Lifetime determination:
    - `builder.Services.AddSingleton()` - Instance creation once, same instance, all requests, during whole app life.
    - `builder.Services.AddTransient()` - New instance every time is requested.
    - `builder.Services.AddScoped()` - New instance once per request withing the same scope.
- Passing data from controller to view:
  - Looslie typed views:
    - `ViewData` [🔗](https://youtu.be/tz4q6q0_JwQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Dictionary of weakly typed objects as `ViewData["PageTitle"]`
    - `ViewBag` [🔗](https://youtu.be/FBvNz00o7jg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Use dynamic properties of type `ViewBag.PageTitle`
    - Features:
      - _ViewBag_ is a wrapper to _ViewData_
      - Dynamically resolved at runtime. No type checking at compile time nor intellisense.
  - Strongly typed view [🔗](https://youtu.be/5auO0iXrOs4?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - ViewModels [🔗](https://youtu.be/Lu24lZsUreg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
    - We create a ViewModel when a Model object does not contain all the data a view needs.
- Page Views:
  - Layout View [🔗](https://youtu.be/Px8nwoO7FO8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - ListView [🔗](https://youtu.be/nHAMDUtiV6w?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - Sections in Layout Page [🔗](https://youtu.be/9OyrzRVZT8o?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
    - For the organization of page elements. They can be optional or mandatory.
  - `_ViewStart.cshtml` [🔗](https://youtu.be/r7WgjrTSlO8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - First page to execute.
  - `_ViewImports.cshtml` [🔗](https://youtu.be/5HskoMcun9A?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Used to include common namespaces.
    - View directives: `@addTagHelper` `@removeTagHelper` `@tagHelperPrefix` `@model` `@inherits` `@inject`
- Routing:
  - Conventional Routing [🔗](https://youtu.be/ZoxrbrHjj2g?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - Attribute Routing [🔗](https://youtu.be/prNptonJAiY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)  - Applied to the controllers or to the controller actions methods.
    - NOTE: The *controller route template* is not combined with *action method route template*, if the route template on the action method begins with `/` or `~/`
    - Tokens in attribute routing: `[Route("[controller]/[action]")]`
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()

### C#

- Extension methods
- Delegates
- Lambda Expressions
