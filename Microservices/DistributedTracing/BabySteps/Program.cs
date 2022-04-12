using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace BabySteps;

internal class Program
{
    static readonly ActivitySource source = new ActivitySource("Give_it_a_unique_source_name_aka_tracer_name", "1.0.0");

    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        using var tracerProvider = Sdk.CreateTracerProviderBuilder()
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MySample"))
                        .AddSource("Give_it_a_unique_source_name_aka_tracer_name")
                        .AddConsoleExporter()
                        .Build();

        await DoSomeWork("banana", 8);
        Console.WriteLine("Example work done");
    }

    static async Task DoSomeWork(string foo, int bar)
    {
        using (Activity activity = source?.StartActivity("DoSomeWork")!)
        {
            if (activity == null) Console.WriteLine("activity is null");

            await StepOne();
            await StepTwo();
        }
    }

    static async Task StepOne()
    {
        await Task.Delay(500);
    }

    static async Task StepTwo()
    {
        await Task.Delay(1000);
    }
}