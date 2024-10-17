package Entities;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

public class CarStock implements Serializable
{
	
	private Map<Car, Ownership> vehiclePool;
	
	public CarStock()
	{
		vehiclePool = new HashMap<>();
	}
	
	//get list of all the cars in the pool
	public List<Car> getAllCars()
	{
		List<Car> cars = new ArrayList<Car>();
		for (Car car: vehiclePool.keySet())
		{
			cars.add(new Car(car));
		}
		return cars;
	}
	
	//get car by licensePlateCar
	public Car getCarByLicensePlate(int licensePlateCar)
	{
		for (Car car: vehiclePool.keySet())
			if (car.getLicensePlateCar() == licensePlateCar)
				return car;
		return null;
	}
	
	//add car to the pool
	public boolean addCarToStock(Car car)
	{
		if (vehiclePool.get(car) != null)
			return false; //הרכב כבר קיים במאגר
		vehiclePool.put(car, new Ownership(car.getLicensePlateCar()));
		return true;
	}
	
	//get list of owners car id by licensePlateCar
	public List<Integer> getOwnerCarDetailsByCarId(int licensePlateCar)
	{
		for (Map.Entry<Car, Ownership> it : vehiclePool.entrySet())
		{
			if (it.getKey().getLicensePlateCar() == licensePlateCar)
				return it.getValue().getOwnerCurrList();
		}
		return null;
	}
	
	//update car details
	public boolean updateCarDetails(int licensePlateCar, String newColor)
	{
		for (Map.Entry<Car, Ownership> it : vehiclePool.entrySet())
		{
			if (it.getKey().getLicensePlateCar() == licensePlateCar)
			{
				it.getKey().setColor(newColor);
				return true;
			}
		}
		return false;
	}
	
	//change ownership
	public List<Integer> changeOwnership(int licensePlateCar, int newOwner)
	{
		for (Map.Entry<Car, Ownership> it : vehiclePool.entrySet())
			if (it.getKey().getLicensePlateCar() == licensePlateCar)
			{
				List<Integer> ownersToRemoveCarId = it.getValue().changeOwnership(newOwner);
				return ownersToRemoveCarId;
			}
		return null;
	}
	
	//get ownership history
	public Ownership getOwnershipHistory(int licensePlateCar)
	{
		for (Map.Entry<Car, Ownership> it : vehiclePool.entrySet())
			if (it.getKey().getLicensePlateCar() == licensePlateCar)
				return it.getValue();
		return null;
	}
	
	public boolean updateOwnership(OwnerCar newOwner, int licensePlateCar)
	{
		for (Map.Entry<Car, Ownership> it : vehiclePool.entrySet())
			if (it.getKey().getLicensePlateCar() == licensePlateCar)
			{
				it.getValue().setOwnerCurrList(newOwner.getId());
				return true;
			}
		return false;
	}
}
