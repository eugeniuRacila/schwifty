using System.Text.Json.Serialization;

namespace Customer.Models
{
    public class Message
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        public Message()
        {
            // ...
        }

        public Message(string text)
        {
            Text = text;
        }
    }
}