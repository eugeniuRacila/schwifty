using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogicLayer.Models;
using LogicLayer.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LogicLayer.Middlewares
{
    public class WebSocketServerMiddleware
    {
        private readonly RequestDelegate _next;

        private WebSocketServerConnectionManager _manager;

        public WebSocketServerMiddleware(RequestDelegate next, WebSocketServerConnectionManager manager)
        {
            _next = next;
            _manager = manager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                
                bool isDriver = false;

                var userId = int.Parse(context.Request.Query["id"]);

                if (context.Request.Path.ToString().ToLower() == "/driver")
                {
                    _manager.AddDriverSocket(userId, webSocket);
                    isDriver = true;
                    Console.WriteLine($"New socket (driver) connection established (with user id: {userId})");
                }
                else
                {
                    _manager.AddSocket(userId, webSocket);
                    Console.WriteLine($"New socket (customer) connection established (with user id: {userId})");
                }
                
                //Send ConnID Back
                // await SendConnID(webSocket, conn);

                await Receive(webSocket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        Console.WriteLine("Receive->Text");
                        Console.WriteLine($"Message: {Encoding.UTF8.GetString(buffer, 0, result.Count)}");
                        
                        await RouteJSONMessageAsync(Encoding.UTF8.GetString(buffer, 0, result.Count));
                    } 
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        WebSocketConnection webSocketConnection;
                        
                        if (isDriver)
                        {
                            WebSocketConnection foundWebSocket = _manager.GetDriverSockets().FirstOrDefault(s => s.Value.Socket == webSocket).Value;
                            _manager.GetDriverSockets().TryRemove(foundWebSocket.SocketConnectionId, out webSocketConnection);
                        }
                        else
                        {
                            WebSocketConnection foundWebSocket = _manager.GetCustomerSockets().FirstOrDefault(s => s.Value.Socket == webSocket).Value;
                            _manager.GetCustomerSockets().TryRemove(foundWebSocket.SocketConnectionId, out webSocketConnection);
                        }
                        
                        Console.WriteLine($"Socket connection closed -> close for user id: {webSocketConnection?.UserId}");

                        await webSocketConnection.Socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                    }
                });
            }
            else
            {
                Console.WriteLine("Not a websocket request");
                await _next(context);
            }
        }

        private async Task RouteJSONMessageAsync(string message)
        {
            var routeOb = JsonConvert.DeserializeObject<dynamic>(message);
            
            Console.WriteLine("To: " + routeOb.To);
            Guid guidOutput;

            if (Guid.TryParse(routeOb.To.ToString(), out guidOutput))
            {
                Console.WriteLine("Targeted");
                var sock = _manager.GetCustomerSockets().FirstOrDefault(s => s.Key == routeOb.To.ToString());
                if (sock.Value != null)
                {
                    if (sock.Value.Socket.State == WebSocketState.Open)
                        await sock.Value.Socket.SendAsync(Encoding.UTF8.GetBytes(routeOb.Message.ToString()), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else
                {
                    Console.WriteLine("Invalid Recipient");
                }
            }
            else
            {
                Console.WriteLine("Broadcast");
                foreach (var sock in _manager.GetCustomerSockets())
                {
                    if (sock.Value.Socket.State == WebSocketState.Open)
                        await sock.Value.Socket.SendAsync(Encoding.UTF8.GetBytes(routeOb.Message.ToString()), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
        
        private async Task SendConnID(WebSocket socket, string connID)
        {
            var buffer = Encoding.UTF8.GetBytes("ConnID: " + connID);
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                       cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}