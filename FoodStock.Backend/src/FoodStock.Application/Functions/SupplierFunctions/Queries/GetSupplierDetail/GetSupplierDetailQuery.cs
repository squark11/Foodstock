using MediatR;

namespace FoodStock.Application.Functions.SupplierFunctions.Queries.GetSupplierDetail;

public class GetSupplierDetailQuery : IRequest<SupplierDetailViewModel>
{
    public Guid Id { get; set; }
}
