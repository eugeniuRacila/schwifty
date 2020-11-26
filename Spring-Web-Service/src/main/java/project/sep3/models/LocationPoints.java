package project.sep3.models;

import org.hibernate.annotations.CacheConcurrencyStrategy;

import javax.persistence.Cacheable;
import javax.persistence.Embeddable;

@Embeddable
@Cacheable
@org.hibernate.annotations.Cache(usage = CacheConcurrencyStrategy.READ_WRITE)
public class LocationPoints {

    private String startingAddress;
    private double startingLat;
    private double startingLng;
    private String destinationAddress;
    private double destinationLat;
    private double destinationLng;

    public LocationPoints() {
    }

    public String getStartingAddress() {
        return startingAddress;
    }

    public void setStartingAddress(String startingAddress) {
        this.startingAddress = startingAddress;
    }

    public double getStartingLat() {
        return startingLat;
    }

    public void setStartingLat(double startingLat) {
        this.startingLat = startingLat;
    }

    public double getStartingLng() {
        return startingLng;
    }

    public void setStartingLng(double startingLng) {
        this.startingLng = startingLng;
    }

    public String getDestinationAddress() {
        return destinationAddress;
    }

    public void setDestinationAddress(String destinationAddress) {
        this.destinationAddress = destinationAddress;
    }

    public double getDestinationLat() {
        return destinationLat;
    }

    public void setDestinationLat(double destinationLat) {
        this.destinationLat = destinationLat;
    }

    public double getDestinationLng() {
        return destinationLng;
    }

    public void setDestinationLng(double destinationLng) {
        this.destinationLng = destinationLng;
    }
}
