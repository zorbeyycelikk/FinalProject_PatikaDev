using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateBasketItemValidator: AbstractValidator<CreateBasketItemRequest>
{
    public CreateBasketItemValidator()
    {

        RuleFor(x => x.BasketId).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Quantity).NotEmpty().GreaterThan(0);
    }
}