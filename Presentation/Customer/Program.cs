using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Customer.Services;

namespace Customer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });
            
            builder.Services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<ILocalStorageService, LocalStorageService>()
                .AddSingleton<AbstractOrderService, OrderService>()
                .AddSingleton<ServicesHub>()
                .AddSingleton<IWebSocketService, WebSocketService>();
            
            var host = builder.Build();

            var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
            await authenticationService.Initialize();
            
            await host.RunAsync();
        }
    }
}
