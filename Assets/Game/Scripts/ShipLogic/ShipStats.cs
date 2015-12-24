using UnityEngine;
using System.Collections;

//Ship stats like cannons count, speed, badewind etc...
public class ShipStats
{
	public int cannonsCount { get; private set; }

	public ShipStats()
	{
		cannonsCount = 12;
	}

	public ShipStats(ShipStats stats)
	{
		cannonsCount = stats.cannonsCount;
	}
}
