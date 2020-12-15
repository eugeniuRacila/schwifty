package project.sep3.restService.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;
import project.sep3.DAO.customers.CustomerDAO;
import project.sep3.models.Customer;
import project.sep3.models.LoginRequest;

@RestController
@RequestMapping("/auth")
public class AuthCustomerController {
    private final CustomerDAO customerDAO;
    private final BCryptPasswordEncoder bCryptPasswordEncoder = new BCryptPasswordEncoder();

    @Autowired
    public AuthCustomerController(CustomerDAO customerDAO) {
        this.customerDAO = customerDAO;
    }

    @PostMapping
    @RequestMapping("customer/register")
    @ResponseStatus(HttpStatus.CREATED)
    public Customer createCustomer(@RequestBody Customer customer) {
        // Check if email address is already taken
        if (customerDAO.findByEmail(customer.getEmail()) == null) {
            System.out.println("Email is not taken");
            return customerDAO.create(customer.getFirstName(), customer.getLastName(), customer.getEmail(), customer.getPassword());
        }
        System.out.println("Email is taken");
        throw new ResponseStatusException(HttpStatus.UNPROCESSABLE_ENTITY, "Email is already registered");
    }

    @PostMapping
    @RequestMapping("customer/login")
    @ResponseStatus(HttpStatus.OK)
    public Customer loginCustomer(@RequestBody LoginRequest loginRequest) {
        Customer foundCustomer = customerDAO.findByEmail(loginRequest.email);

        // Check if user exists
        if (foundCustomer != null) {
            // Check if password matches the hashed one
            if (bCryptPasswordEncoder.matches(loginRequest.password, foundCustomer.getPassword())) {
                return foundCustomer;
            }

            throw new ResponseStatusException(HttpStatus.UNAUTHORIZED, "Password does not match");
        }

        throw new ResponseStatusException(HttpStatus.NOT_FOUND, "Email is not registered");
    }


}

