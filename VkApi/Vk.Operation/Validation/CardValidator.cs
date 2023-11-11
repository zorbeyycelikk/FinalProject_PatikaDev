using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateCardValidator: AbstractValidator<CreateCardRequest>
{
    public CreateCardValidator()
    {
        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.CardNumber).NotEmpty().MaximumLength(16);
        RuleFor(x => x.Cvv).NotEmpty().MaximumLength(3);
        RuleFor(x => x.ExpiryDate).NotEmpty();
    }
}

public class UpdateCardValidator: AbstractValidator<UpdateCardRequest>
{
    public UpdateCardValidator()
    {
        RuleFor(x => x.ExpiryDate).NotEmpty();
    }
}