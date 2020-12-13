using System.Collections.Generic;
using Driver.Services.order;


namespace Driver.Services.hub
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