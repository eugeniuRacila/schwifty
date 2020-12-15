package project.sep3.DAO.customers;

import project.sep3.entities.Customer;
import project.sep3.models.Order;

public interface CustomerDAO {
    Customer create(String firstName, String lastName, String email, String password);
    Customer findByEmail(String email);

    Order getActiveOrder(int customerId);
}
