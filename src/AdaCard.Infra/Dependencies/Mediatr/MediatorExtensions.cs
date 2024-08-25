using System.Reflection;

using AdaCard.Core.Features.Cards.Commands;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace AdaCard.Infra.Dependencies.Mediatr;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(CreateCardCommand))!;
        services.AddValidatorsFromAssembly(assembly);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            cfg.Lifetime = ServiceLifetime.Scoped;
        });

        return services;
    }
}
