using FluentValidation;
using FoodStock.Application.Repositories;

namespace FoodStock.Application.Functions.RoleFunctions.Commands.UpdateRole;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    private readonly IRoleRepository _roleRepository;

    public UpdateRoleCommandValidator(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100);

        RuleFor(x => x.Name)
            .CustomAsync((value, context, cancellationToken) =>
            {
                var roleAlreadyExists = GetAlreadyExistsRole(value, context.InstanceToValidate.Id).Result;
                if (roleAlreadyExists)
                {
                    context.AddFailure("Role", "Role with this name already exists.");
                }

                return Task.CompletedTask;
            });
    }
    
    private async Task<bool> GetAlreadyExistsRole(string role, Guid roleId)
    {
        var roles = await _roleRepository.GetAllAsync();
        return roles.Any(r => r.Name == role && r.Id != roleId);
    }
}
