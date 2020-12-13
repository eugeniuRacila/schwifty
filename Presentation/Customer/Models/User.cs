using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Customer.Models
{
    public class User
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonProperty("Email")]
        [JsonPropertyName("Email")]
        public string Email { get; set; }
                
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