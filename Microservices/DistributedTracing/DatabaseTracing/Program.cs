using Dapper;
using System.Data.SqlClient;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using LightStep;
using OpenTracing.Util;

var builder = WebApplication.CreateBuilder(args);

var lsToken = builder.Configuration.GetValue<string>("LightstepToken");

builder.Services.AddScoped(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("EmployeeDbConnectionString")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure tracing
builder.Services.AddOpenTelemetryTracing(builder => builder
    // Customize the traces gathered by the HTTP request handler
    .AddAspNetCoreInstrumentation(options =>
    {
        // Only capture the spans generated from the ems/* endpoints
        options.Filter = context => context.Request.Path.Value?.Contains("ems") ?? false;
        options.RecordException = true;
        // Add metadata for the request such as the HTTP method and response length
        options.Enrich = (activity, eventName, rawObject) =>
        {
            switch (eventName)
            {
                case "OnStartActivity":
                    {
                        if (rawObject is not HttpRequest httpRequest)
                        {
                            return;
                        }

                        activity.SetTag("requestProtocol", httpRequest.Protocol);
                        activity.SetTag("requestMethod", httpRequest.Method);
                        break;
                    }
                case "OnStopActivity":
                    {
                        if (rawObject is HttpResponse httpResponse)
                        {
                            activity.SetTag("responseLength", httpResponse.ContentLength);
                        }

                        break;
                    }
            }
        };
    })
    // Customize the telemetry generated by the SqlClient
    .AddSqlClientInstrumentation(options =>
    {
        options.EnableConnectionLevelAttributes = true;
        options.SetDbStatementForStoredProcedure = true;
        options.SetDbStatementForText = true;
        options.RecordException = true;
        options.Enrich = (activity, x, y) => activity.SetTag("db.type", "sql");
    })
    .AddSource("zd.ems.ems-api")
    // Create resources (key-value pairs) that describe your service such as service name and version
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ems-api")
        .AddAttributes(new[] { new KeyValuePair<string, object>("service.version", "1.0.0.0") }))
    // Ensures that all activities are recorded and sent to exporter
    .SetSampler(new AlwaysOnSampler())
    // Exports spans to Lightstep
    ////.AddOtlpExporter(otlpOptions =>
    ////{
    ////    otlpOptions.Endpoint = new Uri("https://ingest.lightstep.com:443/traces/otlp/v0.9");
    ////    otlpOptions.Headers = $"lightstep-access-token={lsToken}";
    ////    otlpOptions.Protocol = OtlpExportProtocol.HttpProtobuf;
    ////})
    .AddOtlpExporter(opts =>
    {
        opts.Endpoint = new Uri("http://localhost:4317");
    })
);

////var satelliteOptions = new SatelliteOptions("ingest.lightstep.com");
////var overrideTags = new Dictionary<string, object>
//// {
////   { LightStepConstants.ComponentNameKey, "YOUR_SERVICE_NAME" },
////   {"my_tag", "foobar"}
//// };
////
////var tracerOptions = new Options("YOUR_ACCESS_TOKEN").WithSatellite(satelliteOptions).WithTags(overrideTags);
////var lightStepTracer = new LightStep.Tracer(tracerOptions);
////
////GlobalTracer.Register(lightStepTracer);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var activitySource = new ActivitySource("zd.ems.ems-api");

app.MapPost("/ems/billing", async (Timekeeping timekeepingRecord, SqlConnection db) =>
{
    using var activity = activitySource.StartActivity("Record project work", ActivityKind.Server);
    activity?.AddEvent(new ActivityEvent("Project billed"));
    activity?.SetTag(nameof(Timekeeping.EmployeeId), timekeepingRecord.EmployeeId);
    activity?.SetTag(nameof(Timekeeping.ProjectId), timekeepingRecord.ProjectId);
    activity?.SetTag(nameof(Timekeeping.WeekClosingDate), timekeepingRecord.WeekClosingDate);

    ////var span = lightStepTracer.BuildSpan("testSpan").Start();
    ////span.Log("This is a log message!");
    ////// for tag value, use either "client" or "server" depending on whether
    ////// this service receives or creates requests
    ////span.SetTag("span.kind", "client");
    ////span.Finish();
    ////// manually flush the tracer to make sure your span is sent.
    ////lightStepTracer.Flush();

    await db.ExecuteAsync(
        "INSERT INTO Timekeeping Values(@EmployeeId, @ProjectId, @WeekClosingDate, @HoursWorked)",
        timekeepingRecord);
    return Results.Created($"/ems/billing/{timekeepingRecord.EmployeeId}", timekeepingRecord);
})
    .WithName("RecordProjectWork")
    .Produces(StatusCodes.Status201Created);

app.MapGet("/ems/billing/{empId}/", async (int empId, SqlConnection db) =>
{
    var result = await db.QueryAsync<Timekeeping>($"SELECT * FROM Timekeeping WHERE EmployeeId={empId}");
    return result.Any() ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetBillingDetails")
    .Produces<IEnumerable<Timekeeping>>()
    .Produces(StatusCodes.Status404NotFound);

app.MapPost("/ems/payroll/add/", async (Payroll payrollRecord, SqlConnection db) =>
{
    await db.ExecuteAsync(
        "INSERT INTO Payroll Values(@EmployeeId, @PayRateInUSD)", payrollRecord);
    return Results.Created($"/ems/payroll/{payrollRecord.EmployeeId}", payrollRecord);
})
    .WithName("AddEmployeeToPayroll")
    .Produces(StatusCodes.Status201Created);

app.MapGet("/ems/payroll/{empId}", async (int empId, SqlConnection db) =>
{
    var result = await db.QueryAsync<Payroll>($"SELECT * FROM Payroll WHERE EmployeeId={empId}");
    return result.Any() ? Results.Ok(result) : Results.NotFound();
})
    .WithName("GetEmployeePayroll")
    .Produces<IEnumerable<Payroll>>()
    .Produces(StatusCodes.Status404NotFound);

app.Run();

public class Timekeeping
{
    public int EmployeeId { get; set; }
    public int ProjectId { get; set; }
    public DateTime WeekClosingDate { get; set; }
    public int HoursWorked { get; set; }
}

public class Payroll
{
    public int EmployeeId { get; set; }
    public decimal PayRateInUSD { get; set; }
}