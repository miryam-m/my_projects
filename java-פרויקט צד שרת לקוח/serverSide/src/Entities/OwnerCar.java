package Entities;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;


public class OwnerCar implements Serializable
{
    private int id;
    private String name;
    private String address;
    private List<Integer> CarList; //קודי הרכבים שבבעלותו
    
    public OwnerCar(int id, String name, String address)
    {
    	this.id = id;
    	this.name = name;
    	this.address = address;
    	this.CarList = new ArrayList<>();
    }
    
    public OwnerCar(OwnerCar own)
    {
    	this.id = own.id;
    	this.name = own.name;
    	this.address = own.address;
    	this.CarList = own.CarList;
    }

	public int getId() {
		return id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	public List<Integer> getCarList() {
		return CarList;
	}

	public void setCarList(List<Integer> carList) {
		CarList = carList;
	}
	
	//פונקציה המחזירה את קודי הרכבים שברשותו
	public List<Integer> getCars()
	{
		if(CarList == null || CarList.isEmpty())
			return null;
		return CarList;	
	}
	
	//פונקציה הבודקת האם קוד הרכב שהתקבל נמצא בבעלותו
    public boolean getCarById(int CarId) 
    {
        if (CarList == null || CarList.isEmpty()) 
            return false;
        
        return CarList.contains(CarId);
    }

	//פונקציה המקבלת קוד רכב להוספה
	public boolean addCarToOwner(int CarId)
	{
        if (!CarList.contains(CarId)) 
        {
        	CarList.add(CarId);	
        	return true;
        }
        return false;
	}
	
	public boolean removeCarIdFormList(int CarId)
	{
        if (CarList.contains(CarId)) 
        {
        	CarList.remove((Integer)CarId);	
        	return true;
        }
        return false;
	}
	
	@Override
    public String toString() {
        return "CarOwner{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", address='" + address + '\'' +
                ", carList=" + CarList +
                '}';
    }
}



