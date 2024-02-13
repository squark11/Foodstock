using FluentValidation;
using FoodStock.Application.Repositories;

namespace FoodStock.Application.Functions.ProducentFunctions.Commands.UpdateProducent;

public class UpdateProducentCommandValidator : AbstractValidator<UpdateProducentCommand>
{
    private readonly IProducentRepository _producentRepository;

    public UpdateProducentCommandValidator(IProducentRepository producentRepository)
    {
        _producentRepository = producentRepository;
        
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(150)
            .WithMessage("{PropertyName} cannot exceed 150 character.");

        RuleFor(p => p.Name)
            .Custom((value, context) =>
            {
                var takenNames = GetAlreadyExistsProducentNames(value, context.InstanceToValidate.Id).Result;
                if (takenNames)
                {
                    context.AddFailure("Name", "Producent with this name already exists");

                }
            });
    }

    private async Task<bool> GetAlreadyExistsProducentNames(string name, Guid producentId)
    {
        var producents = await _producentRepository.GetAllAsync();
        return producents.Any(x => x.Name == name && x.Id != producentId);
    }
}
