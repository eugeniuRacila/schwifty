package project.sep3.orders.db;


import java.util.List;

public interface OrdersDAO {
    Order create(int customerId, String typeOfCar, LocationPoint locationPoint, int neededSeats);
    List<Order> readAll();
}
