using FluentValidation.Results;
using FoodStock.Aplication.Responses;

namespace FoodStock.Application.Functions.ProductFunctions.Commands.CreateProduct;

public class CreateProductCommandResponse : BaseResponse
{
    public Guid? ProductId { get; set; }

    public CreateProductCommandResponse() 
        : base()
    { }

    public CreateProductCommandResponse(ValidationResult validationResult)
        : base(validationResult)
    { }

    public CreateProductCommandResponse(string message) 
        : base(message)
    { }

    public CreateProductCommandResponse(bool success, string message) 
        : base(success, message)
    { }

    public CreateProductCommandResponse(Guid productId)
    {
        ProductId = productId;
    }
}
