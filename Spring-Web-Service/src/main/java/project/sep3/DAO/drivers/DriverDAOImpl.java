package project.sep3.DAO.drivers;

import org.hibernate.*;
import org.hibernate.cfg.Configuration;
import org.hibernate.criterion.Restrictions;
import org.hibernate.service.ServiceRegistry;
import org.hibernate.service.ServiceRegistryBuilder;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import project.sep3.models.Driver;

import javax.persistence.criteria.CriteriaBuilder;
import java.util.ArrayList;
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
    public Driver get(String email, String password) {
        Session session = getNewSession();
        Criteria criteria = session.createCriteria(Driver.class);
        Driver driver =(Driver)criteria.add(Restrictions.eq("email", email)).uniqueResult();
        System.out.println("I am here + " + driver);
        session.close();
        return driver;
    }

    public Driver get(int id) {
        Session session = getNewSession();
        Criteria criteria = session.createCriteria(Driver.class);
        Driver driver =(Driver) session.get(Driver.class, id);
        System.out.println("I am here + " + driver);
        session.close();
        return driver;
    }

  public ArrayList<Driver> get() {
      Session session = getNewSession();
  }
}
