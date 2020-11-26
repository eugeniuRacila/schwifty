using System;
using System.Collections.Generic;
using Customer.Models;

namespace Customer.Services.Order
{
    public interface IOrderService
    {
        Action<IList<Message>> GetOrdersObservable();
        void AddMessage(string jsonPayload);
        IList<Message> GetAllOrders();
    }
}