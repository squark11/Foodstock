using MediatR;

namespace FoodStock.Application.Functions.AuthFunctions.Queries.GetInactiveUserList;

public class GetInactiveUserListQuery : IRequest<List<InactiveUserListViewModel>>
{
}
