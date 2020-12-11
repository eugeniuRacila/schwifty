package project.sep3.restService.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import project.sep3.DAO.drivers.DriverDAO;
import project.sep3.models.Driver;

import java.util.ArrayList;

@RestController
@RequestMapping("/drivers")
public class DriverController {
    private final DriverDAO driverDAO;

    @Autowired
    public DriverController(DriverDAO driverDAO) {
        this.driverDAO = driverDAO;
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public Driver create(@RequestBody Driver driver) {
        // TODO if checks for fields validation
        System.out.println("Driver password:" + driver.getPassword());
        System.out.println("Driver password:" + driver.getEmail());
        System.out.println("Driver password:" + driver.getFirstName());
        System.out.println("Driver password:" + driver.getPhoneNumber());
        System.out.println("Driver password:" + driver.getLastName());
        return driverDAO.create(driver.getFirstName(), driver.getLastName(), driver.getEmail(), driver.getPhoneNumber(), driver.getPassword());
    }

    @GetMapping
    @RequestMapping("/drivers/{email}")
    public Driver getDriver(@PathVariable String email, String password) {
        try{
            return driverDAO.get(email, password);
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    @GetMapping
    @RequestMapping("/drivers/{id}")
    public Driver getDriver(@PathVariable int id) {
        try{
            return driverDAO.get(id);
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    @GetMapping
    @RequestMapping("/drivers")
    public ArrayList<Driver> getDriver() {
        try{
            return driverDAO.get();
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }
}
