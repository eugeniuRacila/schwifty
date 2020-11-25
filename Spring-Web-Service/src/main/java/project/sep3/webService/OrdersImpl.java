package project.sep3.webService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import project.sep3.orders.db.Order;
import project.sep3.orders.db.OrdersDAO;
import project.sep3.util.DuplicateKeyException;

import java.util.List;


@RestController
@RequestMapping("/orders")
public class OrdersImpl {
    private final OrdersDAO ordersDAO;

    @Autowired
    public OrdersImpl(OrdersDAO ordersDAO) {
        this.ordersDAO = ordersDAO;
    }

    @GetMapping
    public List<Order> readAll() {
        return ordersDAO.readAll();
    }

    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public Order create(@RequestBody Order order)  {
        try {
            System.out.println(order.getTypeOfCar());
            return ordersDAO.create(order.getCustomerId(), order.getTypeOfCar(), order.getLocationPoint(), order.getNeededSeats());
        } catch(RuntimeException e) {
            if (e.getMessage().contains("duplicate key"))
                throw new DuplicateKeyException(e);
            else
                throw e;
        }
    }


}
