using AdaCard.Core.Features.Cards.DTOs;

using FluentResults;

using MediatR;

namespace AdaCard.Core.Features.Cards.Commands;

public abstract class CardCommandBase : IRequest<Result<Card>>
{
    protected CardCommandBase(
        string titulo,
        string conteudo,
        string lista)
    {
        Titulo = titulo;
        Conteudo = conteudo;
        Lista = lista;
    }

    public string Titulo { get; }

    public string Conteudo { get; }

    public string Lista { get; }
}
