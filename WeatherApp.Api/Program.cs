using Microsoft.AspNetCore.Cors;

using AspNetCore.Proxy;

using WeatherApp.Api.Settings;
using WeatherApp.Api.Mapping;
using WeatherApp.Api.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

//utilities
builder.Services.AddScoped<ForecastConditionDtoCreator>();
builder.Services.AddScoped<ForecastContentModifier>();

// automapper
builder.Services.AddAutoMapper(typeof(WeatherProfile));

// proxy
builder.Services.AddProxies();

// controllers
builder.Services.AddControllers();

// cors
builder.Services.AddCors(options => options.AddPolicy("WeatherApp",
    builder =>
    {
        builder.WithOrigins("https://localhost:7114")
        .AllowAnyMethod()
        .AllowAnyHeader();
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors("WeatherApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
