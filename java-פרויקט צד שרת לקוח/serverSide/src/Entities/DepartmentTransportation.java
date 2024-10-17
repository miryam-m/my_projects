package Entities;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class DepartmentTransportation 
{
	private CarStock carStock;
	private OwnerCarStock ownerStock;
	
	public DepartmentTransportation() 
	{
		carStock = new CarStock();
		ownerStock = new OwnerCarStock();
	}
	
	//add new car
	public boolean addCarToRoad(Car newCar)
	{
		return carStock.addCarToStock(newCar);
	}
	
	//add owner car
	public boolean addOnwerCar(OwnerCar newOwner, int carId)
	{
		Car c = carStock.getCarByLicensePlate(carId);
		if (c == null)
			return false; 
		return ownerStock.addCarOwner(newOwner, carId) && carStock.updateOwnership(newOwner,carId);
	}
	
	//change owner ship
	public boolean changeOwnership(int idOwner, int carId)
	{
		if (carStock.getCarByLicensePlate(carId) == null || ownerStock.getCarOwnerByOwnerId(idOwner) == null)
			return false;  //הרכב או הבעלים לא קימים במאגר
	
		//הוספת הרכב לרשימת הרכבים שברשות הבעלים
		ownerStock.addCarOwner(ownerStock.getCarOwnerByOwnerId(idOwner),carId);
		
		//בעבור כל בעלים קודם הסרת קוד הרכב מרשימת הרכבים שברשותו
		List<Integer> ownersToRemoveCarId = carStock.changeOwnership(carId,idOwner);
		
		return ownerStock.removeCarFromList(ownersToRemoveCarId,carId);
	}
	
	//update car details
	public boolean updateCarDetails(int carId, String newColor)
	{
		return carStock.updateCarDetails(carId, newColor);
	}
	
	//get list of the owners in order of ownership
	public List<OwnerCar> getOwnershipHistoryByCarId(int carId)
	{
		List<Integer> prev = carStock.getOwnershipHistory(carId).getOwnerPreviosList();
		List<Integer> curr = carStock.getOwnershipHistory(carId).getOwnerCurrList();
		List<OwnerCar> owners = new ArrayList<>();
		for (Integer s: prev)
			owners.add(ownerStock.getCarOwnerByOwnerId(s));
		for (Integer s: curr)
			owners.add(ownerStock.getCarOwnerByOwnerId(s));
		return owners;
	}
	
	//get car details by car id
	public Car getCarDetails(int carId)
	{
		return new Car(carStock.getCarByLicensePlate(carId));
	}
	
	//get all cars
	public List<Car> getAllCars()
	{
		return carStock.getAllCars();
	}
	
	//get owner car details by owner car id
	public OwnerCar getOwnerCarDetails(int ownerCarId)
	{
		return new OwnerCar(ownerStock.getCarOwnerByOwnerId(ownerCarId));
	}
	
	//get all owners
	public List<OwnerCar> getAllOwners()
	{
		return ownerStock.getAllCarOwners();
	}
	
	//get car owner details by car id
	public List<OwnerCar> getOwnersCarDetails(int carId)
	{
		List<Integer> lst = carStock.getOwnerCarDetailsByCarId(carId);
		List<OwnerCar> owners = new ArrayList<>();
		for(Integer s : lst)
			owners.add(ownerStock.getCarOwnerByOwnerId(s));
		return owners;
	}
	
	//update owner car details
	public boolean updateOwnerCarDetails(int ownerId, String newName, String newAddress)
	{
		return ownerStock.updateOwnerCarDetails(ownerId, newName, newAddress);
	}
	
	//renew license
	public boolean licenseRenewal(int carId, int paymentApprovalId)
	{
		for(Car c : getAllCars())
		{
			if (c.getLicensePlateCar() == carId)
				return c.licenseRenewal(paymentApprovalId);
		}
		return false;
	}
	

}
