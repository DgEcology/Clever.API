using Microsoft.AspNetCore.Mvc;

namespace Clever.Web.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("api/error")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Error()
    {
        return Problem();
    }
}