namespace FoodStock.Core.Entities;

public class Supplier
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual List<Product> Products { get; set; }
    public virtual List<OrderItem> OrderItems { get; set; }
    public virtual List<Order> Orders { get; set; }
}
