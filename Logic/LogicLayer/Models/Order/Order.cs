using System;
using System.Text.Json.Serialization;
using LogicLayer.Services;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace LogicLayer.Models
{
    public class Order
    {
        [JsonProperty("id")]
        [JsonPropertyName("id")]
        public int OrderId { get; set; }

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

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public OrderStatus _orderStatus
        {
            get => OrderStatus.GetById(stateId);
            set => stateId = value.GetId();
        }

        [JsonProperty("stateId")]
        [JsonPropertyName("stateId")]
        private int stateId { get; set; }

        [JsonProperty("stateDesc")]
        [JsonPropertyName("stateDesc")]
        public string stateDesc => _orderStatus.GetDesc();

        [JsonProperty("createdOn")]
        [JsonPropertyName("createdOn")]
        public long CreatedOn { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        public Order()
        {
            _orderStatus = OrderCreated.GetInst();
        }

        public OrderStatus NextStatus()
        {
            _orderStatus.NextStatus(this);
            return _orderStatus;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
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