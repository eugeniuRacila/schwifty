using System.Collections.Generic;

namespace Driver.Services
{
    public class ServicesHub
    {
        public Dictionary<string, IMutualService> Services = new Dictionary<string, IMutualService>();

        public ServicesHub(AbstractOrderService orderService)
        {
            Services.Add("OrderService", orderService);
        }
    }
}