﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Driver.Models;
using Newtonsoft.Json;

namespace Driver.Services.order
{
    public class OrderService : AbstractOrderService
    {
        private IList<Order> _list;

        public override void AddOrder(string jsonPayload)
        {
            Console.WriteLine($"OrderService -> AddOrder : {jsonPayload}");

            var orderToCreate = JsonConvert.DeserializeObject<Order>(jsonPayload);
            
            // Call Data Layer and store the order into database
            
            _list.Add(orderToCreate);
            
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

        // public override void TakeOrder(Order order)
        // {
        //     _httpService.Patch<Order>("api/orders/take-order/" + order.OrderId, order);
        //     _list.Remove(order);
        // }
    }
}