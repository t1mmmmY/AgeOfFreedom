using UnityEngine;
using System.Collections;
using System;

//There is much work to do
public class Location : ICloneable
{
	City city;
	Vector2 position;
	City lastCity;

	public Location()
	{
		position = Vector2.zero;
	}


	public bool inCity
	{
		get { return city != null ? true : false; }
	}

	public bool inTheSea { get; private set; }


	public void EnterTheCity(City city)
	{
		this.city = city;
		inTheSea = false;
		position = city.position;
	}

	public void LeaveTheCity()
	{
		lastCity = this.city;
		if (city != null)
		{
			position = this.city.position;
		}
		this.city = null;
		inTheSea = true;
	}

	public City GetCity()
	{
		return city;
	}

	public City GetLastCity()
	{
		return lastCity;
	}

	public Vector2 GetPosition()
	{
		return position;
	}

	public Tavern GetTavern()
	{
		return city.tavern;
	}

	public void SetPosition(Vector2 newPos)
	{
		position = newPos;
	}

	public object Clone()
	{
		Location location = new Location();
		location.city = city;
		location.position = position;
		location.lastCity = lastCity;
		location.inTheSea = inTheSea;
		return location;
	}

}
