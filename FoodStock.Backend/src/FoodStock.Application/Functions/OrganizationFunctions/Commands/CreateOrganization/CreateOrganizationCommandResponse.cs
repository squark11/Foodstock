using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.OrganizationFunctions.Commands.CreateOrganization;

public class CreateOrganizationCommandResponse : BaseResponse
{
    public Guid OrganizationId { get; set; }

    public CreateOrganizationCommandResponse() : base()
    {
    }
    
    public CreateOrganizationCommandResponse(string message) : base(message)
    {
    }
    
    public CreateOrganizationCommandResponse(bool success, string message) : base(success,message)
    {
    }
    
    public CreateOrganizationCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }
    
    public CreateOrganizationCommandResponse(Guid organizationId)
    {
        OrganizationId = organizationId;
    }
    
    
    
}
