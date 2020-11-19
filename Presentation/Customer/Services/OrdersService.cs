using System;
using System.Collections;
using System.Collections.Generic;
using Customer.Models;

namespace Customer.Services
{
    public class OrdersService
    {
        private IList<Message> _list;
        public Action<IList<Message>> OrdersUpdate;

        public OrdersService()
        {
            _list = new List<Message>();
            _list.Add(new Message("Initial message"));
        }

        public void AddMessage(Message message)
        {
            _list.Add(message);
            OrdersUpdate?.Invoke(GetAllOrders());
        }

        public IList<Message> GetAllOrders()
        {
            return new List<Message>(_list);
        }
    }
}