using AutoMapper;
using FoodStock.Application.Functions.OrganizationFunctions.Queries.GetOrganizationList;
using FoodStock.Application.Repositories;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.OrganizationFunctions.Queries.GetOrganizationDetail;

public class GetOrganizationDetailHandler : IRequestHandler<GetOrganizationDetailQuery, GetOrganizationDetailViewModel>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;

    public GetOrganizationDetailHandler(IOrganizationRepository organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }
    
    public async Task<GetOrganizationDetailViewModel> Handle(GetOrganizationDetailQuery request, CancellationToken cancellationToken)
    {
        var organization = await _organizationRepository.GetByIdAsync(request.Id);
        if (organization is null)
        {
            throw new OrganizationNotFoundException(request.Id);
        }
        var organizationDetail = _mapper.Map<GetOrganizationDetailViewModel>(organization);
        return organizationDetail;
    }
}
