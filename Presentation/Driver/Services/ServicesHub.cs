using System.Collections.Generic;

namespace Driver.Services
{
    public class ServicesHub
    {
        public Dictionary<string, IMutualService> Services = new Dictionary<string, IMutualService>();

        public ServicesHub(AOrderService orderService)
        {
            Services.Add("OrderService", orderService);
        }
    }
}