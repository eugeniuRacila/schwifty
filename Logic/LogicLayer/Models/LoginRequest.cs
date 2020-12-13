using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LogicLayer.Models
{
    public class LoginRequest
    {
        [Required]
        [JsonProperty("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("password")]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}