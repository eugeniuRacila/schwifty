using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LogicLayer.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LogicLayer.Services
{
    public class OrderService : IOrderService
    {
        public async Task<List<Order>> GetOrdersAsync()
        {
            var client = new RestClient("http://localhost:8080/");
            var request = new RestRequest("orders", Method.GET);
            
            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<List<Order>>(response.Content);
        }
        
        public async Task<Order> CreateOrderAsync(Order orderToCreate)
        {
            var jsonOrder = JsonConvert.SerializeObject(orderToCreate);
            var client = new RestClient("http://localhost:8080/");
            var request = new RestRequest("orders", Method.POST) {RequestFormat = DataFormat.Json};

            request.AddJsonBody(jsonOrder);
            
            var response = await client.ExecuteAsync(request);

            Console.WriteLine($"OrderService -> CreateOrderAsync : {response.Content}");
            
            return JsonConvert.DeserializeObject<Order>(response.Content);
        }

        public async Task<Order> TakeOrderAsync(Order order, int driverId)
        {
            var jsonOrder = JsonConvert.SerializeObject(order);
            var client = new RestClient("http://localhost:8080/");
            var request = new RestRequest("orders", Method.PATCH) {RequestFormat = DataFormat.Json};
            
            request.AddJsonBody(jsonOrder);
            
            var response = await client.ExecuteAsync(request);

            Console.WriteLine($"OrderService -> TakeOrderAsync : {response.Content}");
            
            return JsonConvert.DeserializeObject<Order>(response.Content);
        }

        public void NextOrderStatusAsync(Order order)
        {
            order.NextStatus();
            var jsonOrder = JsonConvert.SerializeObject(order);

            var client = new RestClient("http://localhost:8080/");
            var request = new RestRequest("orders/update", Method.POST) {RequestFormat = DataFormat.Json};

            request.AddJsonBody(jsonOrder);
            
            client.ExecuteAsync(request);
        }
    }
}