using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreatePaymentByCardRequestValidator: AbstractValidator<CreatePaymentByCardRequest>
{
    public CreatePaymentByCardRequestValidator()
    {
        RuleFor(x => x.receiverAccountNumber).NotEmpty();
        RuleFor(x => x.CardNumber).NotEmpty().MaximumLength(16);
        RuleFor(x => x.Cvv).NotEmpty().MaximumLength(3);
        RuleFor(x => x.ExpiryDate).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(0);
    }
}