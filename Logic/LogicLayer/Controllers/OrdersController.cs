using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogicLayer.Models;
using LogicLayer.Services;
using LogicLayer.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            Console.WriteLine($"OrdersController -> orderToCreate : {orderToCreate}");
            
            await _orderService.CreateOrderAsync(orderToCreate);
            
            
            // Broadcast order to all drivers
            Package package = new Package("OrderService", "AddOrder", JsonConvert.SerializeObject(orderToCreate));
            string jsonPackage = JsonConvert.SerializeObject(package);
            
            foreach (var sock in _manager.GetDriverSockets())
            {
                if (sock.Value.State == WebSocketState.Open)
                    await sock.Value.SendAsync(Encoding.UTF8.GetBytes(jsonPackage), WebSocketMessageType.Text, true, CancellationToken.None);
            }

            return orderToCreate;
        }
    }
}