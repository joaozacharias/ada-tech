using AdaCard.Core.Entities;
using AdaCard.Core.Interfaces;

using Microsoft.Extensions.Logging;

namespace AdaCard.Infra.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AdaCardContext context;
    private readonly ILogger<UnitOfWork> logger;

    public UnitOfWork(
        AdaCardContext context,
        ILogger<UnitOfWork> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        LogOperations();

        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await this.context.DisposeAsync();
    }

    private void LogOperations()
    {
        foreach (var entry in this.context.ChangeTracker.Entries())
        {
            if (entry.Entity is Card entity)
            {
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                {
                    this.logger.LogInformation("{DateTime} - Card {Id} - {Titulo} - {Operacao}", DateTime.UtcNow, entity.Id, entity.Titulo, "Alterar");
                }

                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
                {
                    this.logger.LogInformation("{DateTime} - Card {Id} - {Titulo} - {Operacao}", DateTime.UtcNow, entity.Id, entity.Titulo, "Removido");
                }
            }
        }
    }
}
