using AutoMapper;
using FluentValidation;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Exceptions;
using MediatR;

namespace FoodStock.Application.Functions.OrganizationFunctions.Commands.UpdateOrganization;

public class UpdateOrganizationCommandHandler : IRequestHandler<UpdateOrganizationCommand, UpdateOrganizationCommandResponse>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IMapper _mapper;

    public UpdateOrganizationCommandHandler(IOrganizationRepository organizationRepository, IMapper mapper)
    {
        _organizationRepository = organizationRepository;
        _mapper = mapper;
    }
    
    public async Task<UpdateOrganizationCommandResponse> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateOrganizationCommandValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            return new UpdateOrganizationCommandResponse(validatorResult);
        }
        var organization = _mapper.Map<Organization>(request);
        await _organizationRepository.UpdateAsync(organization);
        return new UpdateOrganizationCommandResponse(organization.Id);
    }
}
