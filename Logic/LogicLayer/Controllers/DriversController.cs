using System;
using System.Linq;
using System.Threading.Tasks;
using LogicLayer.Models;
using LogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogicLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriversController : Controller
    {
        private readonly IOrderService _orderService;

        public DriversController(IOrderService orderService)
        {
            _orderService = orderService;
        }
          
        [HttpPost]
        [Route("orders/active")]
        public async Task<ActionResult<Order>> GetActiveOrder()
        {
            int driverId = int.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("id"))?.Value);
            Console.WriteLine("driver_id: "+ driverId);
            var order = await _orderService.GetDriverActiveOrder(driverId);
            Console.WriteLine("active order: "+order);
            return order;
        }
    }
}