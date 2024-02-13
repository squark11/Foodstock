using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using MediatR;

namespace FoodStock.Application.Functions.ProducentFunctions.Commands.CreateProducent;

public class CreateProducentCommandHandler : IRequestHandler<CreateProducentCommand, CreateProducentCommandResponse>
{
    private readonly IProducentRepository _producentRepository;
    private readonly IMapper _mapper;

    public CreateProducentCommandHandler(IProducentRepository producentRepository, IMapper mapper)
    {
        _producentRepository = producentRepository;
        _mapper = mapper;
    }
    
    public async Task<CreateProducentCommandResponse> Handle(CreateProducentCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateProducentCommandValidator(_producentRepository);
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid)
        {
            return new CreateProducentCommandResponse(validatorResult);
        }

        var producent = _mapper.Map<Producent>(request with { Id = new Guid() });
        producent = await _producentRepository.AddAsync(producent);

        return new CreateProducentCommandResponse(producent.Id);
    }
}
