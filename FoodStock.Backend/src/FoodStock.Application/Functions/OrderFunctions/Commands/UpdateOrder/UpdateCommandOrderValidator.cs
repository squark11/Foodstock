using FluentValidation;
using MediatR;

namespace FoodStock.Application.Functions.OrderFunctions.Commands.UpdateOrder;

public class UpdateCommandOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateCommandOrderValidator()
    {
        RuleFor(x => x.OrderStatus)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .NotNull();
    }
}