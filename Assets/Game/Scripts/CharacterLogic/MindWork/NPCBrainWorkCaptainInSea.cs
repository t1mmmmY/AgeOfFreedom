﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class NPCBrain : BaseBrain
{
//	protected bool isMoving = false;
	float maxVisibilityRange = 3.0f;

	enum Capabilitiy
	{
		Trading,
		Raven,
		Revenge
	}


	void DoWorkInSea()
	{
		ChooseWhatToDo();
	}

	void ChooseWhatToDo()
	{
		//If ship is not moving
		if (!character.fleet.isMoving)
		{
			List<KeyValuePair<Capabilitiy, int>> capabilities = new List<KeyValuePair<Capabilitiy, int>>();

			capabilities.Add(new KeyValuePair<Capabilitiy, int>(Capabilitiy.Trading, HowMuchWantToTrade()));
			capabilities.Add(new KeyValuePair<Capabilitiy, int>(Capabilitiy.Raven, HowMuchWantToRaven()));
			capabilities.Add(new KeyValuePair<Capabilitiy, int>(Capabilitiy.Revenge, HowMuchWantToRevenge()));

			Capabilitiy decision = GetPriorityOption(capabilities);

			switch (decision)
			{
				case Capabilitiy.Trading:
					Trading();
					break;
				case Capabilitiy.Raven:
					Raven();
					break;
				case Capabilitiy.Revenge:
					DoRevenge();
					break;
			}
		}
		else
		{
			
		}
	}

	Capabilitiy GetPriorityOption(List<KeyValuePair<Capabilitiy, int>> capabilities)
	{
		capabilities.Sort(
			delegate(KeyValuePair<Capabilitiy, int> firstPair,
				KeyValuePair<Capabilitiy, int> nextPair)
		{
			return nextPair.Value.CompareTo(firstPair.Value);
		});

		return capabilities[0].Key;
	}


	int HowMuchWantToTrade()
	{
		return 1;
	}

	int HowMuchWantToRaven()
	{
		return 0;
	}

	int HowMuchWantToRevenge()
	{
		return 0;
	}


	//Trading. If want to
	void Trading()
	{
//		Debug.Log("Trading");
		MoveToOtherCity();
	}

	//Hunting other ships
	void Raven()
	{
		Debug.Log("Raven");
	}

	//Hunting for blood enemy
	void DoRevenge()
	{
		Debug.Log("DoRevenge");
	}


	void SeeTheFleets(List<Fleet> nearestFleets)
	{
		//TODO
		//What to do when see the fleet

		System.Random rand = new System.Random();
		int targetFleetNumber = rand.Next(0, nearestFleets.Count);
		AttackTheFleet(nearestFleets[targetFleetNumber]);
	}


	void MoveToOtherCity()
	{
//		if (!character.fleet.fighting)
//		{
			_MoveToOtherCity();
//			Loom.QueueOnMainThread(_MoveToOtherCity);
//		}
	}

	void _MoveToOtherCity()
	{
		ShipTargetPoint targetPoint = new ShipTargetPoint();
		targetPoint.SetTargetCity(CitiesManager.Instance.GetRandomCity(character.location.GetLastCity()));

		character.fleet.MoveTo(targetPoint);
	}

	void AttackTheFleet(Fleet otherFleet)
	{
		Debug.Log("FIGHT!");
		this.character.fleet.Fight(otherFleet, true);
		otherFleet.Fight(this.character.fleet, false);
	}



	public override void OnGetDestination()
	{
		if (IsCaptain())
		{
			failedRecruiting = 0;
		}

		base.OnGetDestination();
	}

	public override void OnChangePosition()
	{
		if (IsCaptain())
		{
			if (!character.fleet.fighting)
			{
				List<Fleet> nearestFleets = FleetsManager.GetNearestFleets(character.fleet, maxVisibilityRange);
				if (nearestFleets.Count > 0)
				{
					Debug.Log("I see a ship!");
					SeeTheFleets(nearestFleets);

				}
			}

		}
//		else
//		{
//			Debug.Log("I'm not a captain");
//		}

		base.OnChangePosition();
	}


	public override void OnFighting()
	{
		StopThink();

		base.OnFighting();
	}

	public override void OnFinishFighting(BattleResult result)
	{
		if (IsCaptain())
		{
			switch (result.status)
			{
				case BattleStatus.Win:
					DecideWhatToDoWithDefeated(result);
					break;
				case BattleStatus.Defeat:
					
					break;
				case BattleStatus.EnemyEscaped:
					ResumeThink();
					break;
			}
		}

		base.OnFinishFighting(result);
	}

	private void DecideWhatToDoWithDefeated(BattleResult result)
	{
		//TODO
		BattleResult enemyResult = result.enemyResult;
		int capturedSailors = 0;
		for (int shipNumber = 0; shipNumber < enemyResult.shipsOnStart.Count; shipNumber++)
		{
			if (enemyResult.GetShipStatus(enemyResult.shipsOnStart[shipNumber]) == ShipStatus.Captured)
			{
				//Recruit from enemy team
				for (int i = 0; i < enemyResult.shipsOnStart[shipNumber].team.characters.Count; i++)
				{
					if (enemyResult.shipsOnStart[shipNumber].team.characters[i].ProposeMercy(this.character))
					{
						//Add to my team
						this.character.team.Recruit(enemyResult.shipsOnStart[shipNumber].team.characters[i]);
						capturedSailors++;
					}
					else
					{
						//kill
						enemyResult.shipsOnStart[shipNumber].team.characters[i].Kill();
					}
				}
			}
		}

		ResumeThink();
		Debug.LogWarning("Captured sailors " + capturedSailors);
	}

}
