using System.Threading.Tasks;

namespace Customer.Services.Order
{
    /// <summary>
    /// We will need it in the future
    /// </summary>
    public class OrderService : AbstractOrderService
    {
        //
        // public override async Task<Models.Order> CreateOrderAsync(Models.Order orderToCreate)
        // {
        //     Models.Order createdOrder = await _httpService.Post<Models.Order>("/api/orders", orderToCreate);
        //
        //     return createdOrder;
        // }

        // public async Task<Models.Order> GetMyActiveOrder()
        // {
        //     Models.Order activeOrder = await _httpService.Get<Models.Order>("/api/customers/orders/active");
        //     return activeOrder;
        // }
    }
}