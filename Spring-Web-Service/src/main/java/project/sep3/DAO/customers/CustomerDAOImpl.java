package project.sep3.DAO.customers;

import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.hibernate.cfg.Configuration;
import org.hibernate.service.ServiceRegistry;
import org.hibernate.service.ServiceRegistryBuilder;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import project.sep3.models.Customer;

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
        //Customer customerToCreate = new Customer(firstName, lastName, email, password);
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
}
