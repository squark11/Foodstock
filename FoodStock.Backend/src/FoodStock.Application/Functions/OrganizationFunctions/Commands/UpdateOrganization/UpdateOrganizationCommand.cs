using MediatR;

namespace FoodStock.Application.Functions.OrganizationFunctions.Commands.UpdateOrganization;

public sealed record UpdateOrganizationCommand : IRequest<UpdateOrganizationCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OwnerName { get; set; }
    public string OwnerSurname { get; set; }
    public string Nip { get; set; }
    public string Country  { get; set; }
    public string City { get; set; }
    public string CityCode { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
}
