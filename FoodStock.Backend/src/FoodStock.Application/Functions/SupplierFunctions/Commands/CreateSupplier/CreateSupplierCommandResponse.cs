using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.SupplierFunctions.Commands.CreateSupplier;

public class CreateSupplierCommandResponse : BaseResponse
{
    public Guid SupplierId { get; set; }

    public CreateSupplierCommandResponse() : base()
    {
    }
    
    public CreateSupplierCommandResponse(string message) : base(message)
    {
    }
    
    public CreateSupplierCommandResponse(bool success, string message) : base(success, message)
    {
    }
    
    public CreateSupplierCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }
    
    public CreateSupplierCommandResponse(Guid supplierId)
    {
        SupplierId = supplierId;
    }
}
