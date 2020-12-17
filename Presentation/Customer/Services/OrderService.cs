using System;
using System.Threading.Tasks;
using Customer.Models;
using Driver.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Services
{
    public abstract class AbstractOrderService : IMutualService
    {
        public Order ActiveOrder { get; set; }
        
        public bool hasActiveOrder()
        {
            return ActiveOrder != null;
        }

        public abstract Task UpdMyActiveOrder();
    }
    
    public class OrderService : AbstractOrderService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OrderService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        public override async Task UpdMyActiveOrder()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var httpService = scope.ServiceProvider.GetService<IHttpService>();
                ActiveOrder = await httpService.Post<Order>("/api/customers/orders/active", new {});
            }

            Console.WriteLine("Active order: " + ActiveOrder);
        }
        
    }
}