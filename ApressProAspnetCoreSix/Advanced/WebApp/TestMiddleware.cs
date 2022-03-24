using Advanced.Models;

namespace Advanced.WebApp;

public class TestMiddleware
{
    private RequestDelegate nextDelegate;

    public TestMiddleware(RequestDelegate next)
    {
        nextDelegate = next;
    }

    public async Task Invoke(HttpContext context, DataContext dataContext)
    {
        if (context.Request.Path == "/test")
        {
            await context.Response.WriteAsync(
            $"There are {dataContext.People.Count()} people\n");
            await context.Response.WriteAsync(
            $"There are {dataContext.Locations.Count()} locations\n");
            await context.Response.WriteAsync(
            $"There are {dataContext.Departments.Count()} departments\n");
        }
        else
        {
            await nextDelegate(context);
        }
    }
}