using Dapper;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped(_ =>
    new SqlConnection(builder.Configuration.GetConnectionString("EmployeeDbConnectionString")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/ems/billing", async (Timekeeping timekeepingRecord, SqlConnection db) =>
{
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