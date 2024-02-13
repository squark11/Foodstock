using System.Reflection.Metadata.Ecma335;
using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.SupplierFunctions.Commands.UpdateSupplier;

public class UpdateSupplierCommandResponse : BaseResponse
{
    public Guid SupplierId { get; set; }
    
    public UpdateSupplierCommandResponse() : base()
    {
    }
    
    public UpdateSupplierCommandResponse(string message) : base(message)
    {
    }
    
    public UpdateSupplierCommandResponse(bool success, string message) : base(success, message)
    {
    }
    public UpdateSupplierCommandResponse(ValidationResult validationResult) : base(validationResult)
    {
    }
    
    public UpdateSupplierCommandResponse(Guid supplierId)
    {
        SupplierId = supplierId;
    }
}
