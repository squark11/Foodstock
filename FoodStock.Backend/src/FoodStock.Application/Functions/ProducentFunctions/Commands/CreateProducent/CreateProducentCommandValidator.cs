using FluentValidation;
using FoodStock.Application.Repositories;

namespace FoodStock.Application.Functions.ProducentFunctions.Commands.CreateProducent;

public class CreateProducentCommandValidator : AbstractValidator<CreateProducentCommand>
{
    private readonly IProducentRepository _producentRepository;

    public CreateProducentCommandValidator(IProducentRepository producentRepository)
    {
        _producentRepository = producentRepository;
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(150)
            .WithMessage("{PropertyName} cannot exceed 150 character.");

        RuleFor(x => x.Name)
            .Custom((value, context) =>
            {
                var takenNames = GetAlreadyExistsProducentNames(value).Result;
                if (takenNames)
                {
                    context.AddFailure("Name", "Producent with this name already exists");
                }
            });
    }

    private async Task<bool> GetAlreadyExistsProducentNames(string name)
    {
        var producents = await _producentRepository.GetAllAsync();
        return producents.Any(p => p.Name == name);
    }
}
