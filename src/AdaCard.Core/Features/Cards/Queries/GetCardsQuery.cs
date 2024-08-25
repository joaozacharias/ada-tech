using AdaCard.Core.Features.Cards.DTOs;

using MediatR;

namespace AdaCard.Core.Features.Cards.Queries;

public class GetCardsQuery : IRequest<IEnumerable<Card>>
{
}
