namespace LogicLayer.Models
{
    public class CustomerPicked : OrderStatus
    {
        public int id = 4;
        private static OrderStatus OrderStatus;

        private CustomerPicked()
        {
        }

        public static OrderStatus GetInst()
        {
            if (OrderStatus == null)
            {
                OrderStatus = new CustomerPicked();
            }
            return OrderStatus;
        }

        public string GetDesc()
        {
            return "Enjoy your trip!";
        }

        public int GetId()
        {
            return id;
        }

        public void NextStatus(Order order)
        {
            order._orderStatus = DestinationReached.GetInst();
        }

        public void AbortByDriver(Order order)
        {
            throw new System.NotImplementedException();
        }

        public void AbortByCustomer(Order order)
        {
            throw new System.NotImplementedException();
        }
    }
}