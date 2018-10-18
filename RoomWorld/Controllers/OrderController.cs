using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Exceptions;
using Service.Services;

namespace RoomWorld.Controllers
{
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("/user/orders")]
        [Authorize]
        public async Task<IActionResult> OrderedFlats()
        {
            try
            {
                var orders = await _orderService.GetOrdersByEmailAsync();
                return Ok(orders);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpGet("/user/flats/orders")]
        [Authorize]
        public async Task<IActionResult> OrderForUserFlats()
        {
            try
            {
                var orders = await _orderService.GetOrdersForUsersFlatsAsync();
                return Ok(orders);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("/add/order")]
        [Authorize]
        public async Task<IActionResult> OrderFlat(OrderParams orderParams)
        {
            try
            {
                await _orderService.AddOrderAsync(orderParams);
                return Ok();
            }
            catch (FlatNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}