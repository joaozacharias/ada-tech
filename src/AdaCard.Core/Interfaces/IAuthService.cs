using FluentResults;

namespace AdaCard.Core.Interfaces;

public interface IAuthService
{
    Task<Result<string>> AuthenticationAsync(string login, string senha);
}
