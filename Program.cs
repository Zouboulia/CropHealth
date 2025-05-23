using Microsoft.EntityFrameworkCore;
using SmartCrop.Components;
using SmartCrop.Data;
using SmartCrop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContext<SmartCropDbContext>(options =>
    options.UseSqlite("Data Source= smartcrop.db"));

builder.Services.AddScoped<MonitoringService>();
builder.Services.AddControllers(); //enable the Web API support

builder.Services.AddHttpClient();

builder.Services.AddScoped<WeatherService>(); //Weather service


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers(); // Map the Web API controllers

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SmartCrop.Client._Imports).Assembly);

app.Run();