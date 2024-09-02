package Entities;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class OwnerCarStock implements Serializable
{
	private List<OwnerCar> ownersList; //רשימת בעלי רכבים

	public OwnerCarStock()
	{
		this.ownersList = new ArrayList<>();
	}

	public void setOwnersList(List<OwnerCar> ownersList) {
		this.ownersList = ownersList;
	}
	
	public List<OwnerCar> getAllCarOwners() {
		List<OwnerCar> lst = new ArrayList<>();
		for (OwnerCar o : ownersList)
			lst.add(new OwnerCar(o));
        return lst;
    }

    public OwnerCar getCarOwnerByOwnerId(int ownerId) 
    {
        for (OwnerCar owner : ownersList)
        {
            if (owner.getId() == ownerId) 
                return owner;
        }
        return null;
    }
    
    public boolean removeCarFromList(List<Integer> owners, int carId)
    {
        boolean success = true;
        
        for (Integer ownerId : owners) 
        {
            OwnerCar owner = getCarOwnerByOwnerId(ownerId);
            if (owner == null || !owner.removeCarIdFormList(carId)) 
                success = false;
        }
        return success;
    }


    public OwnerCar getCarOwnerByOwnerName(String ownerName) 
    {
        for (OwnerCar owner : ownersList)
        {
            if (owner.getName().equals(ownerName)) 
                return owner;
        }
        return null;
    }

    public boolean addCarOwner(OwnerCar newOwner, int carId)
    {
    	boolean flag = newOwner.addCarToOwner(carId);
    	if (flag)
    	{    		
    		if(!ownersList.contains(newOwner))
    	          return ownersList.add(newOwner); 
    	}
    	return flag;
    }

    public void printOwners() {
        for (OwnerCar owner : ownersList) {
            System.out.println(owner.toString());
        }
    }
    
    public boolean updateOwnerCarDetails(int ownerId, String newName, String newAddress) 
    {
        OwnerCar owner = getCarOwnerByOwnerId(ownerId);
        if (owner != null) 
        {
            owner.setName(newName);
            owner.setAddress(newAddress);
            return true;
        }
        return false;
    }
    
}



