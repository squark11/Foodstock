using FoodStock.Application.Functions.OrderItemFunctions.Queries.GetOrderItemDetail;
using FoodStock.Application.Functions.OrganizationFunctions.Queries.GetOrganizationDetail;
using FoodStock.Application.Functions.SupplierFunctions.Queries.GetSupplierDetail;
namespace FoodStock.Application.Functions.OrderFunctions.Queries.GetOrderDetail;

public sealed record OrderDetailViewModel
{
    public Guid Id { get; set; }
    public string OrderName { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? AcceptanceOfTheOrderDate { get; set; }
    public Guid OrderBy { get; set; }
    public Guid? AcceptedBy { get; set; }
    public string OrderStatus { get; set; }
    public GetOrganizationDetailViewModel Organization { get; set; }
    public SupplierDetailViewModel Supplier { get; set; }
    public List<GetOrderItemViewModel> OrderItems { get; set; }
}
