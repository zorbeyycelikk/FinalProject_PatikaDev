using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateOrderValidator: AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.OrderNumber).NotEmpty().WithMessage(" 'Order Number' degeri bos birakilamaz.");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Status).NotEmpty().MaximumLength(50);
    }
}

public class UpdateOrderValidator: AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Status).NotEmpty().MaximumLength(50);
    }
}