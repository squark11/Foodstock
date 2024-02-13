using FoodStock.Core.Commons;
using FoodStock.Core.Enums;

namespace FoodStock.Core.Entities;

public class Order
{
    public Guid Id { get; set; }
    public string OrderName { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? AcceptanceOfTheOrderDate { get; set; }
    public Guid OrderBy { get; set; }
    public Guid? AcceptedBy { get; set; }
    public virtual User User { get; set; }
    public string OrderStatus { get; set; }
    public Guid OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }
    public Guid SupplierId { get; set; }
    public virtual Supplier Supplier { get; set; }
    public virtual List<OrderItem> OrderItems { get; set; }
}
