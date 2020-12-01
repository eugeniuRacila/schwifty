using System;
using System.Collections.Generic;
using Driver.Models;

namespace Driver.Services
{
    public abstract class AbstractOrderService : IMutualService
    {
        public Action<IList<Order>> OrdersUpdate;

        public abstract void AddOrder(string jsonPayload);
        public abstract void InitializeOrdersPool(IList<Order> orders);
        public abstract IList<Order> GetAllOrders();
    }
}