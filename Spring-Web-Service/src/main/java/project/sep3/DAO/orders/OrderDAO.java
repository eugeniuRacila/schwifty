package project.sep3.DAO.orders;


import project.sep3.models.LocationPoints;
import project.sep3.models.Order;

import java.util.List;

public interface OrderDAO {
    Order create(Order order);
    void update(Order order);
    Order take(Order order, int driverId);
    List<Order> readAll();
}
