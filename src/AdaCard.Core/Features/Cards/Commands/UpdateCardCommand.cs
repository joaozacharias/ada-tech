using AdaCard.Core.Features.Cards.DTOs;

namespace AdaCard.Core.Features.Cards.Commands;

public class UpdateCardCommand : CardCommandBase
{
    public UpdateCardCommand(int id, CreateCard updateCard)
        : base(updateCard.Titulo, updateCard.Conteudo, updateCard.Lista)
    {
        this.Id = id;
    }

    public int Id { get; }
}
