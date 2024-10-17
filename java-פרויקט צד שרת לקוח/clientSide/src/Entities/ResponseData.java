package Entities;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class ResponseData implements Serializable
{
	private List<Object> results;
	private List<String> messError;
	
	public ResponseData() 
	{
		results = new ArrayList<Object>();
		messError = new ArrayList<String>();
	}
	
	public Object getFirstResult()
	{
		return results.get(0);
	}

	public List<Object> getResults() 
	{
		return results;
	}

	public void setResults(List<Object> results)
	{
		this.results = results;
	}

	public List<String> getMessError() 
	{
		return messError;
	}

	public void setMessError(List<String> messError) 
	{
		this.messError = messError;
	}
	
	
	

}
