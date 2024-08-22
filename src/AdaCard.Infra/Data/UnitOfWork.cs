using AdaCard.Core.Interfaces;

namespace AdaCard.Infra.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AdaCardContext context;

    public UnitOfWork(AdaCardContext context)
    {
        this.context = context;
    }

    public async Task SaveAsync()
    {
        await this.context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await this.context.DisposeAsync();
    }
}
