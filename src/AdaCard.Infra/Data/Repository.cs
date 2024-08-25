using AdaCard.Core.Abstractions;
using AdaCard.Core.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace AdaCard.Infra.Data;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : EntityBase
{
    internal readonly DbSet<TEntity> dbSet;

    public Repository(AdaCardContext context)
    {
        this.dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var newEntity = await this.dbSet.AddAsync(entity);
        return newEntity.Entity;
    }

    public void DeleteAsync(TEntity entity)
    {
        this.dbSet.Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification, bool asNoTracking, CancellationToken cancellationToken, params string[] includes)
    {
        var query = asNoTracking
            ? dbSet.AsNoTracking().Where(specification.Criteria)
            : dbSet.Where(specification.Criteria);

        if (includes is not null)
        {
            includes.ToList().ForEach(include =>
            {
                if (include != null)
                {
                    query = query.Include(include);
                }
            });
        }

        return await query.ToArrayAsync();
    }

    public async Task<TEntity?> FindByIdAsync(object id, CancellationToken cancellationToken)
    {
        return await this.dbSet.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await this.dbSet.AsNoTracking().ToArrayAsync();
    }
}
