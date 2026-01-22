using Microsoft.AspNetCore.Mvc;
using Records.MessageRecords;

[Route("/weatherforecast")]
[ApiController]
public class WeatherController : ControllerBase
{
    readonly string[] summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
   
   [HttpGet(Name = "WeatherForecast")]
   public ActionResult<WeatherForecast> GetLogs() {
        var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
        return Ok(forecast);
   }
}
