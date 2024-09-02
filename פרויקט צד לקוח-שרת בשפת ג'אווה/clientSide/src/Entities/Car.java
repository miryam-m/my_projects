package Entities;
import java.io.Serializable;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;


public class Car implements Serializable
{
	//static members
	private static int count = 100000000;
 
	private int licensePlateCar;  //קוד רכב
	private LocalDate licensingDate;  //תאריך רישוי
	private LocalDate testDate; //תאריך טסט
	private String color;  //צבע
	private String company;  //חברה
	
		
	public Car(String color, String company) 
	{
		licensePlateCar = count++;
		licensingDate = LocalDate.now();
		testDate = licensingDate.plusYears(3);
		this.color = color;
		this.company = company;
	}
	
	public Car(Car car)
	{
		this.licensePlateCar = car.licensePlateCar;
		this.licensingDate = car.licensingDate;
		this.testDate = car.testDate;
		this.color = car.color;
		this.company = car.company;
	}
	
	public boolean licenseRenewal(int paymentApprovalId)
	{
		if (checkPaymentApprovalId(paymentApprovalId))
		{
			this.licensingDate = this.licensingDate.plusYears(1);
			return true;
		}
		return false;
	}
	
	private boolean checkPaymentApprovalId(int paymentApprovalId)
	{
		return true;
	}
	
	public int getLicensePlateCar() 
	{
		return licensePlateCar;
	}
	public void setLicensePlateCar(int licensePlateCar) 
	{
		this.licensePlateCar = licensePlateCar;
	}
	public LocalDate getLicensingDate() 
	{
		return licensingDate;
	}
	public void setLicensingDate(LocalDate licensingDate) 
	{
		this.licensingDate = licensingDate;
	}
	public LocalDate getTestDate() 
	{
		return testDate;
	}
	public void setTestDate(LocalDate testDate) 
	{
		this.testDate = testDate;
	}
	public String getColor() 
	{
		return color;
	}
	public void setColor(String color) 
	{
		this.color = color;
	}
	public String getCompany() 
	{
		return company;
	}
	public void setCompany(String company) 
	{
		this.company = company;
	}
	
	@Override
	public String toString() {
        return "Car{" +
                "licensePlateCar=" + licensePlateCar +
                ", licensingDate=" + licensingDate +
                ", testDate=" + testDate +
                ", color='" + color + '\'' +
                ", company='" + company + '\'' +
                '}';
    }


}
