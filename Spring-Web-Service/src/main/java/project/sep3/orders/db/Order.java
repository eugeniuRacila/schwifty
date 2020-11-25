package project.sep3.orders.db;

import org.hibernate.annotations.CacheConcurrencyStrategy;

import javax.persistence.*;

@Entity
@Table(name="orders")
@Cacheable
@org.hibernate.annotations.Cache(usage = CacheConcurrencyStrategy.READ_WRITE)
public class Order {
    @Id
    @Column(name = "id")
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Basic
    private Integer id;
    @Column(name="customer_Id", nullable=false)
    private int customerId;
    @Column(name="driver_Id")
    private int driverId;
    @Column(name="type_Of_Car")
    private String typeOfCar;
    @Column(name = "needed_Seats", nullable = false)
    private int neededSeats;


    @Embedded
    private LocationPoint locationPoint;

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }


    public LocationPoint getLocationPoint() {
        return locationPoint;
    }

    public void setLocationPoint(LocationPoint locationPoint) {
        this.locationPoint = locationPoint;
    }



    public Order() {}

    public void setCustomerId(int customerId) {
        this.customerId = customerId;
    }

    public void setDriverId(int driverId) {
        this.driverId = driverId;
    }


    public void setTypeOfCar(String typeOfCar) {
        this.typeOfCar = typeOfCar;
    }

    public void setNeededSeats(int neededSeats) {
        this.neededSeats = neededSeats;
    }

    public int getCustomerId() {
        return customerId;
    }

    public int getDriverId() {
        return driverId;
    }


    public String getTypeOfCar() {
        return typeOfCar;
    }

    public int getNeededSeats() {
        return neededSeats;
    }
}
