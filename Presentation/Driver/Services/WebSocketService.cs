using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Driver.Models;
using Newtonsoft.Json;

namespace Driver.Services
{
    public class WebSocketService : IWebSocketService
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions();
        private readonly CancellationTokenSource _disposalTokenSource = new CancellationTokenSource();
        private readonly ClientWebSocket _webSocket = new ClientWebSocket();
        private readonly ServicesHub _servicesHub;

        private string _socketConnectionId;

        public WebSocketService(ServicesHub servicesHub)
        {
            _servicesHub = servicesHub;
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

                    IMutualService foundService = _servicesHub.Services[deserializedPackage.Service];
                    
                    if (foundService != null)
                    {
                        var method = foundService.GetType().GetMethod(deserializedPackage.Action);

                        if (method != null)
                        {
                            Console.WriteLine($"Invoking the service: {deserializedPackage.Service} on the {deserializedPackage.Action}, with: {deserializedPackage.Payload} parameters.");
                        
                            method.Invoke(foundService, new object[] { deserializedPackage.Payload });
                        }
                        else
                        {
                            Console.WriteLine($"[ERROR] Service {deserializedPackage.Service} doesn't have a method with the name of {deserializedPackage.Action}");
                            throw new Exception();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[ERROR] Service {deserializedPackage.Service} was not found in the servies hub");
                        throw new Exception();
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