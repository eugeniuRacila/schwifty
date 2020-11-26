using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace LogicLayer.Utils
{
    public class WebSocketServerConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _customrSockets = new ConcurrentDictionary<string, WebSocket>();
        private ConcurrentDictionary<string, WebSocket> _driverSockets = new ConcurrentDictionary<string, WebSocket>();

        public string AddSocket(WebSocket socket)
        {
            string connId = Guid.NewGuid().ToString();
            _customrSockets.TryAdd(connId, socket);
            Console.WriteLine("WebSocketServerConnectionManager-> AddSocket: WebSocket added with ID: " + connId);
            
            return connId;
        }

        public string AddDriverSocket(WebSocket socket)
        {
            string connId = Guid.NewGuid().ToString();
            _driverSockets.TryAdd(connId, socket);

            Console.WriteLine($"[WS] WebSocketServerConnectionManager -> AddDriverSocket :: connection ID: {connId}");

            return connId;
        }

        public ConcurrentDictionary<string, WebSocket> GetCustomerSockets()
        {
            return _customrSockets;
        }

        public ConcurrentDictionary<string, WebSocket> GetDriverSockets()
        {
            return _driverSockets;
        }
    }
}