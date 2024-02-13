using MediatR;

namespace FoodStock.Application.Functions.ProductFunctions.Commands.DeleteProduct;

public sealed record DeleteProductCommand : IRequest
{
    public Guid Id { get; set; }
}
