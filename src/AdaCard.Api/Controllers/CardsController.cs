using AdaCard.Api.Helpers;

using Microsoft.AspNetCore.Mvc;

namespace AdaCard.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CardsController : ControllerBase
{
    [Authorize]
    [HttpGet,
        ProducesResponseType(StatusCodes.Status200OK),
        ProducesResponseType(StatusCodes.Status401Unauthorized),
        ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync()
    {
        return Ok();
    }
}
