using System.Text.Json;

namespace TextDotJson;

internal static class Program
{
    static void Main(string[] args)
    {
        var weatherForecast = new WeatherForecast
        {
            Date = DateTime.Parse("2021-12-01"),
            TemperatureCelsius = 25,
            Summary = "Hot"
        };

        string jsonString = JsonSerializer.Serialize(weatherForecast);

        Console.WriteLine(jsonString);
    }
}