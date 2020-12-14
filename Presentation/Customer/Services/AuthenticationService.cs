using System.Threading.Tasks;
using Customer.Models;
using Microsoft.AspNetCore.Components;

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
        
        public User User { get; private set; }
        
        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            IWebSocketService webSocketService
        ) {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _webSocketService = webSocketService;
        }
        
        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<User>("user");
            
            // Initialize web sockets connection
            if (User != null)
                await _webSocketService.InitializeWebSocketsAsync(User.Id);
        }

        public async Task Login(string email, string password)
        {
            User = await _httpService.Post<User>("/api/auth/customer/login", new { email, password });
            await _localStorageService.SetItem("user", User);
            
            // Initialize web sockets connection
            await _webSocketService.InitializeWebSocketsAsync(User.Id);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}