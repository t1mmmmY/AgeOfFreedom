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

		for (int i = 0; i < allFleets.Count; i++)
		{
			if (allFleets[i] != fleet && 
				Vector2.Distance(allFleets[i].location.GetPosition(), fleet.location.GetPosition()) <= maxDistance &&
				allFleets[i].location.inTheSea &&
				!allFleets[i].fighting)
			{
				nearestFleets.Add(allFleets[i]);
			}
		}

		return nearestFleets;
	}

}
