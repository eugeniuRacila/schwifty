using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LogicLayer.Models;
using LogicLayer.Services;
using LogicLayer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogicLayer.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
     public class CustomersController : Controller
     {
          private readonly IOrderService _orderService = new OrderService();
          
          [HttpPost]
          [Route("orders/active")]
          public async Task<ActionResult<Order>> GetActiveOrder()
          {
               int customerId = int.Parse(User.Claims.FirstOrDefault(c => c.Type.Equals("id"))?.Value);
               var order = await _orderService.GetCustomerActiveOrder(customerId);
               return order;
          }
          
//         private readonly IJwtAuthManager _jwtAuthManager;
//
//         public CustomersController(IJwtAuthManager jwtAuthManager)
//         {
//             _jwtAuthManager = jwtAuthManager;
//         }
//         
//         [AllowAnonymous]
//         [HttpPost("login")]
//         public ActionResult Login([FromBody] LoginRequest request)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest();
//             }
//
//             // if (!_userService.IsValidUserCredentials(request.UserName, request.Password))
//             // {
//             //     return Unauthorized();
//             // }
//
//             // var role = _userService.GetUserRole(request.UserName);
//             // var claims = new[]
//             // {
//             //     new Claim(ClaimTypes.Name,request.UserName),
//             //     new Claim(ClaimTypes.Role, role)
//             // };
//
//             var jwtResult = _jwtAuthManager.GenerateTokens(request.Email, new Claim[0], DateTime.Now);
//             Console.WriteLine($"User [{request.Email}] logged in the system.");
//             // _logger.LogInformation($"User [{request.UserName}] logged in the system.");
//             return Ok(new LoginResult
//             {
//                 Email = request.Email,
//                 Role = "customer",
//                 AccessToken = jwtResult.AccessToken,
//                 RefreshToken = jwtResult.RefreshToken.TokenString
//             });
//         }
//     }
//     
//     public class LoginRequest
//     {
//         [Required]
//         [JsonPropertyName("email")]
//         public string Email { get; set; }
//
//         [Required]
//         [JsonPropertyName("password")]
//         public string Password { get; set; }
//     }
//     
//     public class LoginResult
//     {
//         [JsonPropertyName("email")]
//         public string Email { get; set; }
//
//         [JsonPropertyName("role")]
//         public string Role { get; set; }
//
//         [JsonPropertyName("accessToken")]
//         public string AccessToken { get; set; }
//
//         [JsonPropertyName("refreshToken")]
//         public string RefreshToken { get; set; }
     }
}