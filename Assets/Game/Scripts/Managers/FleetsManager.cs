using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class FleetsManager 
{
	private static List<Fleet> allFleets;

	static FleetsManager()
	{
		allFleets = new List<Fleet>();
	}

	public static bool AddFleet(Fleet fleet)
	{
		if (!allFleets.Contains(fleet))
		{
			allFleets.Add(fleet);
			return true;
		}
		else
		{
			return false;
		}
	}

	public static bool RemoveFleet(Fleet fleet)
	{
		if (allFleets.Contains(fleet))
		{
			allFleets.Remove(fleet);
			return true;
		}
		else
		{
			return false;
		}
	}

	public static List<Fleet> GetNearestFleets(Fleet fleet, float maxDistance)
	{
		List<Fleet> nearestFleets = new List<Fleet>();

		foreach (Fleet someFleet in allFleets)
		{
			if (someFleet != fleet && 
				Vector2.Distance(someFleet.location.GetPosition(), fleet.location.GetPosition()) <= maxDistance &&
				someFleet.location.inTheSea)
			{
				nearestFleets.Add(someFleet);
			}
		}

		return nearestFleets;
	}

}
