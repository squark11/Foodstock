using MediatR;

namespace FoodStock.Application.Functions.RoleFunctions.Queries.GetRoleDetail;

public class GetRoleDetailQuery : IRequest<RoleDetailViewModel>
{
    public Guid Id { get; set; }
}
