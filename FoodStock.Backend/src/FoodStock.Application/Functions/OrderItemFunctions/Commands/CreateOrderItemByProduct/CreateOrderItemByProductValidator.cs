using FluentValidation;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.CreateOrderItemByProduct;

public class CreateOrderItemByProductValidator : AbstractValidator<CreateOrderItemByProductCommand>
{
    public CreateOrderItemByProductValidator()
    {

        RuleFor(p => p.OrderId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull();
        
        RuleFor(p => p.Quantity)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull()
            .InclusiveBetween(1, 500)
            .WithMessage("{PropertyName} must be between 1 and 500");
    }
}
