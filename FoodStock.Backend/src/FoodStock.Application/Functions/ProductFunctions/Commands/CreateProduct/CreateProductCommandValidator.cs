using FluentValidation;

namespace FoodStock.Application.Functions.ProductFunctions.Commands.CreateProduct;

public class CreateProductCommandValidator 
    : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot exceed 100 characters.");

        RuleFor(p => p.ExpirationDate)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull()
            .Must(expirationDate =>
            {
                var oneWeekFromNow = DateTime.Now.AddDays(7);
                return expirationDate.Date >= oneWeekFromNow;
            })
            .WithMessage("Expiration date must be grater than one week.");

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

        RuleFor(p => p.DeliveryDate)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull()
            .Must(deliveryDate => deliveryDate.Date <= DateTime.Now)
            .WithMessage("Delivery date cannot be greater than added date.");

        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull();
        
        RuleFor(p => p.ProducentId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull();

        RuleFor(p => p.SupplierId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull();

    }
}
