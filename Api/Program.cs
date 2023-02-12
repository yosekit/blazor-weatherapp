using AspNetCore.Proxy;

using WeatherApp.Api.Settings;
using WeatherApp.Api.Mapping;
using WeatherApp.Api.Services;
using WeatherApp.Api.Services.ResponseModifiers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(); //
builder.Services.AddRazorPages(); //

// settings
builder.Services.AddSingleton(
    builder.Configuration.GetSection(WeatherSettings.JsonName).Get<WeatherSettings>());

builder.Services.AddSingleton(
    builder.Configuration.GetSection(CountriesSettings.JsonName).Get<CountriesSettings>());

// http clients
builder.Services.AddHttpClient("Weather", (sp, client) =>
{
    client.BaseAddress = new Uri(sp.GetRequiredService<WeatherSettings>().BaseUrl!);
});
builder.Services.AddHttpClient("Countries", (sp, client) =>
{
    client.BaseAddress = new Uri(sp.GetRequiredService<CountriesSettings>().BaseUrl!);
});

// services
builder.Services.AddScoped<ForecastConditionDtoCreator>();

builder.Services.AddScoped<WeatherResponseModifier>();
builder.Services.AddScoped<CountriesResponseModifier>();

// automapper
builder.Services.AddAutoMapper(typeof(WeatherProfile));

// proxy
builder.Services.AddProxies();

// controllers
builder.Services.AddControllers();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles(); //
app.UseStaticFiles();

app.UseRouting(); //

app.MapRazorPages(); //
app.MapControllers();
app.MapFallbackToFile("index.html"); //

app.Run();
