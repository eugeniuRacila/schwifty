using System;
using System.Collections.Generic;
using Customer.Models;

namespace Customer.Services.Order
{
    public abstract class AOrderService : IMutualService
    {
        public Action<IList<Message>> OrdersUpdate;

        public abstract void AddMessage(string jsonPayload);
        public abstract IList<Message> GetAllOrders();
    }
}