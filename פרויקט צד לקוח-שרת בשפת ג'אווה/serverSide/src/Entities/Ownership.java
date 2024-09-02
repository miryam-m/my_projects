package Entities;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class Ownership implements Serializable
{
	private int CarId;
	private List<Integer> ownerCurrList; //בעלים נוכחים
	private List<Integer> ownerPreviosList; // בעלים קודמים

	public Ownership(int CarId)
	{
		this.CarId = CarId;
		this.ownerCurrList = new ArrayList<>();
		this.ownerPreviosList = new ArrayList<>();
	}

	public int getCarId() {
		return CarId;
	}

	public List<Integer> getOwnerCurrList() {
		return ownerCurrList;
	}

	public void setOwnerCurrList(List<Integer> ownerCurrList) {
		this.ownerCurrList = ownerCurrList;
	}
	
	public void setOwnerCurrList(int newOwnerId) {
	    this.ownerCurrList.add(newOwnerId);
	}


	public List<Integer> getOwnerPreviosList() {
		return ownerPreviosList;
	}

	public void setOwnerPreviosList(List<Integer> ownerPreviosList) {
		this.ownerPreviosList = ownerPreviosList;
	}
	
	//שינוי בעלות
    public void changeOwnership(List<Integer> currOwnerIds, int newOwnerId)
    {
        if (currOwnerIds == null || currOwnerIds.isEmpty()) 
        {
            ownerPreviosList.addAll(ownerCurrList);
            ownerCurrList.clear();
        } 
        else 
        {
            for (Integer currOwnerId : currOwnerIds) 
            {
                if (ownerCurrList.contains(currOwnerId)) 
                {
                    ownerCurrList.remove(currOwnerId);
                    ownerPreviosList.add(currOwnerId);
                } 
                else 
                    System.out.println("Error: Current owner ID " + currOwnerId + " not found.");
            }
        }
        ownerCurrList.add(newOwnerId);
    }
    
    //החזרת הבעלים הנוכחים שהופכים להיות קודמים כדי למחוק מרשימת הרכבים שבבעלותם במחלקת בעל רכב
    public List<Integer> changeOwnership(int newOwnerId)
    {
   	   List<Integer> ownersToRemoveCarId = new ArrayList<>();

        ownerPreviosList.addAll(ownerCurrList);
         for(Integer o : ownerCurrList)
        	 ownersToRemoveCarId.add(o);
        ownerCurrList.clear();
        ownerCurrList.add(newOwnerId);
        
        return ownersToRemoveCarId;
    }
	
    


	
}
