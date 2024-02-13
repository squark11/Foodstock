using FluentValidation;
using FoodStock.Application.Functions.RoleFunctions.Queries.GetRolesList;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;

namespace FoodStock.Application.Functions.RoleFunctions.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public CreateRoleCommandValidator(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100);

        RuleFor(x => x.Name)
            .Custom((value, context) =>
            {
                var roleAlreadyExists = GetAlreadyExistsRole(value).Result;
                if (roleAlreadyExists)
                {
                    context.AddFailure("Role", "Role with this name already exists.");
                }
            });
    }
    
    private async Task<bool> GetAlreadyExistsRole(string role)
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Any(r => r.Name == role);
    }
}
