using System.Threading.Tasks;
using LogicLayer.Models;
using RestSharp;

namespace LogicLayer.Services
{
    public class OrderService
    {
        public async Task<Order> CreateOrderAsync(Order orderToCreate)
        {
            return orderToCreate;
        }
    }
}