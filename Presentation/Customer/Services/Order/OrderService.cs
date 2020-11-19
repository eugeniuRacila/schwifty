using System;
using System.Collections.Generic;
using Customer.Models;
using Newtonsoft.Json;

namespace Customer.Services.Order
{
    public class OrderService : AOrderService
    {
        private IList<Message> _list;

        public OrderService()
        {
            _list = new List<Message>();
            _list.Add(new Message("Initial message"));
        }

        public override void AddMessage(string jsonPayload)
        {
            Console.WriteLine("AddMessage was called");
            var message = JsonConvert.DeserializeObject<Message>(jsonPayload);
            _list.Add(message);
            OrdersUpdate?.Invoke(GetAllOrders());
        }

        public override IList<Message> GetAllOrders()
        {
            return new List<Message>(_list);
        }
    }
}