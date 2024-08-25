using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using AdaCard.Core.Entities;
using AdaCard.Core.Features.Login.Specifications;
using AdaCard.Core.Interfaces;
using AdaCard.Core.Settings;

using FluentResults;

using Microsoft.IdentityModel.Tokens;

namespace AdaCard.Core.Features.Login.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> userRepository;
    private readonly AuthenticationConfigurations authenticationConfigurations;

    public AuthService(
        IRepository<User> userRepository,
        AuthenticationConfigurations authenticationConfigurations)
    {
        this.userRepository = userRepository;
        this.authenticationConfigurations = authenticationConfigurations;
    }

    public async Task<Result<string>> AuthenticationAsync(string login, string senha, CancellationToken cancellationToken)
    {

        var users = await userRepository.FindAsync(new FindByLoginSpec(login), true, cancellationToken);

        if (users == null || !users.Any())
        {
            return Result.Fail("User not found!");
        }

        var user = users.First();

        if (!BCrypt.Net.BCrypt.Verify(senha, user.Senha))
        {
            return Result.Fail("Usuário ou senha inválido!");
        }

        return GenerateToken(user);

    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(authenticationConfigurations.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Nome.ToString()),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            IssuedAt = DateTime.UtcNow
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
