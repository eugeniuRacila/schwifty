using System;
using System.Collections.Generic;
using Driver.Models;
using Newtonsoft.Json;

namespace Driver.Services
{
    public class OrderService : AbstractOrderService
    {
        private IList<Order> _list;

        public OrderService()
        {
            _list = new List<Order>();
        }

        public override void AddOrder(string jsonPayload)
        {
            Console.WriteLine($"OrderService -> AddOrder : {jsonPayload}");

            var orderToCreate = JsonConvert.DeserializeObject<Order>(jsonPayload);
            
            // Call Data Layer and store the order into database
            
            _list.Add(orderToCreate);
            
            OrdersUpdate?.Invoke(GetAllOrders());
        }

        public override IList<Order> GetAllOrders()
        {
            return new List<Order>(_list);
        }
    }
}