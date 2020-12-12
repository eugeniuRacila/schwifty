using Newtonsoft.Json;

namespace LogicLayer.Models
{
    public class DriverAuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public DriverAuthenticateResponse(Driver driver, string jwtToken, string refreshToken)
        {
            Id = driver.Id;
            FirstName = driver.FirstName;
            LastName = driver.LastName;
            Email = driver.Email;
            PhoneNumber = driver.PhoneNumber;
            
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}