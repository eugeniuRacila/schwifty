package project.sep3.DAO.states;

import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import project.sep3.models.State;
import org.hibernate.cfg.Configuration;
import org.hibernate.service.ServiceRegistry;
import org.hibernate.service.ServiceRegistryBuilder;
import java.util.List;

public class StateDAOImpl implements StateDAO {
    private SessionFactory sessionFactory;

    public StateDAOImpl() {
        Configuration con = new Configuration().configure().addAnnotatedClass(State.class);
        ServiceRegistry reg = new ServiceRegistryBuilder().applySettings(con.getProperties()).buildServiceRegistry();
        sessionFactory = con.buildSessionFactory(reg);

        if(readAll().size() == 0){
            populateStates();
        }

    }

    private Session getNewSession() {
        return sessionFactory.openSession();
    }

    private void save(Session session, State state) {
        Transaction tx = session.beginTransaction();
        session.save(state);
        tx.commit();
    }

    private void populateStates(){
        Session session = getNewSession();
        save(session, new State("Created")); // 1
        save(session, new State("Taken by Driver")); // 2
        save(session, new State("Driver waiting for Customer")); // 3
        save(session, new State("Customer picked up")); // 4
        save(session, new State("Destination reached")); // 5
        save(session, new State("Complete")); // 6
        session.close();
    }

    @Override
    public List<State> readAll() {
        Session session = getNewSession();
        Transaction tx = session.beginTransaction();
        Query query = session.createQuery("from State");
        query.setCacheable(true);
        List<State> states = query.list();
        tx.commit();
        session.close();
        return states;
    }
}
