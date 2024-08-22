using AdaCard.Core.Features.Login.Services;
using AdaCard.Core.Interfaces;

namespace AdaCard.Api.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationDepencies(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        
        return services;
    }
}
