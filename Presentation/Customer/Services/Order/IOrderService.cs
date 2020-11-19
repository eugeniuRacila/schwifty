using System;
using System.Collections.Generic;
using Customer.Models;

namespace Customer.Services.Order
{
    public interface IOrderService
    {
        void AddMessage(string jsonPayload);
        IList<Message> GetAllOrders();
    }
}