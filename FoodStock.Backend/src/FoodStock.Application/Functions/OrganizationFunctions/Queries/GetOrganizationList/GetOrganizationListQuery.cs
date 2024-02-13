using MediatR;

namespace FoodStock.Application.Functions.OrganizationFunctions.Queries.GetOrganizationList;

public class GetOrganizationListQuery : IRequest<List<GetOrganizationListViewModel>>
{
}
