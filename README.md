
- [Kudvenkat](#kudvenkat)

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
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()
- [🔗]()

### C#
- Extension methods
- Delegates
- Lambda Expressions