using AdaCard.Core.Abstractions;

namespace AdaCard.Core.Entities;

public class Card : EntityBase
{
    public Card(
        string titulo,
        string conteudo,
        string lista)
    {
        this.Titulo = titulo;
        this.Conteudo = conteudo;
        this.Lista = lista;
    }

    public string Titulo { get; private set; }

    public string Conteudo { get; private set; }

    public string Lista { get; private set; }

    public void UpdateCard(string titulo, string conteudo, string lista)
    {
        this.Titulo = titulo;
        this.Conteudo = conteudo;
        this.Lista = lista;
    }
}
