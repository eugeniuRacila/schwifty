package project.sep3.restService.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import project.sep3.DAO.customers.CustomerDAO;
import project.sep3.models.Customer;
import project.sep3.models.LoginRequest;
import project.sep3.models.Order;

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

    @GetMapping
    @RequestMapping("/{custId}/orders/active")
    public Order GetCustomerActiveOrder(@PathVariable("custId") String custId) {
        int customerId = Integer.parseInt(custId);
        var res = customerDAO.getActiveOrder(customerId);
        return res;
    }


}
