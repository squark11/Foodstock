using FluentValidation;

namespace FoodStock.Application.Functions.CategoryFunctions.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.CategoryName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot exceed 100 characters.");
    }
}
