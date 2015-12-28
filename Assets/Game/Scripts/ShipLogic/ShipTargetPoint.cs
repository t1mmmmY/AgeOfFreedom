using UnityEngine;
using System.Collections;

public class ShipTargetPoint 
{
	private City targetCity;
	public Vector2 targetPoint { get; private set; }

	public bool SetTargetCity(City city)
	{
		targetCity = city;
		targetPoint = targetCity.position;
		return targetCity != null ? true : false;
	}

	public City GetTargetCity()
	{
		return targetCity;
	}
}
