- [Kudvenkat](#kudvenkat)
  - [ASP.NET Core For Beginners](#aspnet-core-for-beginners)
    - [Web Server Hosting](#web-server-hosting)
    - [Topics](#topics)
    - [**SERVER SIDE VALIDATION**](#server-side-validation)
    - [**BOOTSTRAP**](#bootstrap)
    - [**ENTITY FRAMEWORK CORE**](#entity-framework-core)
    - [**ASP.NET Core Identity** - Authentication \& Authorization](#aspnet-core-identity---authentication--authorization)
    - [**CLIENT SIDE VALIDATION**](#client-side-validation)
    - [MSSQL SERVER](#mssql-server)
- [C# Programming Language](#c-programming-language)
  - [C# Tutorial For Beginners](#c-tutorial-for-beginners)
  - [LINQ Tutorials](#linq-tutorials)
  - [SQL Tutorials](#sql-tutorials)
- [Other Topics](#other-topics)
- [NOTES](#notes)
  - [Delete Rule](#delete-rule)

---

# Kudvenkat

1. [Creating asp net core web application 🔗](https://www.youtube.com/watch?v=4IgC2Q5-yDE&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 1
   1. The course is for NET Core 2.2

---

## ASP.NET Core For Beginners

### Web Server Hosting

![Web Server](images/web-server.png)

- InProcess Hosting [🔗](https://youtu.be/ydR2jd3ZaEA?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 6
  - `WebApplication.CreateBuilder(args);` - One web server: Kestrel or IIS Express
    - IIS worker process (_w3wp.exe_ or _iisexpress.exe_)
    - No proxy request penalties
- OutOfProcess Hosting (_default_) [🔗](https://youtu.be/QsXsOX6qq2c?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 7
  - `dotnet.exe` process
  - Internal server: Kestrel
  - External web server (or _reverse proxy server_): IIS (Express), Nginx or Apache

### Topics

- `IConfiguration` service:
  - `appsettings.json` [🔗](https://youtu.be/m_BevGi7zBw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 9
  - Reading order: `appsettings.json`, `appsettings.{Environment}.json`, User secrets, Environment variables, lastly, CLI arguments.
- Middlewares introduction [🔗](https://youtu.be/ALu4jtvjSYw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 10
  - Pipeline configuration [🔗](https://youtu.be/nt6anXAwfYI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 11
- Static files and default files [🔗](https://youtu.be/yt6bzZoovgM?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 12
- Development environments [🔗](https://youtu.be/x8jNX1nb_og?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 14
  - _Development_, _Staging_, _Production_
  - `ASPNETCORE_ENVIRONMENT` for selecting the run environment.
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
- Dependency Injection [🔗](https://youtu.be/BPGtVpu81ek?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 19
  - Benefits: Loose coupling, Easier unit testing
  - Lifetime service registration [🔗](https://youtu.be/v6Nr7Zman_Y?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 44
    - Notes [🔗](https://csharp-video-tutorials.blogspot.com/2019/04/addsingleton-vs-addscoped-vs.html) [🖼️](./images/service-registration.png) - Consider: Service instance within current HTTP request.
    - `builder.Services.AddSingleton()` - Creates a single instance of the service when it is first requested, and reuses that same instance in all the places where that service is needed - _3 4 5 6 7... So on and so foth._
    - `builder.Services.AddScoped()` - A new instance of a Scoped service is created once per request within the scope. For example, in a web application it creates 1 instance per each http request but uses the same instance in the other calls within that same web request. - _3 4 4 4 4 4... So on and so foth._
    - `builder.Services.AddTransient()` - A new instance of a Transient service is created each time it is requested. - _3 3 3 3 3... So on and so foth._
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
- Page Views:
  - Layout View [🔗](https://youtu.be/Px8nwoO7FO8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 28
  - ListView [🔗](https://youtu.be/nHAMDUtiV6w?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 27
  - Render Sections in Layout Page [🔗](https://youtu.be/9OyrzRVZT8o?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 29
    - For the organization of page elements. They can be optional or mandatory.
  - `_ViewStart.cshtml` [🔗](https://youtu.be/r7WgjrTSlO8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 30
  - `_ViewImports.cshtml` [🔗](https://youtu.be/5HskoMcun9A?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 31
    - Used to include common namespaces.
    - View directives: `@addTagHelper` `@removeTagHelper` `@tagHelperPrefix` `@model` `@inherits` `@inject`
- Routing:
  - Conventional Routing [🔗](https://youtu.be/ZoxrbrHjj2g?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 32
  - Attribute Routing [🔗](https://youtu.be/prNptonJAiY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 33
    - Applied to the controllers or to the controller actions methods.
    - NOTE: The _controller route template_ is not combined with _action method route template_, if the route template on the action method begins with `/` or `~/`
    - Tokens in attribute routing: `[Route("[controller]/[action]")]`
- Using `libman` [🔗](https://youtu.be/5qzzjvZ4w0c?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 34
  - Use LibMan with ASP\.NET Core in Visual Studio [🔗](https://learn.microsoft.com/en-us/aspnet/core/client-side/libman/libman-vs?view=aspnetcore-9.0#add-library-files)
- Tag Helpers:
  - Notes: Similar to _HTML Helpers_. Server side components for HTML rendering. Use: Link generation, form creation, asset load, etc.
  - Tag Helpers & HTML Helpers [🔗](https://youtu.be/iaSdTMdReWg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 35
  - Why use Tag Helpers [🔗](https://youtu.be/pXvizSVe-eQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 36
  - Image Tag Helper [🔗](https://youtu.be/4jW9T-TLPTM?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 37
    - Provide cache-busting behaviour for static image files.
  - Environment Tag Helper [🔗](https://youtu.be/-E4zP2L-R_U?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 38
  - Form Tag Helpers [🔗](https://youtu.be/mU4hV50rkVE?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 40
    - `Form Tag Helper`, `Input Tag Helper`, `Label Tag Helper`, `Select Tag Helper`, `TextArea Tag Helper`, `Validation Tag Helper`
- Model Binding [🔗](https://youtu.be/-GkZERrqEQo?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 41
  - "Model binding maps data in an HTTP request to controller action method parameters. The action parameters say be simple or complext types."
  - `name` input attribute value is used for mapping.
  - Data available in the HTTP request (with preeminence order): `Form values` → `Route values` → `Query strings`

### **SERVER SIDE VALIDATION**

- Model Validation [🔗](https://youtu.be/aDRC_IgwmH8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 42
- Custom validation attributes [🔗](https://youtu.be/o_AH2MGti0A?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 76
- Select list validation [🔗](https://youtu.be/woFHR3iNEEI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 43

### **BOOTSTRAP**

- Navigation Menu [🔗](https://youtu.be/l2dzzuxvmxk?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 39

### **ENTITY FRAMEWORK CORE**

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
- Seed Data to Database [🔗]()
- Keeping _domain models_ and _database schema_ in sync [🔗](https://youtu.be/MhvOKHUWgiY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 52
  - `Remove-Migration`
  - This video also explains how to remove migration that has already been applied to the database.
- File upoad [🔗](https://youtu.be/aoxEJii70_I?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 53
  - `IFormFile`, the video includes jQuery code for `.custom-file-label`.
  - Notes:
    - File is saved to the web server location `wwwroot/images`
    - The file uploaded to the server can be accessed through Model Binding using the IFormFile interface.
  - Upload multiple files at once [🔗](https://youtu.be/14ZqBoQIW-Q?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 54
- Update a model:
  - Edit View - GET [🔗](https://youtu.be/lhiIvx7jMaY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 55
    - `Edit.cshtml` with controller and viewmodel.
  - Edit View - POST [🔗](https://youtu.be/wamToyK4x7I?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 56
- Handling `404` Not Found Errors:
  - Type 1: Resource with ID not found [🔗](https://youtu.be/QiF3eJ4Zb0o?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 57
  - Type 2: URL does not match any route.
    - Centralized `404` Error Handling (`400`-`599`):
      - `UseStatusCodePages` [🔗](https://youtu.be/DVo138knAHQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 58
      - User facing: `UseStatusCodePagesWithRedirects` and `UseStatusCodePagesWithReExecute` [🔗](https://youtu.be/9CwgiSxrkeQ?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 59
- Correct Global Exception handling:
  - Not production: `UseExceptionHandler` [🔗](https://youtu.be/jeBttUIqpuc?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 60
- Logging:
  - Basics, Logging from console, Built-in Logging Providers [🔗](https://youtu.be/WyAJe6lA-bY?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 61
  - Logging Exceptions [🔗](https://youtu.be/LhpO2sgxqfw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 62
  - Logging to a file [🔗](https://youtu.be/o5u4fE0t79k?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 63

### **ASP.NET Core Identity** - Authentication & Authorization

- Google search [🔎](https://www.google.com/search?q=asp+net+core+identity+documentation)
- Official documentation [📑](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio)

Content:

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
- Roles:
  - Create Roles [🔗](https://youtu.be/TuJd2Ez9i3I?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 78
  - Get list of roles [🔗](https://youtu.be/KGIT8P29jf4?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 79
  - Edit existing role [🔗](https://youtu.be/7ikyZk5fGzk?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 80
  - Add or remove users [🔗](https://youtu.be/TzhqymQm5kw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 81
    - Tables for: Users, Roles, UserRoles
  - _Role based authorization_ [🔗](https://youtu.be/DXVe6skc42k?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 82
    - Show or hide navigation menu based on user [🔗](https://youtu.be/IPjK65ehQBg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 83
- Users:
  - List users, register new user and redirect correctly [🔗](https://youtu.be/OMX0UiLpMSA?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 84
  - Edit user information, roles and claims [🔗](https://youtu.be/QYlIfH8qyrU?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 85
  - Delete user [🔗](https://youtu.be/MhNfyZGfY-A?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 86
    - Display delete confirmation [🔗](https://youtu.be/hKLjt9GzYM8?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 87
  - Delete role [🔗](https://youtu.be/pj3GCelrIGM?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 88
  - [🔗]() - Part
  - [🔗]() - Part

### **CLIENT SIDE VALIDATION**

- Client side validation implementation [🔗](https://youtu.be/PUX3PzyBofg?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 74
  - Requirements (in this order): `jquery.js`, `jquery.validate.js`, `jquery.validate.unobtrusive.js`
- Remote validation [🔗](https://youtu.be/2jZc11l67Zk?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 75

### MSSQL SERVER

- Enforce `ON DELETE NO ACTION` [🔗](https://youtu.be/txTZAFut9mA?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU) - Part 89
  - _Cascading referential integrity constraint_
- [🔗]() - Part
- [🔗]() - Part

The SSL connection could not be established, see inner exception.

---

# C# Programming Language

## C# Tutorial For Beginners

[Full tutorial 🔗](https://www.youtube.com/playlist?list=PLAC325451207E3105)

- Some topics related to the main tutorial of this file:
  - Delegates [🔗](https://youtu.be/D2h46fvQX04?list=PLAC325451207E3105)
  - Lambda Expressions [🔗](https://youtu.be/LDgQ-spnrYY?list=PLAC325451207E3105)
  - `Task`, `async`, `await` [🔗](https://youtu.be/C5VhaxQWcpE?list=PLAC325451207E3105)
    - `Thread`, `Action` [🔗](https://youtu.be/SgHYVPKJRX8?list=PLAC325451207E3105)

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

## Delete Rule

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
