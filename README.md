- [Kudvenkat](#kudvenkat)
- [ASP.NET Core For Beginners](#aspnet-core-for-beginners)
  - [**Introduction**](#introduction)
  - [**WEB SERVER HOSTING**](#web-server-hosting)
  - [**WEB APP CONFIGURATION**](#web-app-configuration)
  - [**MVC**](#mvc)
  - [**DEPENDENCY INJECTION**](#dependency-injection)
  - [**PAGE VIEWS**](#page-views)
  - [**ROUTING**](#routing)
  - [**TAG HELPERS**](#tag-helpers)
  - [**MODELS**](#models)
  - [**SERVER SIDE VALIDATION**](#server-side-validation)
  - [**BOOTSTRAP**](#bootstrap)
  - [**ENTITY FRAMEWORK CORE**](#entity-framework-core)
  - [**Error Handling**](#error-handling)
  - [**Logging**](#logging)
  - [**ASP.NET Core Identity** - Authentication \& Authorization](#aspnet-core-identity---authentication--authorization)
    - [_IDENTITY SET UP_](#identity-set-up)
    - [_ROLES_](#roles)
    - [_USERS_](#users)
    - [_CLAIMS_](#claims)
    - [_AUTHORIZATION POLICY_](#authorization-policy)
    - [_EXTERNAL IDENTITY PROVIDERS_ - Google, Facebook, etc](#external-identity-providers---google-facebook-etc)
      - [Google](#google)
      - [Facebook](#facebook)
    - [Email Management](#email-management)
  - [**PASSWORD/TOKENS/ENCRYPTION/DECRyPTION MANAGEMENT**](#passwordtokensencryptiondecryption-management)
  - [**CLIENT SIDE VALIDATION**](#client-side-validation)
  - [**MSSQL SERVER**](#mssql-server)
  - [**Other**](#other)
- [C# Programming Language](#c-programming-language)
  - [C# Tutorial For Beginners](#c-tutorial-for-beginners)
  - [LINQ Tutorials](#linq-tutorials)
  - [SQL Tutorials](#sql-tutorials)
- [Other Topics](#other-topics)
- [NOTES](#notes)
  - [Libraries](#libraries)
  - [Delete Rule At SQL Server \& EF Core](#delete-rule-at-sql-server--ef-core)
  - [Proxy Variables Example](#proxy-variables-example)
  - [Configuration Sources](#configuration-sources)
    - [System Variables](#system-variables)
  - [Framework Update/Upgrade](#framework-updateupgrade)

---

# Kudvenkat

- <https://www.youtube.com/@Csharp-video-tutorialsBlogspot>
- <https://www.pragimtech.com/>

---

# ASP\.NET Core For Beginners

## **Introduction**

- [Creating asp net core web application 🔗](https://www.youtube.com/watch?v=4IgC2Q5-yDE&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 01 [📑](https://csharp-video-tutorials.blogspot.com/2018/12/aspnet-core-tutorial.html)
  - The course is for NET Core 2.2
- Setting up machine for ASP.NET Core Development [🔗](https://youtu.be/wKUWYOi442I?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 02 [📑](https://csharp-video-tutorials.blogspot.com/2018/12/setting-up-machine-for-aspnet-core.html)
- Creating a ASP.NET Core Web Application [🔗](https://youtu.be/KWidSz17tks?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 03 [📑](https://csharp-video-tutorials.blogspot.com/2019/01/creating-aspnet-core-web-application.html)
- Project file [🔗](https://youtu.be/6gnsUsjRTVo?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 04 [📑](https://csharp-video-tutorials.blogspot.com/2019/01/aspnet-core-project-file.html)
- Main method [🔗](https://youtu.be/X60RR34gKy0?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 05 [📑](https://csharp-video-tutorials.blogspot.com/2019/01/main-method-in-aspnet-core.html)

## **WEB SERVER HOSTING**

![Web Server](images/web-server.png)

- InProcess Hosting [🔗](https://youtu.be/ydR2jd3ZaEA?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 06
  - `WebApplication.CreateBuilder(args);` - One web server: Kestrel or IIS Express
    - IIS worker process (_w3wp.exe_ or _iisexpress.exe_)
    - No proxy request penalties
- OutOfProcess Hosting (_default_) [🔗](https://youtu.be/QsXsOX6qq2c?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 07
  - `dotnet.exe` process
  - Internal server: Kestrel
  - External web server (or _reverse proxy server_): IIS (Express), Nginx or Apache

## **WEB APP CONFIGURATION**

- `launchsettings.json` file [🔗](https://youtu.be/u2S4TkkACVc?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 08 [📑](https://csharp-video-tutorials.blogspot.com/2019/01/aspnet-core-launchsettingsjson-file.html)
- `IConfiguration` service:
  - `appsettings.json` [🔗](https://youtu.be/m_BevGi7zBw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 09 [📑](https://csharp-video-tutorials.blogspot.com/2019/01/aspnet-core-appsettingsjson-file.html)
  - Reading order: `appsettings.json`, `appsettings.{Environment}.json`, User secrets, Environment variables, lastly, CLI arguments.
- Middlewares introduction [🔗](https://youtu.be/ALu4jtvjSYw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 10 [📑](https://csharp-video-tutorials.blogspot.com/2019/01/middleware-in-aspnet-core.html)
  - Pipeline configuration [🔗](https://youtu.be/nt6anXAwfYI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 11
- Static files and default files [🔗](https://youtu.be/yt6bzZoovgM?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 12
- Development environments: Environment variables from OS [🔗](https://youtu.be/x8jNX1nb_og?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 14
  - _Development_, _Staging_, _Production_
  - `ASPNETCORE_ENVIRONMENT` for selecting the run environment.
- Using `libman` [🔗](https://youtu.be/5qzzjvZ4w0c?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 34
  - Use LibMan with ASP\.NET Core in Visual Studio [📑](https://learn.microsoft.com/en-us/aspnet/core/client-side/libman/libman-vs?view=aspnetcore-9.0#add-library-files)

## **MVC**

- MVC Design [🔗](https://youtu.be/f72ookCWhsQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 15
  - MVC Model = Model class and class repository
- NET Core MVC project set up (2.2 version) [🔗](https://youtu.be/KQH51Yip0K0?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 16
- MVC Implementation:
  - Model [🔗](https://youtu.be/KXPbJ9I4ce0?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 18
    - And simple use case for Dependency Injection
  - Controller [🔗](https://youtu.be/-O0UYM0ZIIc?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 20
    - Using Telerik Fiddler, receiving XML format
  - View [🔗](https://youtu.be/SWIcHLBnJUg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 21
    - Contains logic to display the Model data
    - Customize view discovery [🔗](https://youtu.be/gXiYrUoiinY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 22
- Passing data from controller to view:
  - Looslie typed views:
    - `ViewData` [🔗](https://youtu.be/tz4q6q0_JwQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 23
      - Dictionary of weakly typed objects as `ViewData["PageTitle"]`
    - `ViewBag` [🔗](https://youtu.be/FBvNz00o7jg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 24
      - Use dynamic properties of type `ViewBag.PageTitle`
    - Features:
      - _ViewBag_ is a wrapper to _ViewData_
      - Dynamically resolved at runtime. No type checking at compile time nor intellisense.
  - Strongly typed view [🔗](https://youtu.be/5auO0iXrOs4?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 25
  - ViewModels [🔗](https://youtu.be/Lu24lZsUreg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 26
    - We create a ViewModel when a Model object does not contain all the data a view needs.

## **DEPENDENCY INJECTION**

- Dependency Injection [🔗](https://youtu.be/BPGtVpu81ek?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 19
  - Benefits: Loose coupling, Easier unit testing
  - Lifetime service registration [🔗](https://youtu.be/v6Nr7Zman_Y?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 44
    - Notes [🔗](https://csharp-video-tutorials.blogspot.com/2019/04/addsingleton-vs-addscoped-vs.html) [🖼️](./images/service-registration.png) - Consider: Service instance within current HTTP request.
    - `builder.Services.AddSingleton()` - Creates a single instance of the service when it is first requested, and reuses that same instance in all the places where that service is needed - _3 4 5 6 7... So on and so foth._
    - `builder.Services.AddScoped()` - A new instance of a Scoped service is created once per request within the scope. For example, in a web application it creates 1 instance per each http request but uses the same instance in the other calls within that same web request. - _3 4 4 4 4 4... So on and so foth._
    - `builder.Services.AddTransient()` - A new instance of a Transient service is created each time it is requested. - _3 3 3 3 3... So on and so foth._

## **PAGE VIEWS**

- Layout View [🔗](https://youtu.be/Px8nwoO7FO8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 28
- ListView [🔗](https://youtu.be/nHAMDUtiV6w?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 27
- Render Sections in Layout Page [🔗](https://youtu.be/9OyrzRVZT8o?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 29
  - For the organization of page elements. They can be optional or mandatory.
- `_ViewStart.cshtml` [🔗](https://youtu.be/r7WgjrTSlO8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 30
- `_ViewImports.cshtml` [🔗](https://youtu.be/5HskoMcun9A?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 31
  - Used to include common namespaces.
  - View directives: `@addTagHelper` `@removeTagHelper` `@tagHelperPrefix` `@model` `@inherits` `@inject`

## **ROUTING**

- Conventional Routing [🔗](https://youtu.be/ZoxrbrHjj2g?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 32
- Attribute Routing [🔗](https://youtu.be/prNptonJAiY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 33
  - Applied to the controllers or to the controller actions methods.
  - NOTE: The _controller route template_ is not combined with _action method route template_, if the route template on the action method begins with `/` or `~/`
  - Tokens in attribute routing: `[Route("[controller]/[action]")]`

## **TAG HELPERS**

- Notes: Similar to _HTML Helpers_. Server side components for HTML rendering. Use: Link generation, form creation, asset load, etc.
- Tag Helpers & HTML Helpers [🔗](https://youtu.be/iaSdTMdReWg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 35
- Why use Tag Helpers [🔗](https://youtu.be/pXvizSVe-eQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 36
- Image Tag Helper [🔗](https://youtu.be/4jW9T-TLPTM?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 37
  - Provide cache-busting behaviour for static image files.
- Environment Tag Helper [🔗](https://youtu.be/-E4zP2L-R_U?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 38
- Form Tag Helpers [🔗](https://youtu.be/mU4hV50rkVE?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 40
  - `Form Tag Helper`, `Input Tag Helper`, `Label Tag Helper`, `Select Tag Helper`, `TextArea Tag Helper`, `Validation Tag Helper`

## **MODELS**

- Model Binding [🔗](https://youtu.be/-GkZERrqEQo?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 41
  - "Model binding maps data in an HTTP request to controller action method parameters. The action parameters say be simple or complext types."
  - `name` input attribute value is used for mapping.
  - Data available in the HTTP request (with preeminence order): `Form values` → `Route values` → `Query strings`
- Update a model:
  - Edit View - GET [🔗](https://youtu.be/lhiIvx7jMaY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 55
    - `Edit.cshtml` with controller and viewmodel.
  - Edit View - POST [🔗](https://youtu.be/wamToyK4x7I?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 56
- Model Binding Not Working with FOREACH loop [🔗](https://youtu.be/Qobkh8gEP6Q?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 92

## **SERVER SIDE VALIDATION**

- Model Validation [🔗](https://youtu.be/aDRC_IgwmH8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 42
- Custom validation attributes [🔗](https://youtu.be/o_AH2MGti0A?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 76
- Select list validation [🔗](https://youtu.be/woFHR3iNEEI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 43

## **BOOTSTRAP**

- Navigation Menu [🔗](https://youtu.be/l2dzzuxvmxk?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 39

## **ENTITY FRAMEWORK CORE**

- Introduction [🔗](https://youtu.be/OE0_9c-K-Ow?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 45
- Notes:
  - ORM, Domain Classes, Code/Database First, Database Provider Models
  - `Domain & DBContext Classes` → `EF Core` → `DB Provider` → `Actual DB`
- Installation & Multilayer Web Application [🔗](https://youtu.be/8aHzSx-inDE?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 46
  - `Presentation Layer` → `Business Layer` → `Data Access Layer`
- `DbContext` [🔗](https://youtu.be/nN9jOORIFtc?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 47
- Using with SQL Server [🔗](https://youtu.be/xMktEpPmadI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 48
  - Connection string
- Repository Pattern [🔗](https://youtu.be/qJmEI2LtXIY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 49
  - Abstraction of the Data Access Layer, for CRUD operations. How to use `AddScoped` with SQL Server.
- Migrations [🔗](https://youtu.be/G14lmWS-h4k?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 50
  - Migrations keep the DB schema and app model classes in sync.
  - Commands: `get-help about_entityframeworkcore` `Add-Migration` `Update-Database`
- Seed Data to Database [🔗](https://youtu.be/qDUS8ocavBU?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 51
- Keeping _domain models_ and _database schema_ in sync [🔗](https://youtu.be/MhvOKHUWgiY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 52
  - `Remove-Migration`
  - This video also explains how to remove migration that has already been applied to the database.
- File upoad [🔗](https://youtu.be/aoxEJii70_I?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 53
  - `IFormFile`, the video includes jQuery code for `.custom-file-label`.
  - Notes:
    - File is saved to the web server location `wwwroot/images`
    - The file uploaded to the server can be accessed through Model Binding using the IFormFile interface.
  - Upload multiple files at once [🔗](https://youtu.be/14ZqBoQIW-Q?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 54

## **Error Handling**

- Handling `404` Not Found Errors:
  - Type 1: Resource with ID not found [🔗](https://youtu.be/QiF3eJ4Zb0o?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 57
  - Type 2: URL does not match any route.
    - Centralized `404` Error Handling (`400`-`599`):
      - `UseStatusCodePages` [🔗](https://youtu.be/DVo138knAHQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 58
      - User facing: `UseStatusCodePagesWithRedirects` and `UseStatusCodePagesWithReExecute` [🔗](https://youtu.be/9CwgiSxrkeQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 59
- Correct Global Exception handling:
  - Not production: `UseExceptionHandler` [🔗](https://youtu.be/jeBttUIqpuc?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 60
- Custom Error Page (for `ON DELETE NO ACTION`) [🔗](https://youtu.be/0jqZ63ybeRY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 90

## **Logging**

- Basics, Logging from console, Built-in Logging Providers [🔗](https://youtu.be/WyAJe6lA-bY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 61
- Logging Exceptions [🔗](https://youtu.be/LhpO2sgxqfw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 62
- Logging to a file [🔗](https://youtu.be/o5u4fE0t79k?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 63
- `LogLevel` configuration [🔗](https://youtu.be/bTPnT13Efd4?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 64

## **ASP\.NET Core Identity** - Authentication & Authorization

- Google search [🔎](https://www.google.com/search?q=asp+net+core+identity+documentation)
- Official documentation [📑](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio)

### _IDENTITY SET UP_

- Inherit from `IdentityDbContext`, add services, add middleware, generate tables [🔗](https://youtu.be/egITMrwMOPU?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 65
- Register new user [🔗](https://youtu.be/sPbDrqpme_w?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 66
- Services: `UserManager` and `SignInManager` [🔗](https://youtu.be/TfarnVqnhX0?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 67
- Password complexity [🔗](https://youtu.be/kC9qrUcy2Js?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 68
- Show/hide view elements using `@if (SignInManager.IsSignedIn(User)) { }` [🔗](https://youtu.be/YLAHIZmO2PI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 69
- Implementing log in functionality [🔗](https://youtu.be/9d8DXXc71RI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 70
  - `LoginViewModel`, Login View, Login Actions in Account Controller.
- Authorizacion implementation - Types: Simple authorization, Role based, Claims based, Policy based.
  - Implementation as a service, _Simple Authorization_ [🔗](https://youtu.be/uET7MjhUeY4?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 71
  - Redirection after login [🔗](https://youtu.be/-asykt9Zo_w?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 72
    - NOTE: _Security risk_ - Open redirect attack/vulnerability [🔗](https://youtu.be/0q0CZTliQ7A?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 73
- Extend `IdentityUser` class [🔗](https://youtu.be/NV734cJdZts?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 77

### _ROLES_

- Create Roles [🔗](https://youtu.be/TuJd2Ez9i3I?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 78
- Get list of roles [🔗](https://youtu.be/KGIT8P29jf4?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 79
- Edit existing role [🔗](https://youtu.be/7ikyZk5fGzk?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 80
- Add or remove users [🔗](https://youtu.be/TzhqymQm5kw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 81
  - Tables for: Users, Roles, UserRoles
- _Role based authorization_ (RBAC) [🔗](https://youtu.be/DXVe6skc42k?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 82
  - Show or hide navigation menu based on user [🔗](https://youtu.be/IPjK65ehQBg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 83
    - See part-94 for more.
- User role membership: Add/remove roles for user [🔗](https://youtu.be/1OaVUy1pRXA?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)

### _USERS_

- List users, register new user and redirect correctly [🔗](https://youtu.be/OMX0UiLpMSA?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 84
- Edit user information, roles and claims [🔗](https://youtu.be/QYlIfH8qyrU?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 85
- Delete user [🔗](https://youtu.be/MhNfyZGfY-A?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 86
  - Display delete confirmation [🔗](https://youtu.be/hKLjt9GzYM8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 87
- Delete role [🔗](https://youtu.be/pj3GCelrIGM?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 88
- Manager user roles [🔗](https://youtu.be/1OaVUy1pRXA?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 91

### _CLAIMS_

- Manage User Claims [🔗](https://youtu.be/5XA4Z-SOif8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 93
  - "Claim": Name-value pair used for making _access control decisions_.
  - They are a piece of information about the user, NOT what the user can or cannot do.
  - Claims are "policy based".
- Claims Based Authorization (CBAC) [🔗](https://youtu.be/LJQBBvJ6tL0?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 94
  - _What are they?_ • `Services.AddAuthorization` • `AddPolicy` • `RequireClaim` • Use in controllers and actions • Combination of _roles_ and _policies_.
- **Role Based Authorization** (RBAC) Vs **Claim Base Authorization** (CBAC) [🔗](https://youtu.be/Uw2ujXvN3i4?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 95
  - `[Authorize(Roles = "Admin")]` is role based.
  - `[Authorize(Policy = "DeleteRolePolicy")]` is claim based.
- Authorization in views [🔗](https://youtu.be/72zYJw0nF-k?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 96
  - _Claim based_ authorization checks in views.
  - See part-83 for related information.

### _AUTHORIZATION POLICY_

- Using "claim type" and "claim value" in policy based authorization [🔗](https://youtu.be/I2wgxzLbESA?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 98
- Create custom authorization policy (Policy with multiple requirements) [🔗](https://youtu.be/KJprzM49NnU?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 99
  - See "Part 100 Func delegate in C#" for related information.
- Custom authorization requirements and handlers - EXPLANATION [🔗](https://youtu.be/1qdtjlKDJJ0?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 100
  1. BUILT-IN Authorization Requirement
     1. Policies with one requirement. → `RequireClaim`
     1. Policies with multiple requirements:
        1. Simple relationships → `RequireClaim` + `RequireRole`
        2. Complex relationships → `RequireAssertion`
  2. CUSTOM Authorization Requirement
     1. Implement `IAuthorizationRequirement` → `IAuthorizationHandler<T>` where T is the requirement
- Custom authorization requirements and handlers - EXAMPLE [🔗](https://youtu.be/cXsYer31UPo?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 101
- Multiple authorization handlers for a requirement [🔗](https://youtu.be/aKEN2Z-jfgc?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 102
- Custom authorization handler: SUCCESS Vs FAILURE, and NOTHING (`Task.CompletedTask`) [🔗](https://youtu.be/119eY23O-RE?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 103

### _EXTERNAL IDENTITY PROVIDERS_ - Google, Facebook, etc

- [OFFICIAL DOCUMENTATION 📑](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/?view=aspnetcore-8.0)
- Error when connecting from behind a bypassed proxy: You may need to unset [these Windows environment variables](#proxy-variables-example).

#### Google

- Introduction, how it works [🔗](https://youtu.be/ZgPK51X5BGw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 104
- Create Google OAuth Credentials - Obtain Client Id & Secret [🔗](https://youtu.be/V4KqpIX6pdI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 105
  - <https://console.cloud.google.com/>
  - Google+ API: <https://console.cloud.google.com/marketplace/product/google/plus.googleapis.com>
    - <https://developers.google.com/+/api-shutdown>
  - Console work:
    - Delete resource: <https://console.cloud.google.com/cloud-resource-manager?organizationId=0>
    - Google Auth: <https://console.cloud.google.com/auth/audience>
    - Credentials: <https://console.cloud.google.com/apis/credentials>
  - NOTES for Visual Studio 2022:
    - Visual Studio 2022 → Project properties → Debug
      - Enable SSL. From here copy `https://localhost:44370/` and paste it in...
      - App URL: `https://localhost:44370/`
      - Copy, paste the same address in the followin section...
  - NOTES for Google Cloud:
    - Project name: `Employee Mgmt STS` - STS, stands for "Security Token Service"
    - Credentials for Web Application → OAuth 2.0 Client name: `Employee Mgmt Client`
      - Authorized JavaScript origins: `https://localhost:44370` - The HTTP origins that host your web application.
      - Authorized redirect URIs: `https://localhost:44370/signin-google` - Users will be redirected to this path after they have authenticated with Google.
- Google Authentication, setting up the UI and the authentication service [🔗](https://youtu.be/fgzRnlB992s?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 106
- Handle authenticated user information received from Google: `ExternalLoginCallback` Action [🔗](https://youtu.be/vkB2yaV7_LQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 107

#### Facebook

- Register application with Facebook [🔗](https://youtu.be/uAymQERp90w?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 108
  - <https://developers.facebook.com/apps/>
    - App Name: `Employee Mgmt Client`
    - Use case: `Authenticate and request data from users with Facebook login`
  - <https://developers.facebook.com/apps/-app-id-/add/> - Add Facebook login product, change settings
    - Settings:
      - `Client OAuth login`: On
      - `Valid OAuth Redirect URIs`: `https://localhost:44370/signin-facebook` - Base URL comes from project properties → Debug → General → App URL
  - <https://developers.facebook.com/apps/640033055116902/use_cases/customize/>
    - Facebook Login → Permissions → email → `Add`
  - <https://developers.facebook.com/apps/-app-id-/settings/basic/>
    - Privacy Policy URL: `https://localhost:44370/Home/Privacy`
    - User Data Deletion → Data deletion callback URL: `https://localhost:44370/Administration/DeleteUserData/Facebook` (The method has not been added)
    - Category: `Education`
    - App icon: `EmployeeManagement/EmployeeManagement/wwwroot/images/employees_1024x1024.png`
- Facebook authentication code integration [🔗](https://youtu.be/R_1OW8PyiRI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 109

### Email Management

- Why email confirmation is important for app security [🔗](https://youtu.be/MChbBMLS2FQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 111 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/why-email-confirmation-is-important.html)
- Block log in if email is not confirmed [🔗](https://youtu.be/4XugKqgwGnU?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 112 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/block-login-if-email-is-not-confirmed.html)
- Email confirmation for internal accounts [🔗](https://youtu.be/yRP6C7fhAuE?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 113 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/aspnet-core-email-confirmation.html)
  - Using token providers.
- External login email confirmation [🔗](https://youtu.be/k_q5ZSh07t4?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 114 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/external-login-email-confirmation-in.html)
  - External registration is allowed, but external login is blocked until email confirmation is performed.

## **PASSWORD/TOKENS/ENCRYPTION/DECRyPTION MANAGEMENT**

- Password:
  - Forgot password [🔗](https://youtu.be/0W0yAz7fu04?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 115 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/forgot-password-in-aspnet-core.html)
  - Reset password [🔗](https://youtu.be/72Eu92ZkgCg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 116 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/reset-password-in-aspnet-core.html)
- Tokens:
  - How Tokens are generated and validated [🔗](https://youtu.be/fOQjWUokhn8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 117 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/how-tokens-are-generated-and-validated.html)
    - Generated token contains:
      - (`Token Creation Time` + `User ID` + `Token Purpose` + `Security Stamp`) ← Encrypted and then Base64 Encoded
  - Password reset token lifetime (_built-in_ method) [🔗](https://youtu.be/gX6CW8c4Huw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 118 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/aspnet-core-password-reset-token.html)
  - Password reset token lifetime (_CUSTOM_ method) [🔗](https://youtu.be/lYTXJrJGg0U?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 119 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/aspnet-core-custom-token-provider.html)
- Encryption and Decryption:
  - Encryption and Decryption Examples [🔗](https://youtu.be/HlHDTQhVYoI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 120 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/aspnet-core-encryption-and-decryption.html)
- Change Password [🔗](https://youtu.be/r7VzoLhFLd0?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 121 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/change-password-in-aspnet-core.html)
  - With block user access to action controller example.
- Add password to local account linked to external login (Facebook, Google) [🔗](https://youtu.be/mCKdMgFv8MI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 122 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/add-password-to-local-account-linked-to.html)
- Account lockout [🔗](https://youtu.be/jHRWR36UC2s?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 123 [📑](https://csharp-video-tutorials.blogspot.com/2019/11/aspnet-core-account-lockout.html)
- [🔗]() - Part 124 [📑]()

## **CLIENT SIDE VALIDATION**

- Client side validation implementation [🔗](https://youtu.be/PUX3PzyBofg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 74
  - Requirements (in this order): `jquery.js`, `jquery.validate.js`, `jquery.validate.unobtrusive.js`
- Remote validation [🔗](https://youtu.be/2jZc11l67Zk?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 75

## **MSSQL SERVER**

- Enforce `ON DELETE NO ACTION` [🔗](https://youtu.be/txTZAFut9mA?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 89

## **Other**

- Change default access denied route [🔗](https://youtu.be/1Mi9Y9GAuCw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 97
  - _Cascading referential integrity constraint_
- Secret Manager in ASP\.NET Core [🔗](https://youtu.be/TVF9o5qbrkI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 110 [📑](https://csharp-video-tutorials.blogspot.com/2019/10/aspnet-core-secret-manager.html)
  - Use: "Keep production secrets like database connection string, API and encryption keys out of source control."

---

# C# Programming Language

Videos and tutorials mentioned in this course.

## C# Tutorial For Beginners

[Full tutorial 🔗](https://www.youtube.com/playlist?list=PLAC325451207E3105)

- Some topics related to the main tutorial of this file:
  - Delegates [🔗](https://youtu.be/D2h46fvQX04?list=PLAC325451207E3105) - Part 36
  - Lambda Expressions [🔗](https://youtu.be/LDgQ-spnrYY?list=PLAC325451207E3105) - Part 99
  - `Task`, `async`, `await` [🔗](https://youtu.be/C5VhaxQWcpE?list=PLAC325451207E3105) - Part 101
    - `Thread`, `Action` [🔗](https://youtu.be/SgHYVPKJRX8?list=PLAC325451207E3105) - Part 102
  - Func Delegates [🔗](https://youtu.be/3Q4LKCIYrzQ?list=PLAC325451207E3105) - Part 100

## LINQ Tutorials

[LINQ Tutorial 🔗](https://www.youtube.com/playlist?list=PL6n9fhu94yhWi8K02Eqxp3Xyh_OmQ0Rp6a)
[LINQ to SQL 🔗](https://www.youtube.com/playlist?list=PL6n9fhu94yhXCHPed2Q9oBkgvzw9Re8hC)
[LINQ to XML 🔗](https://www.youtube.com/playlist?list=PL6n9fhu94yhX-U0Ruy_4eIG8umikVmBrk)

- Extension methods [🔗](https://youtu.be/VkrKNXscoto?list=PL6n9fhu94yhWi8K02Eqxp3Xyh_OmQ0Rp6)

## SQL Tutorials

- [SQL Server Performance Tuning and Query Optimization 🔗](https://www.youtube.com/playlist?list=PL6n9fhu94yhXg5A0Fl3CQAo1PbOcRPjd0)
- [SQL Server Interview Questions and Answers 🔗](https://www.youtube.com/playlist?list=PL6n9fhu94yhXcztdLO7i6mdyaegC8CJwR)
- [SQL Server tutorial for beginners 🔗](https://www.youtube.com/playlist?list=PL08903FB7ACA1C2FB)
  - Cascading referential integrity constraint [🔗](https://youtu.be/ETepOVi7Xk8?list=PL08903FB7ACA1C2FB) - Part 5

---

# Other Topics

- [How to (Quickly) Incorporate Sass Run-Time Compilation to ASP.NET Core Project 🔗](https://finees1985.wordpress.com/2023/04/08/how-to-quickly-incorporate-sass-run-time-compilation-to-asp-net-core-project/)
  - <https://bootswatch.com/>
    - <https://bootswatch.com/spacelab/>

---

# NOTES

## Libraries

- <https://cdnjs.com/>
- [jquery-validate 🔗](https://jqueryvalidation.org/)

## Delete Rule At SQL Server & EF Core

| **SQL Server** | **EF Core**           | **Behavior**                                                                                                                                     |
| -------------- | --------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------ |
| _Delete rule_  | enum `DeleteBehavior` | The enumeration representing the delete behavior in EF Core.                                                                                     |
| **NoAction**   | `.ClientSetNull`(0)   | If a parent entity is deleted, the dependent entity's foreign key is set to `null` in the client memory but throws if the database disallows it. |
| -              | `.Restrict`(1)        | Prevents deletion of the parent entity if related entities exist.                                                                                |
| **SetNull**    | `.SetNull`(2)         | When a parent entity is deleted, foreign keys in dependent entities are set to `null`.                                                           |
| **Cascade**    | `.Cascade`(3)         | DEFAULT. When a parent entity is deleted, related child entities are also deleted.                                                               |
| -              | `.ClientCascade`(4)   | The dependent entities are deleted in client-side memory but require a save to propagate to the database.                                        |
| **NoAction**   | `.NoAction`(5)        | EF Core does not perform any action, and the database enforces referential integrity constraints.                                                |
| **SetDefault** | `.ClientNoAction`(6)  | No action is performed on the dependent entities in the client; the database handles `SET DEFAULT`.                                              |

## Proxy Variables Example

- For Windows enviroment variables:

```terminal
HTTP_PROXY = http://10.1.33.254:80
HTTPS_PROXY = https://10.1.33.254:80
NO_PROXY = localhost,127.0.0.1,::1,LOCALHOST
```

## Configuration Sources

| Configuration Source                     | Course<br>Part | Override |
| ---------------------------------------- | -------------- | -------- |
| `appsettings.json`                       | 9              | 1st      |
| `appsettings.{env.EnvironmentName}.json` | -              | 2nd      |
| User secrets                             | 110            | 3rd      |
| Environment variables                    | 14             | 4th      |
| Command-line arguments                   | -              | 5th      |

- `env.EnvironmentName`: `Development`

### System Variables

- Environment variables are configured at operating system level.
- Example:
  - `Variable name`: `ConnectionString:EmployeeDBConnection`
  - `Variable value`: `Server=localhost;Database=EmployeeDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=False`

## Framework Update/Upgrade

- .NET Standard: <https://learn.microsoft.com/en-us/dotnet/standard/net-standard>
- UPDATE/UPGRADE:
  - Upgrade to a new .NET version: <https://learn.microsoft.com/en-us/dotnet/core/install/upgrade>
  - Migrate from Windows Forms .NET Framework to .NET: <https://learn.microsoft.com/en-us/dotnet/desktop/winforms/migration/>
  - Overview of porting from .NET Framework to .NET: <https://learn.microsoft.com/en-us/dotnet/core/porting/>
