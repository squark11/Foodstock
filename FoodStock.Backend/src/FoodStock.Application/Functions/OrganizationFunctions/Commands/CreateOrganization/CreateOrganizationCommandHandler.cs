using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Exceptions.AlreadyExistsException;
using MediatR;

namespace FoodStock.Application.Functions.OrganizationFunctions.Commands.CreateOrganization;

public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, CreateOrganizationCommandResponse>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;

    public CreateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateOrganizationCommandResponse> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var organizations = await _organizationRepository.GetAllAsync();
        if (organizations.Any())
        {
            throw new OrganizationAlreadyExistsException();
        }
        var validator = new CreateOrganizationCommandValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            return new CreateOrganizationCommandResponse(validatorResult);
        }
        
        var organization = _mapper.Map<Organization>(request with { Id = new Guid()});
        organization = await _organizationRepository.AddAsync(organization);
    
        return new CreateOrganizationCommandResponse(organization.Id);
    }
    
}
