namespace LogicLayer.Models
{
    public class DestinationReached : OrderStatus
    {
        public int id = 5;
        private static OrderStatus OrderStatus;

        private DestinationReached()
        {
        }

        public static OrderStatus GetInst()
        {
            if (OrderStatus == null)
            {
                OrderStatus = new DestinationReached();
            }
            return OrderStatus;
        }

        public string GetDesc()
        {
            return "Your destination was safely reached!";
        }

        public int GetId()
        {
            return id;
        }

        public void NextStatus(Order order)
        {
            order._orderStatus = OrderComplete.GetInst();
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