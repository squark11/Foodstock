using AutoMapper;
using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using FoodStock.Core.Enums;
using FoodStock.Core.Exceptions;
using FoodStock.Infrastructure.Authentication.Services;
using MediatR;

namespace FoodStock.Application.Functions.OrderFunctions.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, UpdateOrderCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IUserContextService userContextService, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _userContextService = userContextService;
        _mapper = mapper;
    }
    
    public async Task<UpdateOrderCommandResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var supplierRole = "494fbfca-ff6f-4f16-b16a-58e14b6a2354";
        var validator = new UpdateCommandOrderValidator();
        var validatorResult = await validator.ValidateAsync(request);
        if (!validatorResult.IsValid && validatorResult.Errors != null)
        {
            return new UpdateOrderCommandResponse(validatorResult);
        }
        var order = await _orderRepository.GetOrderDetailAsync(request.Id);
        if (order is null)
        {
            throw new OrderNotFoundException(request.Id);
        }
        _mapper.Map(request, order);
        if (_userContextService.GetUserRole == Guid.Parse(supplierRole) && request.OrderStatus == OrderStatus.Accepted.ToString())
        {
            order.AcceptanceOfTheOrderDate = DateTime.Now;
            order.AcceptedBy = _userContextService.GetUserId;
        }
        await _orderRepository.UpdateAsync(order);
        return new UpdateOrderCommandResponse(order.Id);
    }
}
