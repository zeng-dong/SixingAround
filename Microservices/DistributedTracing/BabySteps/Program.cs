using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

Console.WriteLine("Hello, World!");

using var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MySample"))
                .AddSource("Sample.DistributedTracing")
                .AddConsoleExporter()
                .Build();

await DoSomeWork("banana", 8);
Console.WriteLine("Example work done");

static async Task DoSomeWork(string foo, int bar)
{
    await StepOne();
    await StepTwo();
}

static async Task StepOne()
{
    await Task.Delay(500);
}

static async Task StepTwo()
{
    await Task.Delay(1000);
}