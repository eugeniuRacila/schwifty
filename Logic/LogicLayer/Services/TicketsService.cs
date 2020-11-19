using System.Collections.Generic;
using System.Threading.Tasks;
using LogicLayer.Models;
using RestSharp;

namespace LogicLayer.Services
{
    public class TicketsService
    {
        public async Task<Ticket> Create(Ticket ticket)
        {
            // Calling java to save the ticket into DB
            return new Ticket {TextMessageTest = ticket.TextMessageTest};
        }

        public string GetOrders()
        {
            var client = new RestClient("http://localhost:8080/");

            var request = new RestRequest("orders", Method.GET);

            var response = client.Get(request);

            return response.Content;
        }
    }
}