using Azure.Core;
using FunctionalAspire.ApiService.Api;
using ServiceLevelIndicators;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddServiceLevelIndicator(options =>
{
  options.CustomerResourceId = "FunctionalAspire";
  options.LocationId = ServiceLevelIndicator.CreateLocationId("public", AzureLocation.WestUS3.Name);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseWeatherForecaseRoute();
app.UseUserRoute();
app.MapDefaultEndpoints();
app.UseServiceLevelIndicator();
app.Run();

