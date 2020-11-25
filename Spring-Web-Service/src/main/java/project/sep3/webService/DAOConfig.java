package project.sep3.webService;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Scope;
import project.sep3.orders.db.OrdersDAO;
import project.sep3.orders.db.OrdersDAOImpl;

@Configuration
@ComponentScan(basePackages = "project.sep3.webService")
public class DAOConfig {
    @Bean
    @Scope("singleton")
    public OrdersDAO OrdersDAO() {
        return new OrdersDAOImpl();
    }
}
