using API.Dtos;
using API.Errors;
using API.Extensions;

using AutoMapper;

using Core.Entities.OrderAggregate;
using Core.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OrderAddress = Core.Entities.OrderAggregate.Address;

namespace API.Controllers;

[Authorize]
public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;

    private readonly IMapper _mapper;

    public OrdersController(
        IOrderService orderService,
        IMapper       mapper)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(
        OrderDto orderDto)
    {
        var email = HttpContext.User.RetrieveEmailFromPrincipal();

        var address = _mapper.Map<AddressDto, OrderAddress>(orderDto.ShipToAddress);

        var order = await _orderService.CreateOrderAsync(
            email,
            orderDto.DeliveryMethodId,
            orderDto.BasketId,
            address);

        if ( order == null )
            return BadRequest(
                new ApiResponse(
                    400,
                    "Problem creating order"));

        return Ok(order);
    }
}
