using FluentValidation;

namespace FoodStock.Application.Functions.CategoryFunctions.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.CategoryName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot exceed 100 characters.");
    }
}
