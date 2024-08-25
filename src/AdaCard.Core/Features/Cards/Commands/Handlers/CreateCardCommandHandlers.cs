using AdaCard.Core.Entities;

using MediatR;
using AdaCard.Core.Interfaces;
using AdaCard.Core.Features.Cards.DTOs.Mappers;
using FluentResults;

namespace AdaCard.Core.Features.Cards.Commands.Handlers;

public class CreateCardCommandHandlers : IRequestHandler<CreateCardCommand, Result<DTOs.Card>>
{
    private readonly IRepository<Card> cardRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateCardCommandHandlers(
        IRepository<Card> cardRepository,
        IUnitOfWork unitOfWork)
    {
        this.cardRepository = cardRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<DTOs.Card>> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var cardRequested = new Card(request.Titulo, request.Conteudo, request.Lista);

        var card = await this.cardRepository.AddAsync(cardRequested, cancellationToken);

        await this.unitOfWork.SaveAsync(cancellationToken);

        return Result.Ok(CardMapper.Map(card));
    }
}
