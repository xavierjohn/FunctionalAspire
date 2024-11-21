namespace FunctionalAspire.ApiService.Api
{
  using ServiceLevelIndicators;

  public static class WeatherForecaseExt
  {
    public static void UseWeatherForecaseRoute(this WebApplication app)
    {
      string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

      app.MapGet("/weatherforecast", () =>
      {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return forecast;
      })
      .WithName("GetWeatherForecast")
      .AddServiceLevelIndicator();
    }

  }

  record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
  {
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
  }

}
