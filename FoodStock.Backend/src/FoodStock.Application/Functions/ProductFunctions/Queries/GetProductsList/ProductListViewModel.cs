namespace FoodStock.Application.Functions.ProductFunctions.Queries.GetProductsList;

public sealed record ProductListViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string ProducentName { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int Quantity { get; set; }
    public string? BarCode { get; set; }
    public DateTime AddedDate { get; set; }
    public string UserFirstName { get; set; }
    public string UserSurname { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string SupplierName { get; set; }
}
