package project.sep3.DAO.drivers;

import org.hibernate.*;
import org.hibernate.cfg.Configuration;
import org.hibernate.service.ServiceRegistry;
import org.hibernate.service.ServiceRegistryBuilder;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import project.sep3.models.Driver;
import project.sep3.models.Order;

import java.util.List;

public class DriverDAOImpl implements DriverDAO{
    private final SessionFactory sessionFactory;
    private final BCryptPasswordEncoder bCryptPasswordEncoder = new BCryptPasswordEncoder();

    public DriverDAOImpl() {
        Configuration configuration = new Configuration().configure().addAnnotatedClass(Driver.class);
        ServiceRegistry reg = new ServiceRegistryBuilder().applySettings(
                configuration.getProperties()).buildServiceRegistry();
        sessionFactory = configuration.buildSessionFactory(reg);
    }

    // Move into an utils static class
    private Session getNewSession() {
        return sessionFactory.openSession();
    }

    // Move into an utils static class
    private void saveDriver(Session session, Driver driver) {
        Transaction transaction = session.beginTransaction();
        session.save(driver);
        transaction.commit();
    }

    @Override
    public Driver create(String firstName, String lastName, String email, String phoneNumber, String password) {
        Driver driverToCreate = new Driver();
        driverToCreate.setFirstName(firstName);
        driverToCreate.setLastName(lastName);
        driverToCreate.setPhoneNumber(phoneNumber);
        driverToCreate.setEmail(email);



        String hashedPassword = bCryptPasswordEncoder.encode(password);
        System.out.println("hashedPassword: " + hashedPassword);
        driverToCreate.setPassword(hashedPassword);

        Session session = getNewSession();
        saveDriver(session, driverToCreate);
        session.close();

        return driverToCreate;
    }

    @Override
    public Driver findByEmail(String email) {
        Session session = getNewSession();
        Transaction transaction = session.beginTransaction();
        String sql = "FROM Driver WHERE email = :email";
        Query query = session.createQuery(sql);
        query.setParameter("email", email);
        List results = query.list();
        transaction.commit();
        session.close();

        if (results.isEmpty())
            return null;

        return (Driver) results.get(0);
    }

    @Override
    public Order getActiveOrder(int driverId) {
        Session session = getNewSession();
        Transaction transaction = session.beginTransaction();
        Query query = session.createQuery("from Order WHERE state_id < 6 and driver_Id ="+driverId);
        query.setCacheable(true);

        List<Order> list = query.list();
        transaction.commit();
        session.close();
        System.out.println("GET ACTIVE ORDER OK: "+ list.size());
        if (list.size() == 0){
            return null;
        }
        return list.get(0);
    }

}
