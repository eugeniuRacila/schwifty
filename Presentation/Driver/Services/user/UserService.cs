using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Driver.Models;


namespace Driver.Services.user
{
    public class UserService : AbstractUserService
    {
        
        private HttpClient _httpClient;
        
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override async Task<string> RegisterUser(User driver)
        {
            try
            {
                HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync("api/drivers", driver);
                return responseMessage.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}