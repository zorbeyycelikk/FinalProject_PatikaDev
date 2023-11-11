using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreatePaymentByHavaleRequestValidator: AbstractValidator<CreatePaymentByHavaleRequest>
{
    public CreatePaymentByHavaleRequestValidator()
    {
        RuleFor(x => x.SenderAccountNumber).NotEmpty();
        RuleFor(x => x.AccountNumber).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.TransferDescription).MaximumLength(50);
        RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(0);
    }
}