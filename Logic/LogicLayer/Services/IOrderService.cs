using System.Threading.Tasks;
using LogicLayer.Models;

namespace LogicLayer.Services
{
    public interface IOrderService
    {
         Task<Order> CreateOrderAsync(Order orderToCreate);
    }
}