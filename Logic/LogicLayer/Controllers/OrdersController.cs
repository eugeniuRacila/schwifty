using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogicLayer.Models;
using LogicLayer.Services;
using LogicLayer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace LogicLayer.Controllers
{
    [Authorize]
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
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IList<Order>>> GetOrders()
        {
            var receivedOrders = await _orderService.GetOrdersAsync();

            return receivedOrders;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order orderToCreate)
        {
            // Assign customer id to the created order
            orderToCreate.CustomerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("id"))?.Value);

            var createdOrder = await _orderService.CreateOrderAsync(orderToCreate);
            
            // Broadcast order to all drivers
            Package package = new Package("OrderService", "AddOrder", JsonConvert.SerializeObject(createdOrder));
            string jsonPackage = JsonConvert.SerializeObject(package);
        
            foreach (var ws in _manager.GetDriverSockets())
            {
                if (ws.Value.Socket.State == WebSocketState.Open)
                    await ws.Value.Socket.SendAsync(Encoding.UTF8.GetBytes(jsonPackage), WebSocketMessageType.Text, true, CancellationToken.None);
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
        [Route("take-order/{orderId:int}")]
        public async Task<ActionResult<Order>> TakeOrder(int orderId)
        {
            var driverId = int.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("id"))?.Value);
            
            Console.WriteLine($"OrdersController -> takeOrder : {orderId}");
            
            // Get the order from data layer
            var client = new RestClient("http://localhost:8080/");
            var request = new RestRequest($"orders/{orderId}", Method.GET) {RequestFormat = DataFormat.Json};

            var response = await client.ExecuteAsync(request);
            
            Order foundOrder = JsonConvert.DeserializeObject<Order>(response.Content);
            
            var takenOrder = await _orderService.TakeOrderAsync(foundOrder, driverId);

            // Broadcast order to all drivers
            Package package = new Package("OrderService", "OrderTaken", JsonConvert.SerializeObject(takenOrder));
            string jsonPackage = JsonConvert.SerializeObject(package);
            
            foreach (var ws in _manager.GetDriverSockets())
            {
                if (ws.Value.Socket.State == WebSocketState.Open)
                    await ws.Value.Socket.SendAsync(Encoding.UTF8.GetBytes(jsonPackage), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            
            return takenOrder;
        }
        
        [HttpPost]
        [Route("{orderId:int}/nextStatus")]
        public async Task<ActionResult<Order>> NextOrderStatus([FromBody] Order order, int orderId)
        {
            int driverId = int.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("id"))?.Value);
            order.DriverId = driverId;
            _orderService.NextOrderStatusAsync(order);
            // var jsonOrder = JsonConvert.SerializeObject(order);
            // var client = new RestClient("http://localhost:8080/");
            // var request = new RestRequest("orders/update", Method.POST) {RequestFormat = DataFormat.Json};
            // request.AddJsonBody(jsonOrder);
            // await client.ExecuteAsync(request);
            return order;
        }
    }
}