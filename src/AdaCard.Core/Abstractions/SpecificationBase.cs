using System.Linq.Expressions;

using AdaCard.Core.Interfaces;

namespace AdaCard.Core.Abstractions;

public abstract class SpecificationBase<T> : ISpecification<T>
    where T : EntityBase
{
    public SpecificationBase(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>> Criteria { get; }
}
