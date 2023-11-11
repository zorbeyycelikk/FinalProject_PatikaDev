using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateOrderValidator: AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PaymentMethod).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PaymentRefCode).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.BasketId).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(50);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(50);
    }
}

public class UpdateOrderValidator: AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Status).NotEmpty().MaximumLength(15);
    }
}