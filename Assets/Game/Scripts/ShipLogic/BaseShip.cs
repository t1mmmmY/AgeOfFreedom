using UnityEngine;
using System.Collections;

public class BaseShip 
{
	public ShipStats stats { get; private set; }
	public Team team { get; private set; }
	public Location location { get; private set; }


	public BaseShip()
	{
		stats = new ShipStats();
	}

	public BaseShip(ShipStats stats)
	{
		this.stats = new ShipStats(stats);
	}


	public void InitWithTeam(Team team)
	{
		this.team = team;
	}

	private bool MoveTo(ShipTargetPoint targetPoint)
	{
		City targetCity = targetPoint.GetTargetCity();
		if (targetCity != null)
		{
			
			return true;
		}
		else
		{
			return false;
		}
	}

}
