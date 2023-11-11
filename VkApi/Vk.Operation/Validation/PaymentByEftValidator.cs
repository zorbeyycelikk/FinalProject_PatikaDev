using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreatePaymentByEftRequestValidator: AbstractValidator<CreatePaymentByEftRequest>
{
    public CreatePaymentByEftRequestValidator()
    {
        RuleFor(x => x.SenderAccountNumber).NotEmpty();
        RuleFor(x => x.IBAN).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.TransferDescription).MaximumLength(50);
        RuleFor(x => x.Amount).NotEmpty().GreaterThanOrEqualTo(0);
    }
}