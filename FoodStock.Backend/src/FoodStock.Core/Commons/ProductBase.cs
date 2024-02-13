using FoodStock.Core.Entities;

namespace FoodStock.Core.Commons;

public class ProductBase
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public Guid ProducentId { get; set; }
    public Producent Producent { get; set; }
    public string? BarCode { get; set; }
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; }
}
