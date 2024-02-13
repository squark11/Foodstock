using FluentValidation;
using FoodStock.Application.Repositories;

namespace FoodStock.Application.Functions.SupplierFunctions.Commands.UpdateSupplier;

public class UpdateSupplierCommandValidator : AbstractValidator<UpdateSupplierCommand>
{
    private readonly ISupplierRepository _supplierRepository;

    public UpdateSupplierCommandValidator(ISupplierRepository supplierRepository)
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
                var takenNames = GetAlreadyExistsSuppliersNames(value, context.InstanceToValidate.Id).Result;
                if (takenNames)
                {
                    context.AddFailure("Name", "Supplier with this name already exists");
                }
            });
    }

    private async Task<bool> GetAlreadyExistsSuppliersNames(string name, Guid supplierId)
    {
        var suppliers = await _supplierRepository.GetAllAsync();
        return suppliers.Any(s => s.Name == name && s.Id != supplierId);
    }
}
