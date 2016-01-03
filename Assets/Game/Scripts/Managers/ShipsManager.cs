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

	public static bool RemoveShip(BaseShip ship)
	{
		if (allShips.Contains(ship))
		{
			allShips.Remove(ship);
			return true;
		}
		else
		{
			return false;
		}
	}

}
