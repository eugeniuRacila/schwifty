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
    public class DriversController : Controller
    {
        private readonly WebSocketServerConnectionManager _manager;
        private readonly IDriverService _orderService;

        public DriversController(WebSocketServerConnectionManager manager, IDriverService orderService)
        {
            _manager = manager;
            _orderService = orderService;
        }
        
        [HttpPost]
        public async Task<ActionResult<Driver>> CreateOrder([FromBody] Driver driverToCreate)
        {
            Console.WriteLine($"DriversController -> driverToCreate : {driverToCreate}");
            
            var createdDriver = await _orderService.CreateDriverAsync(driverToCreate);

            // Broadcast order to all drivers
            Package package = new Package("DriverService", "RegisterUser", JsonConvert.SerializeObject(createdDriver));
            string jsonPackage = JsonConvert.SerializeObject(package);
            
            foreach (var sock in _manager.GetDriverSockets())
            {
                if (sock.Value.State == WebSocketState.Open)
                    await sock.Value.SendAsync(Encoding.UTF8.GetBytes(jsonPackage), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            
            return createdDriver;
        }
    }
}