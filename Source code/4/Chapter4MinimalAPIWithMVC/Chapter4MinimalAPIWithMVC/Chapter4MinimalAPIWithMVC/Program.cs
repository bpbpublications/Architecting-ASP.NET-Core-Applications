using Chapter4MinimalAPIWithMVC.Client.Services;
using Chapter4MinimalAPIWithMVC.Components;
using Chapter4MinimalAPIWithMVC.ExtensionMethods;
using Chapter4MinimalAPIWithMVC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskListService, TaskListService>();

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration["baseAddress"]
            ?? "http://localhost:5052")
    });

builder.Services.AddHttpClient();

var app = builder.Build();

// Use Minimal API structured as extension method
app.UseTaskListEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapStaticAssets();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Chapter4MinimalAPIWithMVC.Client._Imports).Assembly);

app.Run();
