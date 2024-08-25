using AdaCard.Core.Entities;

using MediatR;
using AdaCard.Core.Interfaces;
using FluentValidation;
using AdaCard.Core.Features.Cards.DTOs.Mappers;
using FluentValidation.Results;
using FluentResults;

namespace AdaCard.Core.Features.Cards.Commands.Handlers;

public class UpdateCardCommandHandlers : IRequestHandler<UpdateCardCommand, Result<DTOs.Card>>
{
    private readonly IRepository<Card> cardRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateCardCommandHandlers(
        IRepository<Card> cardRepository,
        IUnitOfWork unitOfWork)
    {
        this.cardRepository = cardRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<DTOs.Card>> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
    {
        var existentCard = await this.cardRepository.FindByIdAsync(request.Id, cancellationToken);

        if (existentCard == null)
        {
            throw new ValidationException(new List<ValidationFailure> { new ("Card", "Card not found") });
        }

        existentCard.UpdateCard(request.Titulo, request.Conteudo, request.Lista);

        await this.unitOfWork.SaveAsync(cancellationToken);

        return Result.Ok(CardMapper.Map(existentCard));
    }
}
