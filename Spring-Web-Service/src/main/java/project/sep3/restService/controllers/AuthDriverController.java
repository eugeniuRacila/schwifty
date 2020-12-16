package project.sep3.restService.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;
import project.sep3.DAO.drivers.DriverDAO;
import project.sep3.models.Driver;
import project.sep3.models.LoginRequest;

@RestController
@RequestMapping("/auth")
public class AuthDriverController {
    private final DriverDAO driverDAO;
    private final BCryptPasswordEncoder bCryptPasswordEncoder = new BCryptPasswordEncoder();

    @Autowired
    public AuthDriverController(DriverDAO driverDAO) {
        this.driverDAO = driverDAO;
    }

    @PostMapping
    @RequestMapping("driver/register")
    @ResponseStatus(HttpStatus.CREATED)
    public Driver createDriver(@RequestBody Driver driver) {
        // Check if email address is already taken
        if (driverDAO.findByEmail(driver.getEmail()) == null) {
            System.out.println("Email is not taken");
            return driverDAO.create(driver.getFirstName(), driver.getLastName(), driver.getEmail(), driver.getPhoneNumber(), driver.getPassword());
        }
        System.out.println("Email is taken");
        throw new ResponseStatusException(HttpStatus.UNPROCESSABLE_ENTITY, "Email is already registered");
    }

    @PostMapping
    @RequestMapping("driver/login")
    @ResponseStatus(HttpStatus.OK)
    public Driver loginDriver(@RequestBody LoginRequest loginRequest) {
        Driver foundDriver = driverDAO.findByEmail(loginRequest.email);

        // Check if user exists
        if (foundDriver != null) {
            // Check if password matches the hashed one
            if (bCryptPasswordEncoder.matches(loginRequest.password, foundDriver.getPassword())) {
                return foundDriver;
            }

            throw new ResponseStatusException(HttpStatus.UNAUTHORIZED, "Password does not match");
        }

        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Email is not registered");
    }
}