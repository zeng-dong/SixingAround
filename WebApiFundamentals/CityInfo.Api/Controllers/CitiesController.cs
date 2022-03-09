using CityInfo.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<CityDto>> GetCities()
    {
        return Ok(CitiesDataStore.Current);
    }

    /// <summary>
    /// Get a city by id
    /// </summary>
    /// <param name="id">The id of the city to get</param>
    /// <param name="includePointsOfInterest">Whether or not to include the points of interest</param>
    /// <returns>An IActionResult</returns>
    /// <response code="200">Returns the requested city</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CityDto> GetCity(int id)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

        if (city == null) return NotFound();

        return Ok(city);
    }
}