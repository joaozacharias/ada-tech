using AdaCard.Core.Abstractions;

namespace AdaCard.Core.Interfaces;

public interface IRepository<TEntity> 
    where TEntity : EntityBase
{
    Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification, bool asNoTracking, CancellationToken cancellationToken, params string[] includes);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity?> FindByIdAsync(object id, CancellationToken cancellationToken);

    void DeleteAsync(TEntity entity);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
}
