namespace FoodStock.Core.Entities;

public class Producent
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual List<Product> Products { get; set; }
    public virtual List<OrderItem> OrderItems { get; set; }
}
