using AdaCard.Core.Features.Cards.DTOs;

using FluentResults;

using MediatR;

namespace AdaCard.Core.Features.Cards.Commands;

public class DeleteCardByIdCommand : IRequest<Result<IEnumerable<Card>>>
{
    public DeleteCardByIdCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
