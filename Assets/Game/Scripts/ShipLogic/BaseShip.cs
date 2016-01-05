using UnityEngine;
using System.Collections;

public class BaseShip : Logic
{
	public ShipStats stats { get; private set; }
	public Team team { get; private set; }
	public Fleet fleet { get; private set; }

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

	public void ChangeTeam(Team team)
	{
		this.team = team;
	}


	public void OnFinishFighting(BattleStatus status, ShipStatus shipStatus)
	{
		//TODO
		if (shipStatus == ShipStatus.Crashed)
		{
//			Debug.Log("Ship crashed");
		}

		switch (status)
		{
			case BattleStatus.Win:
				break;
			case BattleStatus.Defeat:
				break;
			case BattleStatus.EnemyEscaped:
				break;
		}
	}

}
