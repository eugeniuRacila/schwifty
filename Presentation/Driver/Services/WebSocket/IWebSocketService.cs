using System.Threading.Tasks;

namespace Driver.Services
{
    public interface IWebSocketService
    {
        Task InitializeWebSocketsAsync(int userId);
    }
}