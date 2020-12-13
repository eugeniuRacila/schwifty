using Newtonsoft.Json;

namespace LogicLayer.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(Customer customer, string jwtToken, string refreshToken)
        {
            Id = customer.Id;
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Email = customer.Email;
            
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}