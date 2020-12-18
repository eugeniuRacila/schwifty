using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Driver.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Driver.Services.order
{
    public abstract class AbstractOrderService : IMutualService
    {
        public Action<IList<Order>> OrdersUpdate;
        public Order ActiveOrder { get; set; }

        public bool hasActiveOrder()
        {
            return ActiveOrder != null;
        }

        public abstract void AddOrder(string jsonPayload);
        public abstract Task TakeOrder(Order order);
        public abstract void InitializeOrdersPool(IList<Order> orders);
        public abstract IList<Order> GetAllOrders();
        
        public abstract Task UpdMyActiveOrder();

        public abstract Task NextOrderStatus(Order activeOrder);

        public abstract void OrderTaken(string jsonPayload);
    }

    public class OrderService : AbstractOrderService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private IList<Order> _list;

        public OrderService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _list = new List<Order>();
        }

        public override void AddOrder(string jsonPayload)
        {
            Console.WriteLine($"OrderService -> AddOrder : {jsonPayload}");

            var orderToCreate = JsonConvert.DeserializeObject<Order>(jsonPayload);

            // Call Data Layer and store the order into database
            if (!_list.Contains(orderToCreate))
            {
                _list.Add(orderToCreate);    
            }
            
            // Rerender HTML observer
            OrdersUpdate?.Invoke(GetAllOrders());
        }

        public override void InitializeOrdersPool(IList<Order> orders)
        {
            _list = orders;

            // Rerender HTML observer
            OrdersUpdate?.Invoke(GetAllOrders());
        }

        public override IList<Order> GetAllOrders()
        {
            return new List<Order>(_list);
        }

        public override async Task UpdMyActiveOrder()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var httpService = scope.ServiceProvider.GetService<IHttpService>();
                ActiveOrder = await httpService.Post<Order>("/api/drivers/orders/active", new {});
            }

            Console.WriteLine("Active order: " + ActiveOrder);
        }

        public override async Task NextOrderStatus(Order activeOrder)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var httpService = scope.ServiceProvider.GetService<IHttpService>();
                await httpService.Post<Order>($"api/orders/{activeOrder.Id}/nextStatus", activeOrder);
            }
        }

        public override void OrderTaken(string payload)
        {
            Order order = JsonConvert.DeserializeObject<Order>(payload);
            _list.Remove(order);
        }

        public override async Task TakeOrder(Order order)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            { 
                var httpService = scope.ServiceProvider.GetService<IHttpService>();
                await httpService.Patch<Order>("api/orders/take-order/" + order.Id, order);
                _list.Remove(order);
            }
        }
    }
}