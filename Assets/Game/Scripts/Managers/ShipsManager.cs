using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ShipsManager 
{
	private static List<BaseShip> allShips;

	static ShipsManager()
	{
		allShips = new List<BaseShip>();
	}

	public static bool AddShip(BaseShip ship)
	{
		if (!allShips.Contains(ship))
		{
			allShips.Add(ship);
			return true;
		}
		else
		{
			return false;
		}
	}

	public static List<BaseShip> GetNearestShips(BaseShip ship, float maxDistance)
	{
		List<BaseShip> nearestShips = new List<BaseShip>();

		foreach (BaseShip someShip in allShips)
		{
			if (someShip != ship && 
				Vector2.Distance(someShip.location.GetPosition(), ship.location.GetPosition()) <= maxDistance &&
				someShip.location.inTheSea)
			{
				nearestShips.Add(someShip);
			}
		}

		return nearestShips;
	}

}
