using FoodStock.Core.Commons;

namespace FoodStock.Core.Entities;

public class OrderItem : ProductBase
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public int Quantity { get; set; }
    public Guid ProducentId { get; set; }
    public string? BarCode { get; set; }
    public Guid SupplierId { get; set; }
}
