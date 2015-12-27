using UnityEngine;
using System.Collections;

//There is much work to do
public class Location
{
	City city;
//	Tavern tavern;

	public Location()
	{
//		tavern = null;
	}

//	public bool inTavern
//	{
//		get { return tavern != null ? true : false; }
//	}

	public bool inCity
	{
		get { return city != null ? true : false; }
	}

	public bool inTheSea
	{
		get { return false; }
	}

	public City GetCity()
	{
		return city;
	}

	public void EnterTheCity(City city)
	{
		this.city = city;
//		this.tavern = city.tavern;
	}

	public void LeaveTheCity()
	{
		this.city = null;
	}



	public Tavern GetTavern()
	{
		return city.tavern;
	}

//	public void EnterTheTavern(Tavern tavern)
//	{
//		this.tavern = tavern;
//	}
//
//	public void LeaveTheTavern()
//	{
//		this.tavern = null;
//	}
}
