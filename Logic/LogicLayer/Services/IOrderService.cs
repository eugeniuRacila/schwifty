using System.Collections.Generic;
using System.Threading.Tasks;
using LogicLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace LogicLayer.Services
{
    public interface IOrderService
    {
         Task<Order> CreateOrderAsync(Order orderToCreate);
         Task<Order> TakeOrderAsync(Order order, int driverId);
        Task<List<Order>> GetOrdersAsync();

        void NextOrderStatusAsync(Order order);
        Task<ActionResult<Order>> GetCustomerActiveOrder(int customerId);
        Task<ActionResult<Order>> GetDriverActiveOrder(int driverId);
    }
}