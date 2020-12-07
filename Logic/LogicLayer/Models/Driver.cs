using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LogicLayer.Models
{
    public class Driver
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string DriverId { get; set; }
        
        [JsonProperty("firstName")]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        
        [JsonProperty("lastName")]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        
        [JsonProperty("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [JsonProperty("phoneNumber")]
        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }
        
        [JsonProperty("password")]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonProperty("createdOn")]
        [JsonPropertyName("createdOn")]
        public long CreatedOn { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}