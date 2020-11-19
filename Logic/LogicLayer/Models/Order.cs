using System.Runtime.Serialization;
using System.Text.Json;

namespace LogicLayer.Models
{
    public class Order
    {
        [DataMember(Name = "customerId")]
        public int CustomerId { get; set; }

        public int DriverId { get; set; }
        [DataMember(Name = "startingPoint")]
        public string StartingPoint { get; set; }

        [DataMember(Name = "destinationPoint")]
        public string DestinationPoint { get; set; }

        [DataMember(Name = "typeOfCar")]
        public string TypeOfCar { get; set; }
        
        [DataMember(Name = "amountOfSeats")]
        public int AmountOfSeats { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}