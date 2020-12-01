package project.sep3.restService.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import project.sep3.DAO.customers.CustomerDAO;
import project.sep3.models.Customer;

@RestController
@RequestMapping("/customers")
public class CustomerController {
    private final CustomerDAO customerDAO;

    @Autowired
    public CustomerController(CustomerDAO customerDAO) {
        this.customerDAO = customerDAO;
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public Customer create(@RequestBody Customer customer) {
        // TODO if checks for fields validation
        return customerDAO.create(customer.getFirstName(), customer.getLastName(), customer.getEmail(), customer.getPassword());
    }
}
