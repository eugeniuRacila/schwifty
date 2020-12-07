using System;
using System.Threading.Tasks;
using LogicLayer.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LogicLayer.Services
{
    public class DriverService : IDriverService
    {
        public async Task<Driver> CreateDriverAsync(Driver driverToCreate)
        {
            var jsonOrder = JsonConvert.SerializeObject(driverToCreate);
            var client = new RestClient("http://localhost:8080/");
            var request = new RestRequest("drivers", Method.POST) {RequestFormat = DataFormat.Json};

            request.AddJsonBody(jsonOrder);
            
            var response = await client.ExecuteAsync(request);

            Console.WriteLine($"OrderService -> CreateOrderAsync : {response.Content}");
            
            return JsonConvert.DeserializeObject<Driver>(response.Content);
        }
    }
}