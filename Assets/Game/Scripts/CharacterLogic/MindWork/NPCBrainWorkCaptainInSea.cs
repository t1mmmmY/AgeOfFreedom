using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class NPCBrain : BaseBrain
{
	protected bool isMoving = false;

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
		if (!isMoving)
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
		Debug.Log("Trading");
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



	void MoveToOtherCity()
	{
		isMoving = true;
		Loom.QueueOnMainThread(_MoveToOtherCity);
	}

	void _MoveToOtherCity()
	{
		ShipTargetPoint targetPoint = new ShipTargetPoint();
		targetPoint.SetTargetCity(CitiesManager.Instance.GetRandomCity(character.location.GetLastCity()));

		character.team.ship.MoveTo(targetPoint);
	}



	public override void OnGetDestination()
	{
		isMoving = false;
		failedRecruiting = 0;

		base.OnGetDestination();
	}

}
