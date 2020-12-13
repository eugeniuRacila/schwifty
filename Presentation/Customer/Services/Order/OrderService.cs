using System.Threading.Tasks;

namespace Customer.Services.Order
{
    /// <summary>
    /// We will need it in the future
    /// </summary>
    public class OrderService : AbstractOrderService
    {
        // private readonly IHttpService _httpService;
        //
        // public OrderService(IHttpService httpService)
        // {
        //     _httpService = httpService;
        // }
        //
        // public override async Task<Models.Order> CreateOrderAsync(Models.Order orderToCreate)
        // {
        //     Models.Order createdOrder = await _httpService.Post<Models.Order>("/api/orders", orderToCreate);
        //
        //     return createdOrder;
        // }
    }
}