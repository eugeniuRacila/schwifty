using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LogicLayer.Models;
using LogicLayer.Services;
using LogicLayer.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LogicLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private WebSocketServerConnectionManager _manager;
        private readonly TicketsService _ticketService;

        public TicketController(TicketsService ticketService, WebSocketServerConnectionManager manager)
        {
            _manager = manager;
            _ticketService = ticketService;
        }

        [HttpGet]
        public ActionResult<string> GetOrders()
        {
            return _ticketService.GetOrders();
        }
        
        [HttpPost]
        public async Task<ActionResult<Ticket>> CreateAsync(Ticket ticket)
        {
            // Need to see if it blocks the return (aka if it loops trough the entire array first)
            // Move to service
            
            Package<Ticket> package = new Package<Ticket>("AddOrder", ticket);
            string jsonPackage = JsonSerializer.Serialize(package);

            Console.WriteLine($"jsonPackage :: {jsonPackage}");
            
            foreach (var sock in _manager.GetAllSockets())
            {
                if (sock.Value.State == WebSocketState.Open)
                    await sock.Value.SendAsync(Encoding.UTF8.GetBytes(jsonPackage), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            
            return await _ticketService.Create(ticket);
        }
    }
}