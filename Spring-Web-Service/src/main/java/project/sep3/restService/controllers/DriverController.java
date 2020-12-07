package project.sep3.restService.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import project.sep3.DAO.drivers.DriverDAO;
import project.sep3.models.Customer;
import project.sep3.models.Driver;

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
}
