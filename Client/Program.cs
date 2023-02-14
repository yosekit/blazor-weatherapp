using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using WeatherApp.Client;
using WeatherApp.Client.Application.Options;
using WeatherApp.Client.Application.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiSection = builder.Configuration.GetSection("Api");

// options 
builder.Services.AddSingleton(
    apiSection.GetSection(WeatherOptions.JsonName).Get<WeatherOptions>()!);

builder.Services.AddSingleton(
    apiSection.GetSection(CountriesOptions.JsonName).Get<CountriesOptions>()!);

// http clients
builder.Services.AddHttpClient<WeatherService>((sp, client) =>
{
    client.BaseAddress = new Uri(apiSection.GetValue<string>("BaseUrl")!);
});
builder.Services.AddHttpClient<CountriesService>((sp, client) =>
{
    client.BaseAddress = new Uri(apiSection.GetValue<string>("BaseUrl")!);
});

await builder.Build().RunAsync();
