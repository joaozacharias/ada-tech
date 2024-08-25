using AdaCard.Api.Contracts;
using AdaCard.Api.Contracts.Mappers;
using AdaCard.Core.Features.Login.DTOs;
using AdaCard.Core.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdaCard.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IAuthService authService;

    public LoginController(IAuthService authService)
    {
        this.authService = authService;
    }

    [AllowAnonymous]
    [HttpPost,
     ProducesResponseType(typeof(string), StatusCodes.Status200OK),
     ProducesResponseType(StatusCodes.Status400BadRequest),
     ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest auth, CancellationToken cancellationToken)
    {
        var authResponse = await this.authService.AuthenticationAsync(auth.Login, auth.Senha, cancellationToken);

        if (authResponse.IsFailed)
            return BadRequest(authResponse.Errors.MapToErrror());

        return Ok(authResponse.Value);
    }
}
