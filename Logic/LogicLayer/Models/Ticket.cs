using System.Runtime.Serialization;

namespace LogicLayer.Models
{
    public class Ticket
    {
        [DataMember(Name = "textMessageTest")]
        public string TextMessageTest { get; set; }
    }
}