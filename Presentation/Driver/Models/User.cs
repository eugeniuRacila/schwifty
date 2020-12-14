using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Driver.Models
{
    public class User
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonProperty("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [JsonProperty("firstName")]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("lastName")]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        
        [JsonProperty("phoneNumber")]
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
                
        [JsonProperty("role")]
        [JsonPropertyName("role")]
        public string Role { get; set; }
        
        [JsonProperty("accessToken")]
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
        
        [JsonProperty("refreshToken")]
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}