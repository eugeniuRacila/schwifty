package project.sep3.DAO.orders;


import project.sep3.models.LocationPoints;
import project.sep3.models.Order;

import java.util.List;

public interface OrderDAO {
    Order create(int customerId, String typeOfCar, LocationPoints locationPoints, int neededSeats);
    List<Order> readAll();
}
