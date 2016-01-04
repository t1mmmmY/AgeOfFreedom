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
	}
}

public static class BattleSimulator
{

	public static void SimulateBattle(Fleet attacking, Fleet defender)
	{
		FleetBattleResult attackingResult = new FleetBattleResult(attacking.ships);
		FleetBattleResult defenderResult = new FleetBattleResult(defender.ships);

		//TODO
		if (attacking.admiral.team.characters.Count >= defender.admiral.team.characters.Count)
		{
			attackingResult.result = BattleResult.Win;
			defenderResult.result = BattleResult.Defeat;

			for (int i = 0; i < defenderResult.shipsOnStart.Count; i++)
			{
				defenderResult.DestroyShip(defenderResult.shipsOnStart[i]);
			}
		}
		else
		{
			attackingResult.result = BattleResult.Defeat;
			defenderResult.result = BattleResult.Win;

			for (int i = 0; i < attackingResult.shipsOnStart.Count; i++)
			{
				attackingResult.DestroyShip(attackingResult.shipsOnStart[i]);
			}
		}


		System.Threading.Thread.Sleep(1000);

		attacking.OnFinishFighting(attackingResult);
		defender.OnFinishFighting(defenderResult);
	}

}
