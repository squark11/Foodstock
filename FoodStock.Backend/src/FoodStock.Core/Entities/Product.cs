using System.Security.AccessControl;
using System.Security.Principal;
using FoodStock.Core.Commons;

namespace FoodStock.Core.Entities;

public class Product : ProductBase
{
    public Guid Id { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime AddedDate { get; set; }
    public int Quantity { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public DateTime DeliveryDate { get; set; }

    public Product()
    {
        AddedDate = DateTime.Now;
    }
}
