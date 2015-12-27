using UnityEngine;
using System.Collections;

public class BaseShip : Logic
{
	public ShipStats stats { get; private set; }
	public Team team { get; private set; }
	public Location location { get; private set; }


	public override void Init()
	{
		base.Init();

		stats = new ShipStats();
	}

	public void Init(ShipStats stats)
	{
		base.Init();

		this.stats = new ShipStats(stats);
	}

	public void ChangeTeamTeam(Team team)
	{
		this.team = team;
	}

	public bool MoveTo(ShipTargetPoint targetPoint)
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
