using UnityEngine;
using System.Collections;

public enum ButtleResult
{
	Win,
	Defeat,
	EnemyEscaped
}

public static class BattleSimulator
{
	public static void SimulateButtle(BaseShip attacking, BaseShip defender)
	{
		//TODO
		attacking.OnFinishFighting(ButtleResult.Win);
		defender.OnFinishFighting(ButtleResult.Defeat);
	}

}
