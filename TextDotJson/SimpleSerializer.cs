using System.Text.Json;

namespace TextDotJson;

internal static class SimpleSerializer
{
    internal static void SimpleSerialize()
    {
        var weatherForecast = new WeatherForecast
        {
            Date = DateTime.Parse("2021-12-01"),
            TemperatureCelsius = 25,
            Summary = "Hot"
        };

        string jsonString = JsonSerializer.Serialize(weatherForecast);
        Console.WriteLine(jsonString);
        Console.Clear();

        // generic
        string jsonStringGenerics = JsonSerializer.Serialize<WeatherForecast>(weatherForecast);
        Console.WriteLine(jsonStringGenerics);
        Console.Clear();

        //3. Serialization to a file
        string fileName = "WeatherForecast.json";
        File.WriteAllText(fileName, jsonString);
    }
}