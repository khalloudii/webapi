using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities.OrderAggregate;
using Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Errors;
using WebAPI.Extensions;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this._orderService = orderService;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrderAsync(OrderViewModel ordervm)
        {
            var email = HttpContext.User.RetreiveEmailFromPrincipal();

            var address = _mapper.Map<AddressViewModel, Address>(ordervm.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, ordervm.DeliveryMethodId,
                ordervm.BasketId, address);

            if (order == null) return BadRequest(new ApiResponse(400, "Proplem creating order"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderToReturnViewModel>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetreiveEmailFromPrincipal();

            var orders = await _orderService.GetOrdersForUserAsync(email);

            //return Ok(orders);
            return Ok(_mapper.Map<List<Order>, List<OrderToReturnViewModel>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnViewModel>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetreiveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            //return order;
            return _mapper.Map<Order, OrderToReturnViewModel>(order);
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
    }
}