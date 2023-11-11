using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateBasketValidator: AbstractValidator<CreateBasketRequest>
{
    public CreateBasketValidator()
    {
        
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}