package project.sep3.orders.db;

import javax.persistence.Column;
import javax.persistence.Embeddable;

@Embeddable
public class LocationPoint {

    private String startingAddress;
    private double startingLat;
    private double startingLng;
    private String destinationAddress;
    private double destinationLag;
    private double destinationLng;

    public LocationPoint() {
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

    public double getDestinationLag() {
        return destinationLag;
    }

    public void setDestinationLag(double destinationLag) {
        this.destinationLag = destinationLag;
    }

    public double getDestinationLng() {
        return destinationLng;
    }

    public void setDestinationLng(double destinationLng) {
        this.destinationLng = destinationLng;
    }
}
