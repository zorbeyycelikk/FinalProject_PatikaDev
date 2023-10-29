using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateCustomerValidator: AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.CustomerNumber).NotEmpty().WithMessage(" 'Customer Number' degeri bos birakilamaz.");
        RuleFor(x => x.CustomerNumber).GreaterThan(0).WithMessage(" 'Customer Number' degeri 0'dan buyuk olmalidir.");
        RuleFor(x => x.Email).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(50);
    }
}

public class UpdateCustomerValidator: AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.Email).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(50);
    }
}