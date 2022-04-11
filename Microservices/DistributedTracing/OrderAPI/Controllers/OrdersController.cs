using Microsoft.AspNetCore.Mvc;

namespace OrderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok("Hello Order");
    }
}