namespace FoodStock.Core.Exceptions;

public sealed class OrganizationInfoNotFound : NotFoundException
{
    public OrganizationInfoNotFound() : base("Please configure your organization information")
    {
    }
}
