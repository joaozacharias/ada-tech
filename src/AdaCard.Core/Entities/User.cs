using AdaCard.Core.Abstractions;

namespace AdaCard.Core.Entities;

public class User : EntityBase
{
    public User(
        string login,
        string senha,
        string nome)
    {
        Login = login;
        Senha = senha;
        Nome = nome;
    }

    public string Login { get; }

    public string Senha { get; }

    public string Nome { get; }
}
