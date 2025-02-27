using Chapter8EFFactoryMethod.Components;
using Chapter8EFFactoryMethod.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// Example SQLite connection string
string connectionString = "Data Source=mydatabase.db;";
string databaseName = "TestDatabase";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// You might want to conditionally set this up based on app settings or environment variables
bool useInMemory = true;
if (useInMemory)
{
    builder.Services.AddScoped<IDbContextFactory>(provider =>
    new InMemoryDbContextFactory(databaseName));
}
else
{
    builder.Services.AddScoped<IDbContextFactory>(provider =>
                new SqliteDbContextFactory(connectionString));
}
builder.Services.AddScoped<DataRepository>();


var app = builder.Build();

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
    .AddAdditionalAssemblies(typeof(Chapter8EFFactoryMethod.Client._Imports).Assembly);

// Ensure database creation
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DataRepository>();
    dbContext.InitializeDatabase();
}
app.Run();
