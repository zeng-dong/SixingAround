using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

const string SourceName = "Give_it_a_unique_source_name_aka_tracer_name_";
ActivitySource source = new ActivitySource(SourceName, "1.0.0");

Console.WriteLine("Hello, World!");

using var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MySample"))
                .AddSource(SourceName)
                .AddConsoleExporter()
                .Build();

await DoSomeWork("banana", 8);
Console.WriteLine("Example work done");

async Task DoSomeWork(string foo, int bar)
{
    using (Activity activity = source?.StartActivity("DoSomeWork")!)
    {
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