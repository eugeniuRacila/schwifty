using System;
using System.Threading.Tasks;
using Customer.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace Customer.Services
{
    public interface IAuthenticationService
    {
        User User { get; }
        Task Initialize();
        Task Login(string email, string password);
        Task Logout();
    }
    
    public class AuthenticationService : IAuthenticationService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private readonly IWebSocketService _webSocketService;
        private readonly AbstractOrderService _orderService;

        public User User { get; private set; }
        
        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            IWebSocketService webSocketService,
            AbstractOrderService orderService
        ) {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _webSocketService = webSocketService;
            _orderService = orderService;
        }
        
        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<User>("user");
            
            // Initialize web sockets connection
            if (User != null)
            {
                await _orderService.UpdMyActiveOrder();
                await _webSocketService.InitializeWebSocketsAsync(User.Id);
                // Order foundActiveOrder = await _orderService.GetActiveOrder();
                // Console.WriteLine($"foundActiveOrder: {JsonConvert.SerializeObject(foundActiveOrder)}");
            }
        }

        public async Task Login(string email, string password)
        {
            User = await _httpService.Post<User>("/api/auth/customer/login", new { email, password });
            await _localStorageService.SetItem("user", User);

            if (User != null)
            {
                Console.WriteLine("user ok");
                // Initialize web sockets connection
                await _webSocketService.InitializeWebSocketsAsync(User.Id);
                _navigationManager.NavigateTo("");
            }

        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}