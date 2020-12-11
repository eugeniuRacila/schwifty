package project.sep3.restService.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import project.sep3.models.Order;
import project.sep3.DAO.orders.OrderDAO;
import project.sep3.models.State;

import java.util.List;

@RestController
@RequestMapping("/orders")
public class OrderController {
    private final OrderDAO orderDAO;

    @Autowired
    public OrderController(OrderDAO orderDAO) {
        this.orderDAO = orderDAO;
    }

    @GetMapping
    public List<Order> readAll() {
        return orderDAO.readAll();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public Order create(@RequestBody Order order) {
        System.out.println(order.getStateId());
        return orderDAO.create(order);
    }

    @PostMapping
    @RequestMapping("/update")
    @ResponseStatus(HttpStatus.OK)
    public void update(@RequestBody Order order) {
        orderDAO.update(order);
    }

    @PatchMapping
    public Order take(@RequestBody Order order, @RequestParam int driverId) {
        return orderDAO.take(order, driverId);
    }
}
