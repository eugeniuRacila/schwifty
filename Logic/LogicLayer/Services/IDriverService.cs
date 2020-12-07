using System.Threading.Tasks;
using LogicLayer.Models;

namespace LogicLayer.Services
{
    public interface IDriverService
    {
        Task<Driver> CreateDriverAsync(Driver driverToCreate);
    }
}