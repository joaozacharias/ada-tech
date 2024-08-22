using AdaCard.Core.Abstractions;

namespace AdaCard.Core.Entities;

public class Card : EntityBase
{
    public Card(
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
