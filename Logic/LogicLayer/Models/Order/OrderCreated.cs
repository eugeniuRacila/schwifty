namespace LogicLayer.Models
{
    public class OrderCreated : OrderStatus
    {
        public int id = 1;
        private static OrderStatus OrderStatus;

        private OrderCreated()
        {
        }

        public static OrderStatus GetInst()
        {
            if (OrderStatus == null)
            {
                OrderStatus = new OrderCreated();
            }
            return OrderStatus;
        }

        public void NextStatus(Order order)
        {
            order._orderStatus = OrderTaken.GetInst();
            
        }

        public string GetDesc()
        {
            return "Looking for a Driver.";
        }

        public int GetId()
        {
            return id;
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