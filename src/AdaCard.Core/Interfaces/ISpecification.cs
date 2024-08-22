using System.Linq.Expressions;

using AdaCard.Core.Abstractions;

namespace AdaCard.Core.Interfaces;

public interface ISpecification<T>
    where T : EntityBase
{
    Expression<Func<T, bool>> Criteria { get; }
}
