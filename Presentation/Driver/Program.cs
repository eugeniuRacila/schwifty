using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Driver.Services;
using Driver.Services.authentication;
using Driver.Services.hub;
using Driver.Services.order;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Driver
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(
                sp => new HttpClient {BaseAddress = new Uri("https://localhost:5001")});
            
            builder.Services.AddSingleton<AbstractOrderService, OrderService>();
            builder.Services.AddSingleton<AuthenticationService, AuthenticationService>();
            builder.Services.AddSingleton<ServicesHub>();
            
            // WebSockets injection
            builder.Services.AddSingleton<IWebSocketService, WebSocketService>();

            await builder.Build().RunAsync();
        }
    }
}