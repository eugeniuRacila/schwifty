using System.Collections.Generic;
using System.Threading.Tasks;
using LogicLayer.Models;

namespace LogicLayer.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order> CreateOrderAsync(Order orderToCreate);
    }
}