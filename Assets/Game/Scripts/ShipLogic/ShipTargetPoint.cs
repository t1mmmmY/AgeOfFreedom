using UnityEngine;
using System.Collections;

public class ShipTargetPoint 
{
	private City targetCity;

	public bool SetTargetCity(City city)
	{
		targetCity = city;
		return targetCity != null ? true : false;
	}

	public City GetTargetCity()
	{
		return targetCity;
	}
}
