
package clientSide;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

import Entities.*;

public class HandleClient {

    private InetAddress host;
    private Socket socket;
    private ObjectOutputStream output;
    private ObjectInputStream input;
    private boolean success = false;
    
    private static Scanner in = new Scanner(System.in);

    public HandleClient(int portNumber) {
        try 
        {
            System.out.println("Trying to connect to the server.");
            host = InetAddress.getLocalHost();
            socket = new Socket(host.getHostName(), portNumber);
            System.out.println("Client connected to " + socket.getLocalAddress() + ", port number " + socket.getLocalPort());
            success = socket.isConnected();
            output = new ObjectOutputStream(socket.getOutputStream());
            input = new ObjectInputStream(socket.getInputStream());
        } 
        catch (UnknownHostException e) 
        {
            System.out.println("The IP address of a host could not be determined.");
            e.printStackTrace();
        } catch (IOException e) 
        {
            System.out.println("Can't connect to the server.");
            e.printStackTrace();
        }
    }

    public void ClientMenu() {
    	if (!success)
		{
			System.out.println("Cannot display the menu because you are not connected to the server.");
			return;
		}
    	 System.out.println("Choose an action:");
         System.out.println("1. Add a vehicle");
         System.out.println("2. Add a car owner");
         System.out.println("3. Change ownership");
         System.out.println("4. Update vehicle details");
         System.out.println("5. Get a car ownership history");
         System.out.println("6. Get car details by car id");
         System.out.println("7. Get a list of all the cars");
         System.out.println("8. Get owner car details by owner id");
         System.out.println("9. Get a list of all the owners");
         System.out.println("10. Get owners car details by car id");
         System.out.println("11. Update owner car deatils");
         System.out.println("12. Renewal license plate");
         System.out.println("0. Exit");
         
        while (true) { 
            System.out.print("Enter your choice: ");

            int choice = in.nextInt();
            in.nextLine(); // לנקות את השורה

            switch (choice) {
                case 1:
                    addCarToRoad();
                    break;
                case 2:
                    addOwnerCar();
                    break;
                case 3:
                    changeOwnership();
                    break;
                case 4:
                    updateCarDetails();
                    break;
                case 5:
                    getOwnershipHistory();
                    break;
                case 6:
                	getCarDetails();
                	break;
                case 7:
                	getAllCars();
                	break;
                case 8:
                	getOwnerCarDetails();
                	break;
                case 9:
                	getAllOwners();
                	break;
                case 10:
                	getOwnersCarDetails();
                	break;
                case 11:
                	updateOwnerCarDetails();
                	break;
                case 12:
                	licenseRenewal();
                	break;
                case 0:
                	end();
                    System.out.println("Exited the program.");
                    try 
                    {
                        socket.close();
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                    return;
                default:
                    System.out.println("Choose a valid number!");
            }
        }
    }

    private void addCarToRoad()
    {
        System.out.println("Please enter the vehicle details: color, company, etc..");
        System.out.print("Enter vehicle color: ");
        String color = in.nextLine();
        System.out.print("Insert a company: ");
        String company = in.nextLine();
        Car newCar = new Car(color, company); // יצירת הרכב עם הפרטים
        RequestData requestData = new RequestData("addCarToRoad", newCar);
        sendRequest_getResponse(requestData);
    }

    private void addOwnerCar() 
    {
        System.out.println("Please enter the details of the vehicle owner: name, address, et.");
        System.out.print("Enter the vehicle owner's name: ");
        String name = in.nextLine();
        System.out.print("Enter the vehicle owner's address: ");
        String address = in.nextLine();
        System.out.print("Enter the vehicle owner's ID: ");
        int ownerId = in.nextInt();
        OwnerCar newOwner = new OwnerCar(ownerId, name, address);
        System.out.print("Enter the car id: ");
        int carId = in.nextInt();
        RequestData requestData = new RequestData("addOwnerCar", List.of(newOwner, carId));
        sendRequest_getResponse(requestData);
    }

    private void changeOwnership() 
    {
        System.out.print("Please enter the car ID you want to change the ownership: ");
        int carId = in.nextInt();
        System.out.print("Please enter the new owner ID: ");
        int ownerId = in.nextInt();
        RequestData requestData = new RequestData("changeOwnership", List.of(ownerId, carId));
        sendRequest_getResponse(requestData);
    }

    private void updateCarDetails()
    {
        System.out.println("Please enter the details of the car to update.");
        System.out.print("Enter the car ID you wish to update: ");
        int carId = in.nextInt();
        in.nextLine(); 
        System.out.print("Enter the new color: ");
        String newColor = in.nextLine();
        RequestData requestData = new RequestData("updateCarDetails", List.of(carId, newColor));
        sendRequest_getResponse(requestData);
    }

    private void getOwnershipHistory()
    {
        System.out.print("Please enter the car ID: ");
        int carId = in.nextInt();
        RequestData requestData = new RequestData("getOwnershipHistoryByCarId", carId);
        sendRequest_getResponse(requestData);
    }
    
    private void getCarDetails()
    {
    	System.out.print("Please enter the car ID: ");
    	int carId = in.nextInt();
    	RequestData req = new RequestData("getCarDetails", carId);
    	sendRequest_getResponse(req);
    }
    
    private void getAllCars()
    {
    	sendRequest_getResponse(new RequestData("getAllCars"));
    }
    
    private void getOwnerCarDetails()
    {
    	System.out.print("Please enter the owner ID: ");
    	int ownerId = in.nextInt();
    	sendRequest_getResponse(new RequestData("getOwnerCarDetails", ownerId));
    }
    
    private void getAllOwners()
    {
    	sendRequest_getResponse(new RequestData("getAllOwners"));
    }
    
    private void getOwnersCarDetails()
    {
    	System.out.println("Please enter the car ID: ");
    	int carId = in.nextInt();
    	sendRequest_getResponse(new RequestData("getOwnersCarDetails", carId));
    }
    
    private void updateOwnerCarDetails()
    {
    	System.out.print("Enter the owner id you want to update: ");
    	int ownerId = in.nextInt();
    	System.out.print("Enter the new name: ");
    	String name = in.next();
    	System.out.print("Enter the new address: ");
    	String address = in.next();
    	sendRequest_getResponse(new RequestData("updateOwnerCarDetails", List.of(ownerId, name, address)));
    }
    
    private void licenseRenewal()
    {
    	System.out.print("Please enter the car ID you want to renewal: ");
    	int carId = in.nextInt();
    	System.out.print("Please enter payment code: ");
    	int payment = in.nextInt();
    	sendRequest_getResponse(new RequestData("licenseRenewal", List.of(carId, payment)));
    }
    private void end()
	{
		RequestData req = new RequestData("complete");
		sendRequest_getResponse(req);
		try 
		{
			input.close();
			output.close();
		} 
		catch (IOException e) 
		{
			System.out.println("Cannot close the input / output stream.");
			e.printStackTrace();
		}
	}

    private void sendRequest_getResponse(RequestData requestData) {
        try {
            output.writeObject(requestData);
            if (!(requestData.getAction().equals("complete")))
            {
            	ResponseData responseData = (ResponseData) input.readObject();

                // מדפיס תוצאות או שגיאות
                if (!responseData.getResults().isEmpty()) 
                {
                    for (Object result : responseData.getResults()) 
                    {
                        System.out.println(result.toString());
                    }
                }
                if (!responseData.getMessError().isEmpty()) 
                {
                    for (String error : responseData.getMessError()) 
                    {
                        System.err.println("Error: " + error);
                    }
                }
             
            }         
        } 
        catch (IOException | ClassNotFoundException e) {
            e.printStackTrace();
        }
    }
}