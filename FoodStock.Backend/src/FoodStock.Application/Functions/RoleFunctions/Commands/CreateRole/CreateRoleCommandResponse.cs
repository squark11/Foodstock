using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.RoleFunctions.Commands.CreateRole;

public class CreateRoleCommandResponse : BaseResponse
{
    public Guid RoleId { get; set; }

    public CreateRoleCommandResponse() : base()
    {
    }

    public CreateRoleCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }
    
    public CreateRoleCommandResponse(string message) : base(message)
    {
    }

    public CreateRoleCommandResponse(bool success, string message) 
        : base(success, message)
    { }

    public CreateRoleCommandResponse(Guid roleId)
    {
        RoleId = roleId;
    }
}

