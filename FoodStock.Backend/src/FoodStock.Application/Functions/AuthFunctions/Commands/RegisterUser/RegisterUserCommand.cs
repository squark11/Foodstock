using MediatR;

namespace FoodStock.Application.Functions.AuthFunctions;

public sealed record RegisterUserCommand : IRequest<RegisterUserCommandResponse>
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public Guid RoleId { get; set; }
    public Guid? SupplierId { get; set; }
}
