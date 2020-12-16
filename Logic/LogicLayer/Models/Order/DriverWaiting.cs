namespace LogicLayer.Models
{
    public class DriverWaiting : OrderStatus
    {
        public int id = 3;
        private static OrderStatus OrderStatus;

        private DriverWaiting()
        {
        }

        public static OrderStatus GetInst()
        {
            if (OrderStatus == null)
            {
                OrderStatus = new DriverWaiting();
            }
            return OrderStatus;
        }

        public string GetDesc()
        {
            return "Your Driver arrived the starting point and is waiting for you!";
        }

        public int GetId()
        {
            return id;
        }

        public void NextStatus(Order order)
        {
            order._orderStatus = CustomerPicked.GetInst();
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