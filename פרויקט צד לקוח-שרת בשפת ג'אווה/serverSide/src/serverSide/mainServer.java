package serverSide;


public class mainServer {

	public static void main(String[] args) throws InterruptedException 
	{
		HandleServer server = new HandleServer(28);
		server.start();	
		
	}

}
