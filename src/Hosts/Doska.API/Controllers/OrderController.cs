using Doska.AppServices.Services.Order;
using Doska.AppServices.Services.User;
using Doska.Contracts.OrderDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doska.API.Controllers
{

    public class OrderController : ControllerBase
    {
        IOrderService _OrderService;
        IUserService _UserService;
        public OrderController(IOrderService OrderService, IUserService UserService)
        {
            _OrderService = OrderService;
            _UserService = UserService;
        }



        [HttpGet("/allOrders")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoOrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _OrderService.GetAll(take, skip);

            return Ok(result);
        }

        [HttpGet("/allUserOrders")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoOrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserOrders(CancellationToken token,int take= 500, int skip=0)
        {
            var result = await _OrderService.GetUserOrders(take, skip,await _UserService.GetCurrentUserId(token));

            return Ok(result);
        }



        [HttpPost("/createOrder")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoOrderResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateOrder(Guid ProductId,CancellationToken token)
        {
            var result = await _OrderService.CreateOrderAsync(
                new CreateOrEditOrderRequest { ProductId = ProductId,
                UserId = await _UserService.GetCurrentUserId(token) });

            return Created("", result);
        }

        [HttpPut("/updateOrder/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoOrderResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateOrder(Guid id, CreateOrEditOrderRequest request)
        {
            var result = await _OrderService.EditOrderAsync(id, request);

            return Ok(result);
        }

        [HttpDelete("/deleteOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteOrder(Guid Id)
        {
            await _OrderService.DeleteAsync(Id);
            return Ok();
        }
    }
}
