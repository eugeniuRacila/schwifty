namespace LogicLayer.Models
{
    public interface OrderStatus
    {
        public int GetId();
        public void NextStatus(Order order);
        public void AbortByDriver(Order order);
        public void AbortByCustomer(Order order);

    }
}