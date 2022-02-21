using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
    [HttpGet]
    public JsonResult GetCities()
    {
        return new JsonResult(
            new List<object>
            {
                new { id = 1, name = "New York City" },
                new { id = 2, name = "Houston "}
            });
    }
}