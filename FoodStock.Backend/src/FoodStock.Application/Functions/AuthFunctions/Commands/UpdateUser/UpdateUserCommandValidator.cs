using FluentValidation;
using FoodStock.Application.Repositories;

namespace FoodStock.Application.Functions.AuthFunctions.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .EmailAddress();
        
        RuleFor(x => x.Email)
            .Custom((value, context) =>
            {
                var emailInUse = GetAlreadyExistsEmails(value, context.InstanceToValidate.Id).Result;
                if (emailInUse)
                {
                    context.AddFailure("Email", "That email is taken.");
                }
            });

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MaximumLength(100)
            .NotNull();
        
        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MaximumLength(100)
            .NotNull();
        
        RuleFor(x => x.RoleId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull();
    }

    private async Task<bool> GetAlreadyExistsEmails(string email, Guid userId)
    {
        var emails = await _userRepository.GetAllAsync();
        return emails.Any(e => e.Email == email && e.Id != userId);
    }
}
