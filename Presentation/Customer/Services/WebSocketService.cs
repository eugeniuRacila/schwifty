using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Customer.Models;
using Newtonsoft.Json.Linq;

namespace Customer.Services
{
    public class WebSocketService : IWebSocketService
    {
        private readonly CancellationTokenSource _disposalTokenSource = new CancellationTokenSource();
        private readonly ClientWebSocket _webSocket = new ClientWebSocket();
        private readonly OrdersService _ordersService;

        private string _socketConnectionId;

        public WebSocketService(OrdersService ordersService)
        {
            _ordersService = ordersService;
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
                var receivedAsText = Encoding.UTF8.GetString(buffer.Array ?? throw new NullReferenceException(), 0, received.Count);

                string[] type = receivedAsText.Split('"');
                IList<string> package = type.ToList();

                for (int i = 0; i < type.Length; i++)
                    if (i == 3)
                    {
                        Type magicType = Type.GetType("PackageOrder");
                        
                        Console.WriteLine($"magicType :: {magicType}");

                        
                        ConstructorInfo magicConstructor = magicType.GetConstructor(new Type[] { typeof(OrdersService)});
                        object magicClassObject = magicConstructor.Invoke(new object[]{ _ordersService });
                        MethodInfo magicMethod = magicType.GetMethod("AddOrder");
                        object magicValue = magicMethod.Invoke(magicClassObject, new object[]{receivedAsText});

                        Console.WriteLine($"maginc value :: {magicValue}");
                    }

                // Console.WriteLine($"type of package:: {type[3]}");
                
                
                Console.WriteLine($"Received a message from server, via socket: {receivedAsText}");
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