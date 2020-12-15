package project.sep3.DAO.orders;
import project.sep3.models.Order;

import java.util.List;

public interface OrderDAO {
    Order getById(int id);
    Order create(Order order);
    void update(Order order);
    Order take(Order order);
    List<Order> readAll();
}
