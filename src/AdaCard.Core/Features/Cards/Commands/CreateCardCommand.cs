using AdaCard.Core.Features.Cards.DTOs;

namespace AdaCard.Core.Features.Cards.Commands;

public class CreateCardCommand : CardCommandBase
{
    public CreateCardCommand(CreateCard createCard) : base(createCard.Titulo, createCard.Conteudo, createCard.Lista)
    {
    }
}
