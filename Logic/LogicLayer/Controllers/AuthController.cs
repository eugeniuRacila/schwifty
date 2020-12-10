using System;
using System.Security.Claims;
using System.Threading.Tasks;
using LogicLayer.Models;
using LogicLayer.Services;
using LogicLayer.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace LogicLayer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IJwtAuthManager _jwtAuthManager;

        public AuthController(IAuthService authService, IJwtAuthManager jwtAuthManager)
        {
            _authService = authService;
            _jwtAuthManager = jwtAuthManager;
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult GetCurrentUser()
        {
            return Ok(new LoginResult
            {
                Email = User.Identity.Name,
                Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty
            });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("customer/register")]
        public async Task<ActionResult<Customer>> CustomerRegister([FromBody] Customer customer)
        {
            Console.WriteLine($"AuthController -> CustomerRegister : {customer}");

            var registeredCustomer = await _authService.CreateCustomerAsync(customer);

            return registeredCustomer;
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("customer/login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            Console.WriteLine("customer/login end-point");
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var jsonLoginRequest = JsonConvert.SerializeObject(loginRequest);
            var client = new RestClient("http://localhost:8080/");
            var request = new RestRequest("auth/customer/login", Method.POST) {RequestFormat = DataFormat.Json};

            request.AddJsonBody(jsonLoginRequest);
            
            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var jwtResult = _jwtAuthManager.GenerateTokens(loginRequest.Email, new Claim[0], DateTime.Now);
                Console.WriteLine($"User [{loginRequest.Email}] logged in the system.");
                return Ok(new LoginResult
                {
                    Email = loginRequest.Email,
                    Role = "customer",
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }

            return Unauthorized();
            // if (!_userService.IsValidUserCredentials(request.UserName, request.Password))
            // {
            //     return Unauthorized();
            // }

            // var role = _userService.GetUserRole(request.UserName);
            // var claims = new[]
            // {
            //     new Claim(ClaimTypes.Name,request.UserName),
            //     new Claim(ClaimTypes.Role, role)
            // };


        }
    }
}