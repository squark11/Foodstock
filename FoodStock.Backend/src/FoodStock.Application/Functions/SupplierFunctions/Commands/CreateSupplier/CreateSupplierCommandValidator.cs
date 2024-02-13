using FluentValidation;
using FoodStock.Application.Repositories;

namespace FoodStock.Application.Functions.SupplierFunctions.Commands.CreateSupplier;

public class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
{
    private readonly ISupplierRepository _supplierRepository;

    public CreateSupplierCommandValidator(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;

        RuleFor(s => s.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(150)
            .WithMessage("{PropertyName} cannot exceed 150 characters.");

        RuleFor(s => s.Name)
            .Custom((value, context) =>
            {
                var takenNames = GetAlreadyExistsSuppliersNames(value).Result;
                if (takenNames)
                {
                    context.AddFailure("Name", "Supplier with this name already exists");
                }
            });


    }

    private async Task<bool> GetAlreadyExistsSuppliersNames(string name)
    {
        var suppliers = await _supplierRepository.GetAllAsync();
        return suppliers.Any(s => s.Name == name);
    }
}
