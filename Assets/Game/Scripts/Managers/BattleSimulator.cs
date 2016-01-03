using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BattleResult
{
	Win,
	Defeat,
	EnemyEscaped
}

public class FleetBattleResult
{
	public BattleResult result;
	public List<BaseShip> shipsOnStart;
	public List<bool> shipsStatus;

	public FleetBattleResult(List<BaseShip> ships)
	{
		shipsOnStart = ships;
		shipsStatus = new List<bool>();
		for (int i = 0; i < shipsOnStart.Count; i++)
		{
			shipsStatus.Add(true);
		}
		result = BattleResult.EnemyEscaped;
	}

	public void DestroyShip(BaseShip ship)
	{
		if (shipsOnStart.Contains(ship))
		{
			shipsStatus[shipsOnStart.IndexOf(ship)] = false;
//			shipsOnFinish.Remove(ship);
		}
	}

	public bool IsShipAlive(BaseShip ship)
	{
		if (shipsOnStart.Contains(ship))
		{
			return shipsStatus[shipsOnStart.IndexOf(ship)];
		}
		else
		{
			return false;
		}
//		return shipsOnFinish.Contains(ship);
	}
}

public static class BattleSimulator
{

	public static void SimulateBattle(Fleet attacking, Fleet defender)
	{
		FleetBattleResult attackingResult = new FleetBattleResult(attacking.ships);
		FleetBattleResult defenderResult = new FleetBattleResult(defender.ships);

		//TODO
		attackingResult.result = BattleResult.Win;
		defenderResult.result = BattleResult.Defeat;
		foreach (BaseShip ship in defenderResult.shipsOnStart)
		{
			defenderResult.DestroyShip(ship);
		}

		attacking.OnFinishFighting(attackingResult);
		defender.OnFinishFighting(defenderResult);
	}

}
