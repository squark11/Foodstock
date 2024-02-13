using MediatR;

namespace FoodStock.Application.Functions.RoleFunctions.Queries.GetRolesList;

public class GetRoleListQuery : IRequest<List<RoleListViewModel>>
{
}
