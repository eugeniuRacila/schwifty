package project.sep3.restService.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import project.sep3.DAO.customers.CustomerDAO;
import project.sep3.models.Customer;
import project.sep3.models.LoginRequest;

@RestController
@RequestMapping("/customers")
public class CustomerController {
    private final CustomerDAO customerDAO;

    @Autowired
    public CustomerController(CustomerDAO customerDAO) {
        this.customerDAO = customerDAO;
    }

    @GetMapping
    public Customer findByEmail(@RequestBody LoginRequest loginRequest) {
        return customerDAO.findByEmail(loginRequest.email);
    }
}
