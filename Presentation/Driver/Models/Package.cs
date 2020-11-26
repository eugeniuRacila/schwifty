using System.Text.Json.Serialization;

namespace Driver.Models
{
    public class Package
    {
        [JsonPropertyName("service")]
        public string Service { get; set; } // change to ENUMS
        
        [JsonPropertyName("action")]
        public string Action { get; set; } // change to ENUMS
        
        [JsonPropertyName("payload")]
        public string Payload { get; set; }

        public Package()
        {
            // ...
        }

        public Package(string service, string action, string payload)
        {
            Service = service;
            Action = action;
            Payload = payload;
        }
    }
}