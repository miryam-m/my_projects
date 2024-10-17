package Entities;

import java.io.Serializable;

public class RequestData implements Serializable
{
	private String action;
	private Object data;
	
	public RequestData(String action) 
	{
		this.action = action;
	}
	
	public RequestData(String action, Object data)
	{
		this(action);
		this.data = data;
	}
	
	public String getAction()
	{
		return action;
	}
	
	public Object getData()
	{
		return data;
	}
	
	public void setAction(String action)
	{
		this.action = action;
	}
	
	public void setData(Object data)
	{
		this.data = data;
	}

}
