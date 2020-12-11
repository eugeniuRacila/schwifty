using System.Threading.Tasks;
using Driver.Models;

namespace Driver.Services.authentication
{
    public interface IAuthenticationService
    {
        User User { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }
}