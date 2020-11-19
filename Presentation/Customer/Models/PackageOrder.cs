using System;
using Customer.Services;

namespace Customer.Models
{
    public class PackageOrder
    {
        private readonly OrdersService _ordersService;

        public PackageOrder(OrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        public static void AddOrder(string jsonPayload)
        {
            Console.WriteLine("he did it!");
            Message message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(jsonPayload);
            
            // _ordersService.AddMessage(new Message {Text = "test"});
        }
    }
}