using MediatR;

namespace FoodStock.Application.Functions.AuthFunctions.Queries.GetUsersList;

public class GetUserListQuery : IRequest<List<UserListViewModel>>
{
}
