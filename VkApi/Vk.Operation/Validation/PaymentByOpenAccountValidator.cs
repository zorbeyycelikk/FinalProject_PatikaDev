using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreatePaymentByOpenAccountRequestValidator: AbstractValidator<CreatePaymentByOpenAccountRequest>
{
    public CreatePaymentByOpenAccountRequestValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.ReceiverCustomerId).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(0);
    }
}