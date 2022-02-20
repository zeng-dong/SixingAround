var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#pragma warning disable S1135 // Track uses of "TODO" tags
// TODO: abc

if (app.Environment.IsDevelopment())
#pragma warning restore S1135 // Track uses of "TODO" tags
{
    //this is comment
    app.UseSwagger();

#pragma warning disable S125 // Sections of code should not be commented out
    //app.UseSwaggerUI();
}
#pragma warning restore S125 // Sections of code should not be commented out

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();