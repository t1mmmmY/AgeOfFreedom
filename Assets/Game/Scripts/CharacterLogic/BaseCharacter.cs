using UnityEngine;
using System.Collections;

//BaseCharacter hande human logic. Contain brain and inventory. 
//[System.Serializable]
public class BaseCharacter : Logic
{
	public BaseBrain brain { get; private set; }
	public string name { get; private set; }


//	public string characterID { get; private set; }

	public Team team { get; set; }
	public Location location { get; private set; }


	//Items
	public int money { get; private set; }


	//Events
	public System.Action<BaseCharacter, Team> onChangeTeam;
	public System.Action<BaseCharacter, BaseShip> onBuyShip;
	public System.Action onTheBoard;
	public System.Action onShipChangeLocation;
	public System.Action onShipGetDestination;


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

		team.ship.onGetDestination += OnShipGetDestination;
		team.ship.onChangeLocation += OnShipChangeLocation;

		if (onTheBoard != null)
		{
			onTheBoard();
		}
	}

	public virtual void EnterTheTavern(Tavern tavern)
	{
//		location.EnterTheTavern(tavern);
	}

	public virtual void LeaveTheTavern()
	{
//		location.LeaveTheTavern();
	}


	BaseShip tempShip;

	public bool BuyShip(BaseShip ship)
	{
		//Buy ship if enough money
		team.SetShip(ship);


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


	private void OnChangeTeam()
	{
		Loom.QueueOnMainThread(OnChangeTeamEvent);
	}


	private void OnShipChangeLocation()
	{
		location.SetPosition(team.ship.location.GetPosition());

		if (onShipChangeLocation != null)
		{
			onShipChangeLocation();
		}
	}

	private void OnShipGetDestination()
	{
		if (team.ship.location.inCity)
		{
			EnterTheCity(team.ship.location.GetCity());
		}

		brain.OnGetDestination();

		if (onShipGetDestination != null)
		{
			onShipGetDestination();
		}

		team.ship.onGetDestination -= OnShipGetDestination;
		team.ship.onChangeLocation -= OnShipChangeLocation;
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
