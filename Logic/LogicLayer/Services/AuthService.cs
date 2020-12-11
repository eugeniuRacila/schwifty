using System;
using System.Threading.Tasks;
using LogicLayer.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LogicLayer.Services
{
    public interface IAuthService
    {
        Task<Customer> CreateCustomerAsync(Customer customerToCreate);
    }
    
    public class AuthService : IAuthService
    {
        public async Task<Customer> CreateCustomerAsync(Customer customerToCreate)
        {
            var jsonCustomer = JsonConvert.SerializeObject(customerToCreate);
            Console.WriteLine(jsonCustomer);
            var restClient = new RestClient("http://localhost:8080/");
            var restRequest = new RestRequest("auth/customer/register", Method.POST) {RequestFormat = DataFormat.Json};

            restRequest.AddJsonBody(jsonCustomer);
            
            // WHY THE BAD REQUEST REPLY??
            var response = await restClient.ExecuteAsync(restRequest);

            Console.WriteLine($"AuthService -> CreateCustomerAsync : {response.Content}");

            return JsonConvert.DeserializeObject<Customer>(response.Content);
        }
    }
}