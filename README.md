
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
  - `WebApplication.CreateBuilder(args);`
        - One web server: Kestrel or IIS Express
    - IIS worker process (*w3wp.exe* or *iisexpress.exe*)
    - No proxy request penalties
- OutOfProcess Hosting (*default*) [🔗](https://youtu.be/QsXsOX6qq2c?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - `dotnet.exe` process
  - Internal server: Kestrel
  - External web server (or *reverse proxy server*): IIS (Express), Nginx or Apache

#### Other

- `IConfiguration` service:
  - `appsettings.json` [🔗](https://youtu.be/m_BevGi7zBw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - Reading order: `appsettings.json`, `appsettings.{Environment}.json`, User secrets, Environment variables, lastly, CLI arguments.
- Middlewares introduction [🔗](https://youtu.be/ALu4jtvjSYw?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - Pipeline configuration [🔗](https://youtu.be/nt6anXAwfYI?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
- Static files and default files [🔗](https://youtu.be/yt6bzZoovgM?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
- Development environments [🔗](https://youtu.be/x8jNX1nb_og?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - *Development*, *Staging*, *Production*
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
- Dependency Injection [🔗](https://youtu.be/BPGtVpu81ek?list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU)
  - Benefits: Loose coupling, Easier unit testing
  - Lifetime determination:
    - `builder.Services.AddSingleton()` - Instance creation once, same instance, all requests, during whole app life.
    - `builder.Services.AddTransient()` - New instance every time is requested.
    - `builder.Services.AddScoped()` - New instance once per request withing the same scope.
-  [🔗]()

### C#

- Extension methods
- Delegates
- Lambda Expressions
