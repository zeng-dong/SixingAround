using System.Text.Json;

namespace TextDotJson;

internal static class AsyncSerializer
{
    internal static async Task AsyncSerialize()
    {
        string fileName = "WeatherForecast.json";

        using FileStream openStream = File.OpenRead(fileName);

        WeatherForecast? weatherForecast = await JsonSerializer.DeserializeAsync<WeatherForecast>(openStream);

        if (weatherForecast != null)
        {
            Console.WriteLine($"Date: {weatherForecast.Date}");
            Console.WriteLine($"TemperatureCelsius: {weatherForecast.TemperatureCelsius}");
            Console.WriteLine($"Summary: {weatherForecast.Summary}");
        }
    }
}