using MediatR;

namespace FoodStock.Application.Functions.OrganizationFunctions.Queries.GetOrganizationDetail;

public class GetOrganizationDetailQuery : IRequest<GetOrganizationDetailViewModel>
{
    public Guid Id { get; set; }
}
