using System;
using Customer.Services;
using Customer.Services.Order;

namespace Customer.Models
{
    public class PackageOrder
    {
        private readonly OrderService _orderService;

        public PackageOrder(OrderService orderService)
        {
            _orderService = orderService;
        }

        public static void AddOrder(string jsonPayload)
        {
            Console.WriteLine("he did it!");
            Message message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(jsonPayload);
            
            // _ordersService.AddMessage(new Message {Text = "test"});
        }
    }
}