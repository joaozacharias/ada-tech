using AdaCard.Core.Entities;
using AdaCard.Core.Features.Cards.DTOs.Mappers;
using AdaCard.Core.Interfaces;

using MediatR;

namespace AdaCard.Core.Features.Cards.Queries.Handlers;

public class GetCardsQueryHandler : IRequestHandler<GetCardsQuery, IEnumerable<DTOs.Card>>
{
    private readonly IRepository<Card> cardRepository;

    public GetCardsQueryHandler(IRepository<Card> cardRepository)
    {
        this.cardRepository = cardRepository;
    }

    public async Task<IEnumerable<DTOs.Card>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
    {
        var cards = await this.cardRepository.GetAllAsync(cancellationToken);

        return cards.Select(x => CardMapper.Map(x));
    }
}
