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
     public class CustomersController : Controller
     {
          private readonly IOrderService _orderService;

          public CustomersController(IOrderService orderService)
          {
               _orderService = orderService;
          }
          
          [HttpPost]
          [Route("orders/active")]
          public async Task<ActionResult<Order>> GetActiveOrder()
          {
               int customerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("id"))?.Value);
               var order = await _orderService.GetCustomerActiveOrder(customerId);
               Console.WriteLine("active order: "+order);
               return order;
          }
     }
}