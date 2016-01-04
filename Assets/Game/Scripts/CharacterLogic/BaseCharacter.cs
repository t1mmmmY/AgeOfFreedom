using UnityEngine;
using System.Collections;

//BaseCharacter hande human logic. Contain brain and inventory. 
//[System.Serializable]
public class BaseCharacter : Logic
{
	public BaseBrain brain { get; private set; }
	public string name { get; private set; }


//	public string characterID { get; private set; }

	public bool isCaptain { get; private set; }
	public Team team { get; private set; }
	public Location location { get; private set; }
	public Fleet fleet { get; private set; }


	//Items
	public int money { get; private set; }


	//Events
	public System.Action<BaseCharacter, Team> onChangeTeam;
	public System.Action<BaseCharacter, BaseShip> onBuyShip;
	public System.Action onTheBoard;
	public System.Action onShipChangeLocation;
	public System.Action onShipGetDestination;
	public System.Action onFighting;

	public BaseCharacter()
	{
		name = "Sailor " + System.DateTime.UtcNow.Millisecond.ToString();

		location = new Location();
	}

	public override void Init()
	{
		base.Init();
	}

	public void InitBrain(BaseBrain brain)
	{
		this.brain = brain;
		Init(brain.ID);

		this.brain.onChangeTeam += OnChangeTeam;
	}

	public void EnterTheCity(City city)
	{
		location.EnterTheCity(city);
	}

	public void LeaveTheCity()
	{
//		OnTheBoard();
//		location.LeaveTheCity();
	}

	public void OnTheBoard()
	{
		location.LeaveTheCity();

		fleet.onGetDestination += OnFleetGetDestination;
		fleet.onChangeLocation += OnFleetChangeLocation;
		fleet.onFighting += OnFighting ;

		if (onTheBoard != null)
		{
			onTheBoard();
		}
	}

	//Probably don't need this
	public virtual void EnterTheTavern(Tavern tavern)
	{
//		location.EnterTheTavern(tavern);
	}

	//Probably don't need this
	public virtual void LeaveTheTavern()
	{
//		location.LeaveTheTavern();
	}

	public void BecomeACaptain()
	{
		isCaptain = true;
		team = new Team(this);
		fleet = new Fleet(this);

		Loom.QueueOnMainThread(_CreateFleetVisual);
	}

	public void LostFleet()
	{
		StopBeingCaptain();
	}

	private void _CreateFleetVisual()
	{
		if (TestGameController.Instance.showFleets)
		{
			FleetsVisualizationManager.Instance.CreateFleet(fleet, location.GetCity().GetRect());
		}
	}

	void StopBeingCaptain()
	{
		isCaptain = false;
		fleet = null;
		team.SetCaptain(null);
		TeamsManager.Instance.RemoveTeam(team);
	}

	BaseShip tempShip;

	public bool BuyShip(BaseShip ship)
	{
		//Buy ship if enough money
		team.SetShip(ship);
		fleet.AddShip(ship);

		tempShip = ship;
		Loom.QueueOnMainThread(OnBuyShipEvent);

		return true;
	}

	private void OnBuyShipEvent()
	{
		if (onBuyShip != null)
		{
			onBuyShip(this, tempShip);
		}
		tempShip = null;
	}

	public void ChangeTeam(Team newTeam)
	{
		team = newTeam;
		if (team != null)
		{
			fleet = team.captain.fleet;
		}
	}

	private void OnChangeTeam()
	{
		Loom.QueueOnMainThread(OnChangeTeamEvent);
	}


	private void OnFleetChangeLocation()
	{
//		if (fleet == null)
//		{
//			//TODO
//			//This is definitely wrong
//			return;
//		}


		location.SetPosition(fleet.location.GetPosition());

		brain.OnChangePosition();

		if (onShipChangeLocation != null)
		{
			onShipChangeLocation();
		}
	}

	private void OnFleetGetDestination()
	{
//		if (fleet == null)
//		{
//			Debug.LogWarning("This shouldn't happen!");
//		}
//		else
		{
			if (fleet.location.inCity)
			{
				EnterTheCity(fleet.location.GetCity());
			}
		}

		brain.OnGetDestination();

		if (onShipGetDestination != null)
		{
			onShipGetDestination();
		}

//		if (fleet != null)
//		{
			fleet.onGetDestination -= OnFleetGetDestination;
			fleet.onChangeLocation -= OnFleetChangeLocation;
//		}
	}

	private void OnFighting()
	{
		fleet.onFinishFighting += OnFinishFighting;
		fleet.onGetDestination -= OnFleetGetDestination;
		fleet.onChangeLocation -= OnFleetChangeLocation;

		brain.OnFighting();

		if (onFighting != null)
		{
			onFighting();
		}

		fleet.onFighting -= OnFighting;
	}

	private void OnFinishFighting(BattleResult result)
	{
		if (fleet != null)
		{
			fleet.onFinishFighting -= OnFinishFighting;
		}

		brain.OnFinishFighting(result);

		if (isCaptain)
		{
			switch (result)
			{
				case BattleResult.Win:
					break;
				case BattleResult.Defeat:
					break;
				case BattleResult.EnemyEscaped:
					break;
			}
		}
	}

	private void OnChangeTeamEvent()
	{
		if (onChangeTeam != null)
		{
			onChangeTeam(this, team);
		}
	}


//	public static bool operator ==(BaseCharacter character1, BaseCharacter character2)
//	{
//		if (character1 == null && character2 == null)
//		{
//			return true;
//		}
//		else if (character1 == null || character2 == null)
//		{
//			return false;
//		}
//		else
//		{
//			return character1.characterID == character2.characterID;
//		}
//	}
//
//	public static bool operator !=(BaseCharacter character1, BaseCharacter character2)
//	{
//		return !(character1 == character2);
//	}
}
