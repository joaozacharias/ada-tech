using AdaCard.Core.Abstractions;

namespace AdaCard.Core.Interfaces;

public interface IRepository<TEntity> 
    where TEntity : EntityBase
{
    Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification, bool asNoTracking, params string[] includes);

    Task<TEntity> AddAsync(TEntity entity);

    void DeleteAsync(TEntity entity);
}
