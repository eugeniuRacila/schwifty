package project.sep3.restService.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import project.sep3.DAO.drivers.DriverDAO;
import project.sep3.entities.Customer;
import project.sep3.models.Driver;
import project.sep3.models.LoginRequest;

import java.util.ArrayList;

@RestController
@RequestMapping("/drivers")
public class DriverController {
    private final DriverDAO driverDAO;

    @Autowired
    public DriverController(DriverDAO driverDAO) {
        this.driverDAO = driverDAO;
    }

    @GetMapping
    public Driver findByEmail(@RequestBody LoginRequest loginRequest) {
        return driverDAO.findByEmail(loginRequest.email);
    }
}
