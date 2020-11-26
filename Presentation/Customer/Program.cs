using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Customer.Services;
using Customer.Services.Order;

namespace Customer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001") });
            
            
            builder.Services.AddSingleton<AbstractOrderService, OrderService>();
            builder.Services.AddSingleton<ServicesHub>();
            
            // WebSockets injection
            builder.Services.AddSingleton<IWebSocketService, WebSocketService>();
            
            await builder.Build().RunAsync();
        }
    }
}
