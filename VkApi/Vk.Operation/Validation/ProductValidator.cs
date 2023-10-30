using FluentValidation;
using Vk.Schema;

namespace Vk.Operation.Validation;

public class CreateProductValidator: AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.ProductNumber).NotEmpty().WithMessage(" 'Product Number' degeri bos birakilamaz.");
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Category).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Stock).NotEmpty().GreaterThanOrEqualTo(0);
    }
}

public class UpdateProductValidator: AbstractValidator<UpdateProductRequest>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Stock).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.Category).NotEmpty().MaximumLength(50);

    }
}