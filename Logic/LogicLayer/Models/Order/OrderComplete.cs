namespace LogicLayer.Models
{
    public class OrderComplete : OrderStatus
    {
        public int id = 6;
        private static OrderStatus OrderStatus;

        private OrderComplete()
        {
        }

        public static OrderStatus GetInst()
        {
            if (OrderStatus == null)
            {
                OrderStatus = new OrderComplete();
            }
            return OrderStatus;
        }

        public string GetDesc()
        {
            return "Thank you for using Schwifty!";
        }

        public int GetId()
        {
            return id;
        }

        public void NextStatus(Order order)
        {
        }

        public void AbortByDriver(Order order)
        {
        }

        public void AbortByCustomer(Order order)
        {
        }
    }
}