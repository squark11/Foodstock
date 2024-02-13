namespace FoodStock.Application.Functions.OrderItemFunctions.Queries.GetOrderItemDetail;

public class GetOrderItemViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }
    public Guid ProducentId { get; set; }
    public string ProducentName { get; set; }
    public string? BarCode { get; set; }
}
