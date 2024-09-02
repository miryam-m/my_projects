package serverSide;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public class HandleServer extends Thread
{
	
	private ServerSocket server;
	private boolean isServerOn = false;
	
	public HandleServer(int portNumber) 
	{
		try 
		{
			server = new ServerSocket(portNumber);
			isServerOn = true;
			System.out.println("Server is running!");
		} 
		catch (IOException e) 
		{
			System.out.println("failed to connect to the server");
			e.printStackTrace();
		}
		
	}
	
	public void run() 
	{
		Socket client;
		while (isServerOn)
		{
			try 
			{
				System.out.println("Waiting for client connection...");
				client = server.accept();
				System.out.println("connect from client "+client.getLocalAddress()+", port number: "+client.getLocalPort());
				//HandleConnectedClient clientConnect = new HandleConnectedClient(client);
				//clientConnect.start();
				HandleClientCar clientCar = new HandleClientCar(client);
				clientCar.start();
			} 
			catch (IOException e) 
			{
				System.out.println("Cannot connect to server.");
				e.printStackTrace();
			}
			
		}		
		closeServer();
		
	}

	public boolean isServerOn() 
	{
		return isServerOn;
	}

	public synchronized void setServerOn(boolean isServerOn) 
	{
		this.isServerOn = isServerOn;
	}
	
	private void closeServer()
	{
		try 
		{
			server.close();
		} 
		catch (IOException e) 
		{
			System.out.println("Cannot close the server.");
			e.printStackTrace();
		}
	}
	
	


}
