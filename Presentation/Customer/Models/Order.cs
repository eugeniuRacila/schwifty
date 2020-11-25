using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Customer.Models
{
    public class Order
    {
        [JsonProperty("customerId")]
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("startingPoint")]
        [JsonPropertyName("startingPoint")]
        public LocationPoint StartingPoint { get; set; } = new LocationPoint();
        
        [JsonProperty("destinationPoint")]
        [JsonPropertyName("destinationPoint")]
        public LocationPoint DestinationPoint { get; set; } = new LocationPoint();
        
        [JsonProperty("typeOfCar")]
        [JsonPropertyName("typeOfCar")]
        public string TypeOfCar { get; set; }

        [JsonProperty("neededSeats")]
        [JsonPropertyName("neededSeats")]
        public int NeededSeats { get; set; }

        public class LocationPoint
        {
            [JsonProperty("address")]
            [JsonPropertyName("address")]
            public string Address { get; set; }
            
            [JsonProperty("lat")]
            [JsonPropertyName("lat")]
            public double Lat { get; set; }
            
            [JsonProperty("lng")]
            [JsonPropertyName("lng")]
            public double Lng { get; set; }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}