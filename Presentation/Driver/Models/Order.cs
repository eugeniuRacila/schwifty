using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Driver.Models
{
    public class Order
    {
        [JsonProperty("orderId")]
        [JsonPropertyName("orderId")]
        public string orderId { get; set; }
        
        [JsonProperty("customerId")]
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("driverId")]
        [JsonPropertyName("driverId")]
        public int DriverId { get; set; }

        [JsonProperty("carId")]
        [JsonPropertyName("carId")]
        public int CarId { get; set; }

        [JsonProperty("startingPoint")]
        [JsonPropertyName("startingPoint")]
        public LocationPoint StartingPoint { get; set; }

        [JsonProperty("destinationPoint")]
        [JsonPropertyName("destinationPoint")]
        public LocationPoint DestinationPoint { get; set; }
        
        [JsonProperty("typeOfCar")]
        [JsonPropertyName("typeOfCar")]
        public string TypeOfCar { get; set; }

        [JsonProperty("neededSeats")]
        [JsonPropertyName("neededSeats")]
        public int NeededSeats { get; set; }

        [JsonProperty("createdOn")]
        [JsonPropertyName("createdOn")]
        public long CreatedOn { get; set; }

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