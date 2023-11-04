using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateAccountValidator: AbstractValidator<CreateAccountRequest>
{
    public CreateAccountValidator()
    {
        
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.IBAN).NotEmpty().MaximumLength(34);
        RuleFor(x => x.Balance).NotEmpty();
        RuleFor(x => x.OpenDate).NotEmpty();
        RuleFor(x => x.CloseDate).NotEmpty();
    }
}

public class UpdateAccountValidator: AbstractValidator<UpdateAccountRequest>
{
    public UpdateAccountValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}

