using System.Collections.Generic;
using System.Threading.Tasks;
using LogicLayer.Models;

namespace LogicLayer.Services
{
    public interface IOrderService
    {
         Task<Order> CreateOrderAsync(Order orderToCreate);
         Task<Order> TakeOrderAsync(Order order, int driverId);
        Task<List<Order>> GetOrdersAsync();

        void NextOrderStatusAsync(Order order);
    }
}