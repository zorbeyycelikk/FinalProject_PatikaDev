using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateCustomerValidator: AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.CustomerNumber).NotEmpty().WithMessage(" 'Customer Number' degeri bos birakilamaz.");
        RuleFor(x => x.Email).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Profit)
            .NotEmpty().WithMessage("Profit cannot be empty.")
            .GreaterThanOrEqualTo(0).WithMessage("Profit must be greater than or equal to 0.")
            .LessThan(100).WithMessage("Profit must be less than 100.");
    }
}

public class UpdateCustomerValidator: AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.Email).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Profit)
            .NotEmpty().WithMessage("Profit cannot be empty.")
            .GreaterThanOrEqualTo(0).WithMessage("Profit must be greater than or equal to 0.")
            .LessThan(100).WithMessage("Profit must be less than 100.");        
    }
}