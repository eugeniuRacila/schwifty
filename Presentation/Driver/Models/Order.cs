using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Driver.Models
{
    public class Order
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonProperty("customerId")]
        [JsonPropertyName("customerId")]
        public int CustomerId { get; set; }
        
        [JsonProperty("locationPoint")]
        [JsonPropertyName("locationPoint")]
        public LocationPoint LocationPoints { get; set; }

        [JsonProperty("typeOfCar")]
        [JsonPropertyName("typeOfCar")]
        public string TypeOfCar { get; set; }

        [JsonProperty("neededSeats")]
        [JsonPropertyName("neededSeats")]
        public int NeededSeats { get; set; }
        
        [JsonProperty("stateId")]
        [JsonPropertyName("stateId")]
        public int stateId { get; set; }
        
        [JsonProperty("stateDesc")]
        [JsonPropertyName("stateDesc")]
        public string stateDesc { get; set; }

        [JsonProperty("createdOn")]
        [JsonPropertyName("createdOn")]
        public long CreatedOn { get; set; }


        public Order()
        {
            LocationPoints = new LocationPoint();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        
        public class LocationPoint
        {
            [JsonProperty("startingAddress")]
            public string StartingAddress { get; set; }
        
            [JsonProperty("startingLat")]
            public double StartingLat { get; set; }
        
            [JsonProperty("startingLng")]
            public double StartingLng { get; set; }
        
            [JsonProperty("destinationAddress")]
            public string DestinationAddress { get; set; }
        
            [JsonProperty("destinationLag")]
            public double DestinationLat { get; set; }
        
            [JsonProperty("destinationLng")]
            public double DestinationLng { get; set; }

            public override string ToString()
            {
                return "Starting address: " + StartingAddress + "\nDestination address: " + DestinationAddress +
                       "\nStarting lat: " + StartingLat + "\nStarting lng: " + StartingLng + "\nDestination lat" +
                       DestinationLat + "\nDestination lng:" + DestinationLng;
            }
        }
    }
}