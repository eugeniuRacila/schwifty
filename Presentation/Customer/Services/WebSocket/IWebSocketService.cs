using System.Threading.Tasks;

namespace Customer.Services
{
    public interface IWebSocketService
    {
        Task InitializeWebSocketsAsync();
    }
}