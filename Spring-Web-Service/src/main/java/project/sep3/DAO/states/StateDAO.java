package project.sep3.DAO.states;

import project.sep3.models.LocationPoints;
import project.sep3.models.State;

import java.util.List;

public interface StateDAO {
    List<State> readAll();
}
