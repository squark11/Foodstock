namespace FoodStock.Core.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
    public virtual List<Product> Products { get; set; }
    public virtual List<OrderItem> OrderItems { get; set; }
}
