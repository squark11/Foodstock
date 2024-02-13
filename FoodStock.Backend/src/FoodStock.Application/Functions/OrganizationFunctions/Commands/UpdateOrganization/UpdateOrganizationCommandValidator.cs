using FluentValidation;

namespace FoodStock.Application.Functions.OrganizationFunctions.Commands.UpdateOrganization;

public class UpdateOrganizationCommandValidator : AbstractValidator<UpdateOrganizationCommand>
{
    public UpdateOrganizationCommandValidator()
    {
         RuleFor(o => o.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot exceed 100 character.");
        
        RuleFor(o => o.OwnerName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot exceed 100 character.");
        
        RuleFor(o => o.OwnerSurname)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot exceed 100 character.");
        
        RuleFor(o => o.Nip)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .Matches("^[0-9]+$")
            .WithMessage("{PropertyName} must contain only numbers.")
            .Length(10)
            .WithMessage("{PropertyName} cannot exceed 10 character.");

        RuleFor(o => o.Country)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot exceed 100 character.");
        
        RuleFor(o => o.City)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot exceed 100 character.");
        
        RuleFor(o => o.CityCode)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("{PropertyName} is not valid. It should be (e.g. 00-000)")
            .Length(6)
            .WithMessage("{PropertyName} cannot exceed 6 characters.");
        
        RuleFor(o => o.StreetNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot exceed 100 character.");
        
        RuleFor(o => o.StreetNumber)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .Matches(@"^\d{1,3}(\/\d{1,3}[A-Z]?)?$")
            .WithMessage("{PropertyName} must start with a number and may include additional letters or characters (e.g., 122/12, 2/12A, 8, 2/12B).")
            .MaximumLength(100)
            .WithMessage("{PropertyName} cannot exceed 100 character.");
    }
}
