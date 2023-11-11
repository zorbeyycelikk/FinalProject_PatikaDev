using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateCompleteOrderValidatorHavale: AbstractValidator<CreateCompleteOrderWithHavaleRequest>
{
    public CreateCompleteOrderValidatorHavale()
    {
        
        RuleFor(x => x.CustomerId).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(50);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PaymentMethod).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.BasketId).NotEmpty();
        
        RuleFor(x => x.SenderAccountNumber).NotEmpty();
        RuleFor(x => x.AccountNumber).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateCompleteOrderValidatorEft: AbstractValidator<CreateCompleteOrderWithEftRequest>
{
    public CreateCompleteOrderValidatorEft()
    {
        
        RuleFor(x => x.CustomerId).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Description).MaximumLength(50);
        RuleFor(x => x.Address).NotEmpty().MaximumLength(50);
        RuleFor(x => x.PaymentMethod).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.BasketId).NotEmpty();
        
        RuleFor(x => x.SenderAccountNumber).NotEmpty();
        RuleFor(x => x.IBAN).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(50);
    }
}