package project.sep3.DAO.customers;

import org.hibernate.*;
import org.hibernate.cfg.Configuration;
import org.hibernate.service.ServiceRegistry;
import org.hibernate.service.ServiceRegistryBuilder;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import project.sep3.entities.Customer;
import java.util.List;

public class CustomerDAOImpl implements CustomerDAO {
    private final SessionFactory sessionFactory;
    private final BCryptPasswordEncoder bCryptPasswordEncoder = new BCryptPasswordEncoder();

    public CustomerDAOImpl() {
        Configuration configuration = new Configuration().configure().addAnnotatedClass(Customer.class);
        ServiceRegistry reg = new ServiceRegistryBuilder().applySettings(
                configuration.getProperties()).buildServiceRegistry();
        sessionFactory = configuration.buildSessionFactory(reg);
    }

    // Move into an utils static class
    private Session getNewSession() {
        return sessionFactory.openSession();
    }

    // Move into an utils static class
    private void saveCustomer(Session session, Customer customer) {
        Transaction transaction = session.beginTransaction();
        session.save(customer);
        transaction.commit();
    }

    @Override
    public Customer create(String firstName, String lastName, String email, String password) {
        Customer customerToCreate = new Customer();
        customerToCreate.setFirstName(firstName);
        customerToCreate.setLastName(lastName);
        customerToCreate.setEmail(email);

        String hashedPassword = bCryptPasswordEncoder.encode(password);
        System.out.println("hashedPassword: " + hashedPassword);
        customerToCreate.setPassword(hashedPassword);

        Session session = getNewSession();
        saveCustomer(session, customerToCreate);
        session.close();

        return customerToCreate;
    }

    @Override
    public Customer findByEmail(String email) {
        Session session = getNewSession();
        Transaction transaction = session.beginTransaction();
        String sql = "SELECT * FROM customers WHERE email = :email";
        SQLQuery query = session.createSQLQuery(sql);
        query.addEntity(Customer.class);
        query.setParameter("email", email);
        List results = query.list();
        transaction.commit();
        session.close();

        if (results.isEmpty())
            return null;

        return (Customer) results.get(0);
    }


}
