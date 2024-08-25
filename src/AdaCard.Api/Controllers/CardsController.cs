using AdaCard.Api.Contracts;
using AdaCard.Api.Helpers;
using AdaCard.Core.Features.Cards.Commands;
using AdaCard.Core.Features.Cards.DTOs;
using AdaCard.Core.Features.Cards.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace AdaCard.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CardsController : ControllerBase
{
    private readonly IMediator mediator;

    public CardsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet,
        ProducesResponseType(typeof(IEnumerable<Card>), StatusCodes.Status200OK),
        ProducesResponseType(StatusCodes.Status401Unauthorized),
        ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var query = new GetCardsQuery();

        var result = await this.mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost,
        ProducesResponseType(typeof(Card), StatusCodes.Status200OK),
        ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest),
        ProducesResponseType(StatusCodes.Status401Unauthorized),
        ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostAsync([FromBody] CreateCard createCardRequest, CancellationToken cancellationToken)
    {
        var command = new CreateCardCommand(createCardRequest);

        var result = await this.mediator.Send(command, cancellationToken);

        return Ok(result.Value);
    }

    [HttpPut("{id}"),
        ProducesResponseType(typeof(Card), StatusCodes.Status200OK),
        ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest),
        ProducesResponseType(StatusCodes.Status404NotFound),
        ProducesResponseType(StatusCodes.Status401Unauthorized),
        ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] CreateCard updatedCardRequest, CancellationToken cancellationToken)
    {
        var command = new UpdateCardCommand(id, updatedCardRequest);

        var result = await this.mediator.Send(command, cancellationToken);

        if (result.IsFailed)
        {
            return NotFound(new Error { Message = "Card not found", StatusCode = 400 });
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id}"),
        ProducesResponseType(typeof(IEnumerable<Card>), StatusCodes.Status200OK),
        ProducesResponseType(StatusCodes.Status404NotFound),
        ProducesResponseType(StatusCodes.Status401Unauthorized),
        ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteCardByIdCommand(id);

        var result = await this.mediator.Send(command, cancellationToken);

        if (result.IsFailed)
        {
            return NotFound(new Error { Message = "Card not found", StatusCode = 400 });
        }

        return Ok(result.Value);
    }
}
