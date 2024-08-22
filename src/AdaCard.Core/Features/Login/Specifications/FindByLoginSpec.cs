using AdaCard.Core.Abstractions;
using AdaCard.Core.Entities;

namespace AdaCard.Core.Features.Login.Specifications;

public class FindByLoginSpec : SpecificationBase<User>
{
    public FindByLoginSpec(string login)
        : base(x => x.Login == login) { }
}
