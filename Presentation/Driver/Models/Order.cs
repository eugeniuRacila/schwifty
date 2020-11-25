using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Driver.Models
{
    public class Order
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public string OrderId { get; set; }
        
        [JsonProperty("customerId")]
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("driverId")]
        [JsonPropertyName("driverId")]
        public int DriverId { get; set; }

        [JsonProperty("carId")]
        [JsonPropertyName("carId")]
        public int CarId { get; set; }

        [JsonProperty("locationPoint")]
        [JsonPropertyName("locationPoint")]
        public LocationPoint LocationPoints { get; set; }
        
        [JsonProperty("typeOfCar")]
        [JsonPropertyName("typeOfCar")]
        public string TypeOfCar { get; set; }

        [JsonProperty("neededSeats")]
        [JsonPropertyName("neededSeats")]
        public int NeededSeats { get; set; }

        [JsonProperty("createdOn")]
        [JsonPropertyName("createdOn")]
        public long CreatedOn { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        
        public class LocationPoint
        {
            [JsonProperty("startingAddress")]
            [JsonPropertyName("startingAddress")]
            public string StartingAddress { get; set; }
        
            [JsonProperty("startingLat")]
            [JsonPropertyName("startingLat")]
            public double StartingLat { get; set; }
        
            [JsonProperty("startingLng")]
            [JsonPropertyName("startingLng")]
            public double StartingLng { get; set; }
        
            [JsonProperty("destinationAddress")]
            [JsonPropertyName("destinationAddress")]
            public string DestinationAddress { get; set; }
        
            [JsonProperty("destinationLat")]
            [JsonPropertyName("destinationLat")]
            public double DestinationLat { get; set; }
        
            [JsonProperty("destinationLng")]
            [JsonPropertyName("destinationLng")]
            public double DestinationLng { get; set; }
        }
    }
}