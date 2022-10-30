using ElectronNET.API;
using ElectronNET.API.Entities;
using jira2ets.Clients;
using jira2ets.Services;
using MatBlazor;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseElectron(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddElectron();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddMatBlazor();
builder.Services.AddProtectedBrowserStorage();
builder.Services.AddSingleton<State>();
builder.Services.AddSingleton<EtsClient>();
builder.Services.AddSingleton<JiraClient>();
builder.Services.AddSingleton<EtsService>();
builder.Services.AddSingleton<JiraService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

var options = new BrowserWindowOptions()
{
    ZoomToPageWidth = true,
    UseContentSize = true,
    AutoHideMenuBar = false,
    Show = false,
    WebPreferences = new WebPreferences
    {
        DevTools = true
    }
};

Task.Run(async () => {
      await Electron.WindowManager.CreateWindowAsync(options)
        .ContinueWith(async task =>
        {
            var window = await task;
            var version = await Electron.App.GetVersionAsync();



            window.SetTitle("Jira to Ets: " + version);
            window.Show();
        });
});

app.Run();
