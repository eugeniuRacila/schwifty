using System.Threading.Tasks;
using Customer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Services
{
    public abstract class AbstractOrderService : IMutualService
    {
        public Order ActiveOrder { get; set; }
        
        public abstract Task<Order> GetActiveOrder();
    }
    
    /// <summary>
    /// We will need it in the future
    /// </summary>
    public class OrderService : AbstractOrderService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OrderService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        
        public override async Task<Order> GetActiveOrder()
        {
            Order foundActiveOrder;
            
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var httpService = scope.ServiceProvider.GetService<IHttpService>();

                foundActiveOrder = await httpService.Get<Order>("/api/customers/orders/active");
            }

            ActiveOrder = foundActiveOrder;

            return foundActiveOrder;
        }

        // public override async Task<Models.Order> CreateOrderAsync(Models.Order orderToCreate)
        // {
        //     Models.Order createdOrder = await _httpService.Post<Models.Order>("/api/orders", orderToCreate);
        //
        //     return createdOrder;
        // }

        // public async Task<Models.Order> GetMyActiveOrder()
        // {
        //     Models.Order activeOrder = await _httpService.Get<Models.Order>("/api/customers/orders/active");
        //     return activeOrder;
        // }
    }
}