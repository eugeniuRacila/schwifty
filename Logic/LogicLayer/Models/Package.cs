using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LogicLayer.Models
{
    public class Package<T>
    {
        // change to ENUMS
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("payload")]
        public T Payload { get; set; }

        public Package(string type, T payload)
        {
            Type = type;
            Payload = payload;
        }
    }
}