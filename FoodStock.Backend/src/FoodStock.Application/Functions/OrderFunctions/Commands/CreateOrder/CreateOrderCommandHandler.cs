using AutoMapper;
using FoodStock.Application.Functions.OrderFunctions.Commands.CreateOrderCommand;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Enums;
using FoodStock.Core.Exceptions;
using FoodStock.Infrastructure.Authentication.Services;
using MediatR;

namespace FoodStock.Application.Functions.OrderFunctions.Commands.CreateOrder;

public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand.CreateOrderCommand, CreateOrderCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContextService;
    private readonly IOrganizationRepository _organizationRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IUserContextService userContextService, IOrganizationRepository organizationRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _userContextService = userContextService;
        _organizationRepository = organizationRepository;
    }
    
    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand.CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var organization = await GetOrganization();
        var order = _mapper.Map<Order>(request with { Id = Guid.NewGuid() });
        
        order.OrderBy = _userContextService.GetUserId;
        order.OrderName = $"{organization.Name}_{order.OrderDate:yyyyMMdd}";
        order.OrganizationId = organization.Id;
        order.OrderStatus = OrderStatus.New.ToString();
        
        order = await _orderRepository.AddAsync(order);
        return new CreateOrderCommandResponse(order.Id);
    }

    private async Task<Organization> GetOrganization()
    {
        var organizations = await _organizationRepository.GetAllAsync();
        var organization = organizations.FirstOrDefault();
        if (organization is null)
        {
            throw new OrganizationInfoNotFound();
        }
        return organization;
    }
}
