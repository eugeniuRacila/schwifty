using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LogicLayer.Models
{
    public class Ticket
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}