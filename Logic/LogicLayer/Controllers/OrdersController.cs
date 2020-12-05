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
        private readonly IOrderService _orderService;

        public OrdersController(WebSocketServerConnectionManager manager, IOrderService orderService)
        {
            _manager = manager;
            _orderService = orderService;
        }
        

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order orderToCreate)
        {
            Console.WriteLine($"OrdersController -> orderToCreate : {orderToCreate}");
            
            var createdOrder = await _orderService.CreateOrderAsync(orderToCreate);

            // Broadcast order to all drivers
            Package package = new Package("OrderService", "AddOrder", JsonConvert.SerializeObject(createdOrder));
            string jsonPackage = JsonConvert.SerializeObject(package);
            
            foreach (var sock in _manager.GetDriverSockets())
            {
                if (sock.Value.State == WebSocketState.Open)
                    await sock.Value.SendAsync(Encoding.UTF8.GetBytes(jsonPackage), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            
            return createdOrder;
        }
        
        [HttpPatch]
        public async Task<ActionResult<Order>> TakeOrder([FromBody] Order order, int driverId)
        {
            Console.WriteLine($"OrdersController -> takeOrder : {order}");
            
            var takenOrder = await _orderService.TakeOrderAsync(order, driverId);

            // Broadcast order to all drivers
            Package package = new Package("OrderService", "UpdateOrder", JsonConvert.SerializeObject(takenOrder));
            string jsonPackage = JsonConvert.SerializeObject(package);
            
            foreach (var sock in _manager.GetDriverSockets())
            {
                if (sock.Value.State == WebSocketState.Open)
                    await sock.Value.SendAsync(Encoding.UTF8.GetBytes(jsonPackage), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            
            return takenOrder;
        }
    }
}