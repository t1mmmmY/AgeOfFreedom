using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BattleStatus
{
	Win,
	Defeat,
	EnemyEscaped
}


public enum ShipStatus
{
	Alive,
	Crashed,
	Captured,
	Escaped
}


public class BattleResult
{
	public BattleStatus status;
	public List<BaseShip> shipsOnStart;
	public List<ShipStatus> shipsStatus;
	public BattleResult enemyResult;

	public BattleResult(List<BaseShip> ships)
	{
		shipsOnStart = ships;
		shipsStatus = new List<ShipStatus>();
		for (int i = 0; i < shipsOnStart.Count; i++)
		{
			shipsStatus.Add(ShipStatus.Alive);
		}
		status = BattleStatus.EnemyEscaped;
	}

	public void DestroyShip(BaseShip ship)
	{
		if (shipsOnStart.Contains(ship))
		{
			shipsStatus[shipsOnStart.IndexOf(ship)] = ShipStatus.Crashed;
		}
	}

	public void CaptureShip(BaseShip ship)
	{
		if (shipsOnStart.Contains(ship))
		{
			shipsStatus[shipsOnStart.IndexOf(ship)] = ShipStatus.Captured;
		}
	}

	public ShipStatus GetShipStatus(BaseShip ship)
	{
		if (shipsOnStart.Contains(ship))
		{
			return shipsStatus[shipsOnStart.IndexOf(ship)];
		}
		else
		{
			return ShipStatus.Crashed;
		}
	}

//	public bool IsShipAlive(BaseShip ship)
//	{
//		if (shipsOnStart.Contains(ship))
//		{
//			return shipsStatus[shipsOnStart.IndexOf(ship)] == ShipStatus.Alive;
//		}
//		else
//		{
//			return false;
//		}
//	}
}

public static class BattleSimulator
{

	public static void SimulateBattle(Fleet attacking, Fleet defender)
	{
		BattleResult attackingResult = new BattleResult(attacking.ships);
		BattleResult defenderResult = new BattleResult(defender.ships);


		//TODO
		System.Random rand = new System.Random();

		int attackingSailorsCount = 0;
		int defenderSailorsCount = 0;

		foreach (BaseShip ship in attacking.ships)
		{
			attackingSailorsCount += ship.team.characters.Count;
		}
		foreach (BaseShip ship in defender.ships)
		{
			defenderSailorsCount += ship.team.characters.Count;
		}

		if (attackingSailorsCount >= defenderSailorsCount)
		{
			//Attacking win
			attackingResult.status = BattleStatus.Win;
			defenderResult.status = BattleStatus.Defeat;

			for (int i = 0; i < defenderResult.shipsOnStart.Count; i++)
			{
				if (rand.Next(0, 2) == 0)
				{
					defenderResult.DestroyShip(defenderResult.shipsOnStart[i]);
				}
				else
				{
					defenderResult.CaptureShip(defenderResult.shipsOnStart[i]);
				}
			}

			attackingResult.enemyResult = defenderResult;
		}
		else
		{
			//Defender win
			attackingResult.status = BattleStatus.Defeat;
			defenderResult.status = BattleStatus.Win;

			for (int i = 0; i < attackingResult.shipsOnStart.Count; i++)
			{
				if (rand.Next(0, 2) == 0)
				{
					attackingResult.DestroyShip(attackingResult.shipsOnStart[i]);
				}
				else
				{
					attackingResult.CaptureShip(attackingResult.shipsOnStart[i]);
				}
			}

			defenderResult.enemyResult = attackingResult;
		}



		System.Threading.Thread.Sleep(1000);

		attacking.OnFinishFighting(attackingResult);
		defender.OnFinishFighting(defenderResult);
	}

}
