using System.Collections.Generic;

namespace LogicLayer.Models
{
    public interface OrderStatus
    {
        public static OrderStatus GetById(int id)
        {
            switch (id)
            {
                case 1 : return OrderCreated.GetInst();
                case 2 : return OrderTaken.GetInst();
                case 3 : return DriverWaiting.GetInst();
                case 4 : return CustomerPicked.GetInst();
                case 5 : return DestinationReached.GetInst();
                case 6 : return OrderComplete.GetInst();
            }

            return null;
        }
        
        
        public string GetDesc();
        public int GetId();
        public void NextStatus(Order order);
        public void AbortByDriver(Order order);
        public void AbortByCustomer(Order order);

    }
}