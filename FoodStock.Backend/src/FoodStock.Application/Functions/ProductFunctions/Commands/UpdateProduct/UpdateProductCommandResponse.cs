using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.ProductFunctions.Commands.UpdateProduct;

public class UpdateProductCommandResponse : BaseResponse
{
    public Guid? ProductId { get; set; }

    public UpdateProductCommandResponse() 
        : base()
    { }

    public UpdateProductCommandResponse(ValidationResult validationResult)
        : base(validationResult)
    { }

    public UpdateProductCommandResponse(string message) 
        : base(message)
    { }

    public UpdateProductCommandResponse(bool success, string message) 
        : base(success, message)
    { }

    public UpdateProductCommandResponse(Guid productId)
    {
        ProductId = productId;
    }
}
