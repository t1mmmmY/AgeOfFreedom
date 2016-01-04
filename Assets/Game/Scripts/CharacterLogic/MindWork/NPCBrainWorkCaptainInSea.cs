using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class NPCBrain : BaseBrain
{
//	protected bool isMoving = false;
	float maxVisibilityRange = 2.0f;

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
		switch (result)
		{
			case BattleResult.Win:
				ResumeThink();
				break;
			case BattleResult.Defeat:
				break;
			case BattleResult.EnemyEscaped:
				ResumeThink();
				break;
		}

		base.OnFinishFighting(result);
	}


}
