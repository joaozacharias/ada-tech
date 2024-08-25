namespace AdaCard.Core.Features.Cards.DTOs.Mappers;

public static class CardMapper
{
    public static Func<Entities.Card, Card> Map = (existentCard) =>
    {
        return new Card
        {
            Conteudo = existentCard.Conteudo,
            Id = existentCard.Id,
            Lista = existentCard.Lista,
            Titulo = existentCard.Titulo
        };
    };
}
