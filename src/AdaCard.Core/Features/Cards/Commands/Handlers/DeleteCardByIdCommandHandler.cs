using AdaCard.Core.Entities;
using AdaCard.Core.Features.Cards.DTOs.Mappers;
using AdaCard.Core.Interfaces;

using FluentResults;

using MediatR;

namespace AdaCard.Core.Features.Cards.Commands.Handlers;

public class DeleteCardByIdCommandHandler : IRequestHandler<DeleteCardByIdCommand, Result<IEnumerable<DTOs.Card>>>
{
    private readonly IRepository<Card> cardRepository;
    private readonly IUnitOfWork unitOfWork;

    public DeleteCardByIdCommandHandler(
        IRepository<Card> cardRepository,
        IUnitOfWork unitOfWork)
    {
        this.cardRepository = cardRepository;
        this.unitOfWork = unitOfWork;
    } 
    public async Task<Result<IEnumerable<DTOs.Card>>> Handle(DeleteCardByIdCommand request, CancellationToken cancellationToken)
    {
        var card = await this.cardRepository.FindByIdAsync(request.Id, cancellationToken);

        if (card == null)
        {
            return Result.Fail("Card not found");
        }

        this.cardRepository.DeleteAsync(card);

        await this.unitOfWork.SaveAsync(cancellationToken);

        var cards = await this.cardRepository.GetAllAsync(cancellationToken);

        return Result.Ok(cards.Select(x => CardMapper.Map(x)));
    }
}
