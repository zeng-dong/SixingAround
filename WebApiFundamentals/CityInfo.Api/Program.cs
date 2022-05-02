using Microsoft.AspNetCore.StaticFiles;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
})
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //this is comment
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseMetricServer();
app.UseHttpMetrics();

app.UseAuthorization();

//we use UseEndpoints instead of app.MapControllers

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();