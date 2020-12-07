using System.Collections.Generic;
using Driver.Services.order;
using Driver.Services.user;


namespace Driver.Services.hub
{
    public class ServicesHub
    {
        public Dictionary<string, IMutualService> Services = new Dictionary<string, IMutualService>();

        public ServicesHub(AbstractOrderService orderService, AbstractUserService driverService)
        {
            Services.Add("OrderService", orderService);
            Services.Add("UserService", driverService);
        }
    }
}