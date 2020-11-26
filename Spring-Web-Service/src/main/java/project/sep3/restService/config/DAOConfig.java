package project.sep3.restService.config;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Scope;
import project.sep3.DAO.orders.OrderDAO;
import project.sep3.DAO.orders.OrderDAOImpl;

@Configuration
@ComponentScan(basePackages = "project.sep3.restService")
public class DAOConfig {
    @Bean
    @Scope("singleton")
    public OrderDAO OrdersDAO() {
        return new OrderDAOImpl();
    }
}
