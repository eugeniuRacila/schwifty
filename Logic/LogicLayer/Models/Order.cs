using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LogicLayer.Models
{
    public class Order
    {
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }
        
        [JsonPropertyName("driverId")]
        public int DriverId { get; set; }
        
        [JsonPropertyName("carId")]
        public int CarId { get; set; }
        
        [JsonPropertyName("startingPoint")]
        public LocationPoint StartingPoint { get; set; }
        
        [JsonPropertyName("destinationPoint")]
        public LocationPoint DestinationPoint { get; set; }
        
        [JsonPropertyName("neededSeats")]
        public int NeededSeats { get; set; }

        [JsonPropertyName("createdOn")]
        public long CreatedOn { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        public class LocationPoint
        {
            public string Address { get; set; }
            public double Lat { get; set; }
            public double Lng { get; set; }
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}