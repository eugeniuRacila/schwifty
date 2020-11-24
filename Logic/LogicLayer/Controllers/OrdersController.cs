using System.Threading.Tasks;
using LogicLayer.Models;
using LogicLayer.Services;
using LogicLayer.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LogicLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly WebSocketServerConnectionManager _manager;
        private readonly OrderService _orderService;

        public OrdersController(WebSocketServerConnectionManager manager, OrderService orderService)
        {
            _manager = manager;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order orderToCreate)
        {
            await _orderService.CreateOrderAsync(orderToCreate);

            return orderToCreate;
        }
    }
}