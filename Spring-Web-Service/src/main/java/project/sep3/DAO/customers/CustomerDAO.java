package project.sep3.DAO.customers;

import project.sep3.entities.Customer;

public interface CustomerDAO {
    Customer create(String firstName, String lastName, String email, String password);
    Customer findByEmail(String email);
}
