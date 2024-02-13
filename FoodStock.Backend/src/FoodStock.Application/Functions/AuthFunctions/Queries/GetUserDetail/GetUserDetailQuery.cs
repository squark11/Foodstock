using MediatR;

namespace FoodStock.Application.Functions.AuthFunctions.Queries.GetUserDetail;

public sealed class GetUserDetailQuery : IRequest<UserDetailViewModel>
{
    public Guid Id { get; set; }
}
