var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

#region justSomeText

var text = "Hello World!";
var processName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
var mySettings = new MySettings();
builder.Configuration.GetRequiredSection(nameof(MySettings)).Bind(mySettings);
var environment = app.Environment.EnvironmentName;

var name = mySettings.Name; // Bob
var counter = mySettings.Counter; // 100

var justSomeText = $"{text} {processName} {name} {counter} {environment}";

#endregion

// Enable the Developer Exception Page
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseFileServer(); // replaces 1, 2, 3?
/*
app.UseDefaultFiles(); // 1
app.UseStaticFiles(); // 2
app.UseDirectoryBrowser(); // 3?
 */

// app.MapGet("/", () => { throw new Exception("hola"); });

app.MapGet("/", () => justSomeText);

app.Run();
