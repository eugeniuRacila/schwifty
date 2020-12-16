namespace LogicLayer.Models
{
    public class OrderTaken : OrderStatus
    {
        public int id = 2;
        private static OrderStatus OrderStatus;

        private OrderTaken()
        {
        }

        public static OrderStatus GetInst()
        {
            if (OrderStatus == null)
            {
                OrderStatus = new OrderTaken();
            }
            return OrderStatus;
        }

        public string GetDesc()
        {
            return "Your driver is on its way!";
        }

        public int GetId()
        {
            return id;
        }

        public void NextStatus(Order order)
        {
            order._orderStatus = DriverWaiting.GetInst();
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