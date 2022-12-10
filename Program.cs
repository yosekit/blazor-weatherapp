using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Http;
using WeatherApp;
using WeatherApp.Options;
using WeatherApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// api options
var configApi = builder.Configuration.GetSection("API");
builder.Services.AddSingleton(configApi.GetSection(nameof(WeatherOptions)).Get<WeatherOptions>());
builder.Services.AddSingleton(configApi.GetSection(nameof(CountriesOptions)).Get<CountriesOptions>());
// ...

// http clients
builder.Services.AddHttpClient<WeatherService>((sp, client) =>
{
    client.BaseAddress = new Uri(sp.GetRequiredService<WeatherOptions>().BaseUrl);
});
builder.Services.AddHttpClient<CountriesService>((sp, client) =>
{
    client.BaseAddress = new Uri(sp.GetRequiredService<CountriesOptions>().BaseUrl);
});

await builder.Build().RunAsync() ;
