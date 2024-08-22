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

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var newEntity = await this.dbSet.AddAsync(entity);
        return newEntity.Entity;
    }

    public void DeleteAsync(TEntity entity)
    {
        this.dbSet.Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> specification, bool asNoTracking, params string[] includes)
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
}
