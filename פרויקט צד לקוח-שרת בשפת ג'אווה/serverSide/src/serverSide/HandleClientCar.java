package serverSide;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.text.SimpleDateFormat;
import java.time.LocalTime;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import Entities.*;

public class HandleClientCar extends Thread
{
    private Socket clientSocket;
    private DepartmentTransportation office;

    public HandleClientCar(Socket clientSocket) 
    {
        this.clientSocket = clientSocket;
        office = new DepartmentTransportation();
    }

    public void run() 
    {
        try 
        {
            handleClient();
        } 
        catch (Exception e) 
        {
            e.printStackTrace();
        } 
        try 
        {
			clientSocket.close();
		} 
        catch (IOException e) 
        {
			System.out.println("Cannot clost the client socket.");
			e.printStackTrace();
		} 
    }

    private void handleClient() throws ClassNotFoundException 
    {
        try (ObjectInputStream inFromClient = new ObjectInputStream(clientSocket.getInputStream());
             ObjectOutputStream out = new ObjectOutputStream(clientSocket.getOutputStream())) 
        {
        	
            while (true) 
            {
                RequestData requestData = (RequestData) inFromClient.readObject();

                if ("complete".equals(requestData.getAction())) 
                    break;

                ResponseData responseData = handleClientRequest(requestData);
                out.writeObject(responseData);
            }
            System.out.println("Client wants to leave, bye");
            inFromClient.close();
            out.close();

        } 
        catch (IOException e)
        {
            e.printStackTrace();
        } 

    }

    private ResponseData handleClientRequest(RequestData requestData) 
    {
        ResponseData responseData = new ResponseData();
        boolean success;


        switch (requestData.getAction()) 
        {
            case "addCarToRoad":
            {
            	Car c = (Car)requestData.getData();
            	success = office.addCarToRoad(c);
            	if (success)
            		responseData.getResults().add("The vehicle "+c.getLicensePlateCar()+" was added successfully.");
            	else
            		responseData.getMessError().add("Failed to add vehicle "+c.getLicensePlateCar()+" .");
            	break;
            }
                
            case "addOwnerCar":
            {
            	List<Object> lst = (List<Object>)requestData.getData();
            	OwnerCar own = (OwnerCar)lst.get(0);
            	int  carId = (int)lst.get(1);
            	success = office.addOnwerCar(own, carId);
            	if (success)
            		responseData.getResults().add("The owner "+own.getId()+" was added successfully.");
            	else
            		responseData.getMessError().add("Failed to add owner.");
                break;
            }
                
            case "changeOwnership":
            {
            	List<Integer>lst = (List<Integer>)requestData.getData();
            	int ownerId = lst.get(0);
            	int carId = lst.get(1);
            	success = office.changeOwnership(ownerId, carId);
            	if (success)
            		responseData.getResults().add("The change of ownership for vehicle "+carId+" was successfully carried out.");
            	else
            		responseData.getMessError().add("Change of ownership for vehicle "+carId+" failed.");
                break;
            }
            
            case "updateCarDetails":
            {
                List<Object> ls = (List<Object>)requestData.getData();
                int carId = (int)ls.get(0);
                String color = (String)ls.get(1);
                success = office.updateCarDetails(carId, color);
                if (success)
                    responseData.getResults().add("The vehicle "+carId+" details have been updated successfully.");
                else
                    responseData.getMessError().add("Failed to update vehicle "+carId+" details.");
                break;
            }
            
            case "getOwnershipHistoryByCarId":
            {
            	int carId = (int)requestData.getData();
            	List<OwnerCar> owners = office.getOwnershipHistoryByCarId(carId);
            	if (owners != null)
            		responseData.getResults().add(owners);
            	else
            		responseData.getMessError().add("Failed to get ownership history by car id "+carId+" .");
            	break;
            }
            
            case "getCarDetails":
            {
            	int carId = (int)requestData.getData();
            	Car c = office.getCarDetails(carId);
            	if (c != null)
            		responseData.getResults().add(c);
            	else
            		responseData.getMessError().add("Failed to retrieve vehicle details "+carId+" .");
            	break;
            }
            
            case "getAllCars":
            {
            	List<Car> cars = office.getAllCars();
            	if (cars != null)
            		responseData.getResults().add(cars);
            	else
            		responseData.getMessError().add("Failed to retrieve details of all vehicles.");
            	break;
            }
            
            case "getOwnerCarDetails":
            {
            	int ownerId = (int)requestData.getData();
            	OwnerCar o = office.getOwnerCarDetails(ownerId);
            	if (o != null)
            		responseData.getResults().add(o);
            	else
            		responseData.getMessError().add("Failed to retrieve owner details "+ownerId+" .");
            	break;
            }
            
            case "getAllOwners":
            {
            	List<OwnerCar> owners = office.getAllOwners();
            	if (owners != null)
            		responseData.getResults().add(owners);
            	else
            		responseData.getMessError().add("Failed to retrieve all owner details.");
            	break;
            }
            
            case "getOwnersCarDetails":
            {
            	int carId = (int)requestData.getData();
            	List<OwnerCar> owners = office.getOwnersCarDetails(carId);
            	if (owners != null)
            		responseData.getResults().add(owners);
            	else
            		responseData.getMessError().add("Failed to retrieve the details of the owner of vehicle number "+carId+" .");
            	break;
            }
            
            case "updateOwnerCarDetails":
            {
            	List<Object>ls = (List<Object>)requestData.getData();
            	int ownerId = (int)ls.get(0);
            	String name = (String)ls.get(1);
            	String address = (String)ls.get(2);
            	success = office.updateOwnerCarDetails(ownerId, name, address);
            	if (success)
            		responseData.getResults().add("Owner details "+ownerId+" have been successfully updated.");
            	else
            		responseData.getMessError().add("Failed to update owner details "+ownerId+" .");
            	break;
            }
            
            case "licenseRenewal":
            {
            	List<Integer> lst = (List<Integer>)requestData.getData();
            	int carId = lst.get(0);
            	int payment = lst.get(1);
            	success = office.licenseRenewal(carId, payment);
            	if (success)
            		responseData.getResults().add("The renewal of the license for vehicle "+carId+" was successfully carried out.");
            	else
            		responseData.getMessError().add("The renewal of the license for vehicle "+carId+" failed.");
            	break;
            }
            	
            default:
            {
            	responseData.getMessError().add("Unknown action: " + requestData.getAction());
                break;
            }
                
        }
		return responseData;
    }
}
