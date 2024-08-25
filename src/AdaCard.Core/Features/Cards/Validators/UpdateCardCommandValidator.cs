using AdaCard.Core.Features.Cards.Commands;

using FluentValidation;

namespace AdaCard.Core.Features.Cards.Validators;

public class UpdateCardCommandValidator : CardCommandBaseValidator<UpdateCardCommand>
{
    public UpdateCardCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID é obrigatório!");
    }
}
