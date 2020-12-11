using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
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
        
        [HttpGet]
        public async Task<ActionResult<IList<Order>>> GetOrders()
        {
            var receivedOrders = await _orderService.GetOrdersAsync();

            return receivedOrders;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order orderToCreate)
        {
            
            Console.WriteLine($"OrdersController -> orderToCreate : {orderToCreate}");
            if (!isOrderValid(orderToCreate))
            {
                Console.WriteLine("The data of order:" + orderToCreate.OrderId + " was corrupted. Empty order is returned" );
                return new Order();
            }

            var createdOrder = await _orderService.CreateOrderAsync(orderToCreate);
                //Console.WriteLine("Here: " + createdOrder);
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

        private bool isOrderValid(Order order)
        {
            if (IsNullOrEmpty(order.LocationPoints.StartingAddress))
            {
                return false;
            }

            if (IsNullOrEmpty(order.LocationPoints.DestinationAddress))
            {
                return false;
            }

            if (IsNullOrEmpty(order.TypeOfCar) || !IsCorrectType(order.TypeOfCar))
            {
                return false;
            }

            if (!IsCorrectAmountOfSeats(order.NeededSeats))
            {
                return false;
            }
            return true;
        }
        
        private bool IsNullOrEmpty(string str)
        {
            return str == null || str.Equals("");
        }
        
        private bool IsCorrectType(string type)
        {
            return type.Equals("standard") || type.Equals("comfort") || type.Equals("VIP");
        }

        private bool IsCorrectAmountOfSeats(int seats)
        {
            return seats == 2 || seats == 5 || seats == 8;
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