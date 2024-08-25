namespace AdaCard.Core.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    Task SaveAsync(CancellationToken cancellationToken);
}
