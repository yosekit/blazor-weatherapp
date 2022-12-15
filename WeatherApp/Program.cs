using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using WeatherApp;
using WeatherApp.Options;
using WeatherApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

string? apiBaseUrl = builder.Configuration["ApiBaseUrl"];

// http clients
builder.Services.AddHttpClient<WeatherService>((sp, client) =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
});
builder.Services.AddHttpClient<CountriesService>((sp, client) =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
});

await builder.Build().RunAsync();
