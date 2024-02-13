using System.Data;
using FluentValidation;

namespace FoodStock.Application.Functions.AuthFunctions.Commands.UpdatePassword;

public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordValidator()
    {
        RuleFor(x => x.Password)
            .MinimumLength(8);

        RuleFor(x => x.ConfirmPassword)
            .Equal(e => e.Password);
    }
}
