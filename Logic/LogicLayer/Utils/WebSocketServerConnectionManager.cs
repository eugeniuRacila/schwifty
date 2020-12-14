using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using LogicLayer.Models;

namespace LogicLayer.Utils
{
    public class WebSocketServerConnectionManager
    {
        private ConcurrentDictionary<string, WebSocketConnection> _customrSockets = new ConcurrentDictionary<string, WebSocketConnection>();
        private ConcurrentDictionary<string, WebSocketConnection> _driverSockets = new ConcurrentDictionary<string, WebSocketConnection>();

        public string AddSocket(int userId, WebSocket socket)
        {
            string connId = Guid.NewGuid().ToString();
            _customrSockets.TryAdd(connId, new WebSocketConnection {SocketConnectionId = connId, UserId = userId, Socket = socket});
            Console.WriteLine("WebSocketServerConnectionManager-> AddSocket: WebSocket added with ID: " + connId);
            
            return connId;
        }

        public string AddDriverSocket(int userId, WebSocket socket)
        {
            string connId = Guid.NewGuid().ToString();
            _driverSockets.TryAdd(connId, new WebSocketConnection {SocketConnectionId = connId, UserId = userId, Socket = socket});

            Console.WriteLine($"[WS] WebSocketServerConnectionManager -> AddDriverSocket :: connection ID: {connId}");

            return connId;
        }

        public ConcurrentDictionary<string, WebSocketConnection> GetCustomerSockets()
        {
            return _customrSockets;
        }

        public ConcurrentDictionary<string, WebSocketConnection> GetDriverSockets()
        {
            return _driverSockets;
        }
    }
}