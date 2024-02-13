using MediatR;

namespace FoodStock.Application.Functions.SupplierFunctions.Queries.GetSupplierList;

public class GetSupplierListQuery : IRequest<List<SupplierListViewModel>>
{
}
