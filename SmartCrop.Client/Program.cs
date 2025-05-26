using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5204/") });


// Load configuration from appsettings.json instead of hardcoding the base URL as per linting recommendations
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Read the BaseUrl from the configuration
var baseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl");

// Configure HttpClient with the base URL from configuration
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });


await builder.Build().RunAsync();