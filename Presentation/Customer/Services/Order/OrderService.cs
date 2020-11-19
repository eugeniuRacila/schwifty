using System;
using System.Collections.Generic;
using Customer.Models;
using Newtonsoft.Json;

namespace Customer.Services.Order
{
    public class OrderService : IOrderService
    {
        private IList<Message> _list;
        public Action<IList<Message>> OrdersUpdate;

        public OrderService()
        {
            _list = new List<Message>();
            _list.Add(new Message("Initial message"));
        }

        public void AddMessage(string jsonPayload)
        {
            var message = JsonConvert.DeserializeObject<Message>(jsonPayload);
            _list.Add(message);
            OrdersUpdate?.Invoke(GetAllOrders());
        }

        public IList<Message> GetAllOrders()
        {
            return new List<Message>(_list);
        }
    }
}