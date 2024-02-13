using FluentValidation;

namespace FoodStock.Application.Functions.OrderItemFunctions.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot exceed 100 characters.");
        
        RuleFor(p => p.Quantity)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull()
            .InclusiveBetween(1, 500)
            .WithMessage("{PropertyName} must be between 1 and 500");

        RuleFor(p => p.BarCode)
            .Matches("^[0-9]+$")
            .WithMessage("{PropertyName} must contain only numbers.")
            .Length(13)
            .WithMessage("The length of '{PropertyName}' must be exactly 13 characters.");
        
        RuleFor(p => p.OrderId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull();
        
        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull();
        
        RuleFor(p => p.ProducentId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull();
    }
}
