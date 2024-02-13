using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.OrganizationFunctions.Commands.UpdateOrganization;

public class UpdateOrganizationCommandResponse : BaseResponse
{
    public Guid Id { get; set; }

    public UpdateOrganizationCommandResponse() : base()
    {
    }
    
    public UpdateOrganizationCommandResponse(string message) : base(message)
    {
    }

    public UpdateOrganizationCommandResponse(bool success, string message) : base(success, message)
    {
    }
    
    public UpdateOrganizationCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }

    public UpdateOrganizationCommandResponse(Guid id)
    {
        Id = id;
    }
}
