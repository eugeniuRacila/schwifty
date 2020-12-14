using System.Net.WebSockets;

namespace LogicLayer.Models
{
    public class WebSocketConnection
    {
        public string SocketConnectionId { get; set; }
        public int UserId { get; set; }
        public WebSocket Socket { get; set; }
    }
}