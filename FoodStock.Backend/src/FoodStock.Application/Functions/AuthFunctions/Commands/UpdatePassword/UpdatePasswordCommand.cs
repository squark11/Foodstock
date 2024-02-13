using MediatR;

namespace FoodStock.Application.Functions.AuthFunctions.Commands.UpdatePassword;

public sealed record UpdatePasswordCommand : IRequest<UpdatePasswordResponse>
{
    public Guid Id { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}
