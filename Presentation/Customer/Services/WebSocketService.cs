using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Customer.Models;
using Customer.Services.Order;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Customer.Services
{
    public class WebSocketService : IWebSocketService
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions();
        private readonly CancellationTokenSource _disposalTokenSource = new CancellationTokenSource();
        private readonly ClientWebSocket _webSocket = new ClientWebSocket();
        private readonly IOrderService _orderService;

        private string _socketConnectionId;

        public WebSocketService(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        public async Task InitializeWebSocketsAsync()
        {
            await _webSocket.ConnectAsync(new Uri("wss://localhost:5001/"), _disposalTokenSource.Token);

            if (_webSocket.State == WebSocketState.Open)
                Console.WriteLine("Successfully connected to server (websocket)");
            else
                Console.WriteLine("An error happened when establishing websocket connection");

            _ = ReceiveLoop();
        }

        async Task SendMessageAsync()
        {
            var dataToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Hello, websocket!"));
            await _webSocket.SendAsync(dataToSend, WebSocketMessageType.Text, true, _disposalTokenSource.Token);
        }

        async Task ReceiveLoop()
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            
            while (!_disposalTokenSource.IsCancellationRequested)
            {
                var received = await _webSocket.ReceiveAsync(buffer, _disposalTokenSource.Token);
                var json = Encoding.UTF8.GetString(buffer.Array ?? throw new NullReferenceException(), 0, received.Count);

                Console.WriteLine($"Received a message from server, via socket: {json}");
                
                try
                {
                    var deserializedPackage = JsonConvert.DeserializeObject<Package>(json);
                    
                    if (deserializedPackage.Service == "OrdersService")
                    {
                        var method = _orderService.GetType().GetMethod(deserializedPackage.Action);
                        Console.WriteLine($"var method = {method}");
                        Console.WriteLine($"argument = {deserializedPackage.Payload}");
                        method?.Invoke(_orderService, new object[] { deserializedPackage.Payload });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                // Console.WriteLine($"type of package:: {type[3]}");

                Console.WriteLine($"After call");
                // how to notify other pages when server send any information ???
            }
        }
        
        public void Dispose()
        {
            Console.WriteLine("Client closed sockets connection");
            _disposalTokenSource.Cancel();
            _ = _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Bye", CancellationToken.None);
        }
    }
}