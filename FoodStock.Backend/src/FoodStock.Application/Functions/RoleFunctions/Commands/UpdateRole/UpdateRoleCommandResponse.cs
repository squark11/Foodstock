using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.RoleFunctions.Commands.UpdateRole;

public class UpdateRoleCommandResponse : BaseResponse
{
    public Guid RoleId { get; set; }
    
    public UpdateRoleCommandResponse() : base()
    {}

    public UpdateRoleCommandResponse(ValidationResult validationResult) 
        : base(validationResult)
    {}

    public UpdateRoleCommandResponse(string message) : base(message)
    {}

    public UpdateRoleCommandResponse(bool success, string message) : base(success, message)
    {}

    public UpdateRoleCommandResponse(Guid roleId)
    {
        RoleId = roleId;
    }
}
