using UnityEngine;
using System.Collections;

//There is much work to do
public class Location
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
	}

	public void LeaveTheCity()
	{
		lastCity = this.city;
		position = city.position;
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

}
