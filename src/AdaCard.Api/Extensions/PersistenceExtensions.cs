using AdaCard.Core.Interfaces;
using AdaCard.Core.Settings;
using AdaCard.Infra.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AdaCard.Api.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services)
    {
        services.AddDbContext<AdaCardContext>(opt => opt.UseInMemoryDatabase("AdaCardsBD"));

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static void AddDefaultValuesToDB(this WebApplication app)
    {
        var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<AdaCardContext>();

        var userDefault = new UserDefault();
        app.Configuration.GetSection(nameof(UserDefault)).Bind(userDefault);

        context.Users.Add(new Core.Entities.User(userDefault.Login, BCrypt.Net.BCrypt.HashPassword(userDefault.Senha), "DefaultUser"));
        context.SaveChanges();
    }
}
