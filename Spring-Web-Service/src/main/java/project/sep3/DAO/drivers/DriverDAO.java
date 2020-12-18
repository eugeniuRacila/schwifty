package project.sep3.DAO.drivers;

import project.sep3.models.Driver;
import project.sep3.models.Order;

public interface DriverDAO {
    Driver create(String firstName, String lastName, String email, String phoneNumber, String password);
    Driver findByEmail(String email);

    Order getActiveOrder(int driverId);
}