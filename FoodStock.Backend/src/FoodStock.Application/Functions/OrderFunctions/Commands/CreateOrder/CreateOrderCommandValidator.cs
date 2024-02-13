using FluentValidation;

namespace FoodStock.Application.Functions.OrderFunctions.Commands.CreateOrderCommand;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.SupplierId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull();
    }
}
