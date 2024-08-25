using AdaCard.Core.Features.Cards.Commands;

using FluentValidation;

namespace AdaCard.Core.Features.Cards.Validators;

public abstract class CardCommandBaseValidator<T> : AbstractValidator<T>
    where T : CardCommandBase
{
    public CardCommandBaseValidator()
    {
        RuleFor(x => x.Conteudo)
            .NotEmpty()
            .WithMessage("Conteúdo é obrigatório!");

        RuleFor(x => x.Lista)
            .NotEmpty()
            .WithMessage("Lista é obrigatório!");

        RuleFor(x => x.Titulo)
            .NotEmpty()
            .WithMessage("Título é obrigatório!");
    }
}
