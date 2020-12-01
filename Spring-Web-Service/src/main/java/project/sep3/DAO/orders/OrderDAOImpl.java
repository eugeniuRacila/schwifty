package project.sep3.DAO.orders;


import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.hibernate.cfg.Configuration;
import org.hibernate.service.ServiceRegistry;
import org.hibernate.service.ServiceRegistryBuilder;
import project.sep3.models.LocationPoints;
import project.sep3.models.Order;

import java.util.List;

public class OrderDAOImpl implements OrderDAO {
    private final SessionFactory sessionFactory;

    public OrderDAOImpl() {
        Configuration con = new Configuration().configure().addAnnotatedClass(Order.class);
        ServiceRegistry reg = new ServiceRegistryBuilder().applySettings(
                con.getProperties()).buildServiceRegistry();
        sessionFactory = con.buildSessionFactory(reg);
    }


    private Session getNewSession(){
        return sessionFactory.openSession();
    }
    private void saveOrder(Session session,Order order){
        Transaction tx = session.beginTransaction();
        session.save(order);
        tx.commit();
    }


    @Override
    public Order create(int customerId, String typeOfCar, LocationPoints locationPoints, int neededSeats) {
        Order order = new Order();
        order.setCustomerId(customerId);
        order.setLocationPoint(locationPoints);
        order.setTypeOfCar(typeOfCar);
        order.setNeededSeats(neededSeats);
        Session session = getNewSession();
        saveOrder(session, order);
        session.close();
        return order;
    }

    @Override
    public List<Order> readAll() {
        Session session = getNewSession();
        Query query = session.createQuery("from Order");
        query.setCacheable(true);
        System.out.println(query.list().size());
        List orders = query.list();
        session.close();
        return orders;
    }
}
