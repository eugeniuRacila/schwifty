package project.sep3.DAO.drivers;

import project.sep3.entities.Customer;
import project.sep3.models.Driver;

import java.util.ArrayList;
import java.util.List;

public interface DriverDAO {
    Driver create(String firstName, String lastName, String email, String phoneNumber, String password);
    Driver findByEmail(String email);
}