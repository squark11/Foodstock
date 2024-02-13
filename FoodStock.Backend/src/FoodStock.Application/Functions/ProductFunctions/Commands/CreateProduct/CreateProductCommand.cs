using FoodStock.Core;
using MediatR;

namespace FoodStock.Application.Functions.ProductFunctions.Commands.CreateProduct;

public sealed record CreateProductCommand : IRequest<CreateProductCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ProducentId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Quantity { get; set; }
    public string BarCode { get; set; }
    public DateTime DeliveryDate { get; set; }
    public Guid SupplierId { get; set; }
}
