package project.sep3.DAO.drivers;

import project.sep3.models.Driver;

import java.util.ArrayList;
import java.util.List;

public interface DriverDAO {
    Driver create(String firstName, String lastName, String email, String phoneNumber, String password);
    Driver get(String email, String password);
    Driver get(int id);
    ArrayList<Driver> get();
}