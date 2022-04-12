using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Instrumentation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddOpenTelemetryTracing((builder) => builder
        // Configure the resource attribute `service.name` to MyServiceName
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MyServiceName"))
        // Add tracing of the AspNetCore instrumentation library
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter()
    );

//builder.Services
//    .AddOpenTelemetryMetrics(builder => builder
//        // Configure the resource attribute `service.name` to MyServiceName
//        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MyServiceName"))
//        // Add metrics from the AspNetCore instrumentation library
//        .AddAspNetCoreInstrumentation()
//        .AddConsoleExporter(options =>
//        {
//            options.MetricReaderType = MetricReaderType.Periodic;
//            options.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = 5000;
//        }));
//
//builder.Host
//    .ConfigureLogging(logging => logging
//        .ClearProviders()
//        .AddOpenTelemetry(options =>
//        {
//            // Export the body of the message
//            options.IncludeFormattedMessage = true;
//            // Configure the resource attribute `service.name` to MyServiceName
//            options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MyServiceName"));
//            options.AddConsoleExporter(options =>
//            {
//                options.MetricReaderType = MetricReaderType.Periodic;
//                options.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = 5000;
//            });
//        }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/*
using System.Diagnostics;
using System.Text;

ActivitySource source = new ActivitySource("Sample.ApiOrder", "1.0.0");

ActivitySource.AddActivityListener(new ActivityListener()
{
    ShouldListenTo = (source) => true,
    Sample = (ref ActivityCreationOptions<ActivityContext> options) => ActivitySamplingResult.AllDataAndRecorded,
    ActivityStarted = activity =>
    {
        WriteActivity(desc: "Started", activity: activity);
    },
    ActivityStopped = activity =>
    {
        WriteActivity("Stopped", activity);
    }
});

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

void WriteActivity(string desc, Activity activity)
{
    StringBuilder sb = new StringBuilder();

    sb.AppendLine(desc + ": " + activity.OperationName);
    sb.AppendLine("Activity Kind: " + activity.Kind.ToString());
    sb.AppendLine("Activity Id: " + activity.Id);
    sb.AppendLine("Parent Id: " + activity.ParentId);
    sb.AppendLine("Duration: " + activity.Duration);

    if (activity.Tags != null)
    {
        sb.AppendLine("\tWriting Tags:");
        foreach (var tag in activity.Tags)
        {
            sb.AppendLine("\t\tKey: " + tag.Key.ToString() + " Value: " + tag.Value.ToString());
        }
    }
    if (activity.Events != null)
    {
        sb.AppendLine("\tWriting Events:");
        foreach (ActivityEvent ev in activity.Events)
        {
            sb.AppendLine("\t\tName: " + ev.Name + " Timestamp: " + ev.Timestamp);
        }
    }
    if (activity.Baggage != null)
    {
        sb.AppendLine("\tWriting Baggage:");
        foreach (var baggage in activity.Baggage)
        {
            sb.AppendLine("\t\tKey: " + baggage.Key.ToString() + " Value: " + baggage.Value.ToString());
        }
    }
    sb.AppendLine();

    LogInfo(sb.ToString());
}

void LogInfo(string info)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation(info);
    }
}

*/