using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FleetStats
{
	public float speed { get; private set; }

	private Fleet fleet;

	public FleetStats(Fleet fleet)
	{
		this.fleet = fleet;
	}

	public void SetSpeed(float speed)
	{
		this.speed = speed;
	}
}


public class Fleet : Logic
{
	public BaseCharacter admiral;
	public BaseShip flagship { get; private set; }
	public List<BaseShip> ships { get; private set; }
	FleetStats stats;


	public Location location { get; private set; }
	Vector2 startPosition;
	public ShipTargetPoint destination;
	public System.Action onChangeLocation;
	public System.Action onGetDestination;
	public System.Action onFighting;
	public System.Action<BattleResult> onFinishFighting;
	public System.Action onLostFleet;

	public bool isMoving { get; private set; }
	bool stopMoving = false;
	public bool fighting { get; private set; }

	private string name;


	public bool isFleetEmpty
	{
		get
		{
			return ships.Count == 0 ? true : false;
		}
	}


	public Fleet(BaseCharacter admiral)
	{
		this.admiral = admiral;
		ships = new List<BaseShip>();
		stats = new FleetStats(this);
		fighting = false;
		isMoving = false;
		location = (Location)admiral.location.Clone();
		name = "Fleet " + FleetsVisualizationManager.Instance.GetNumber();

		FleetsManager.AddFleet(this);
	}

	public override void Init()
	{
		base.Init();
	}

	public void ChangeFlagship(BaseShip ship)
	{
		flagship = ship;
		if (!ships.Contains(flagship))
		{
			ships.Add(flagship);
		}

		stats.SetSpeed(flagship.stats.speed);
	}

	public bool AddShip(BaseShip ship)
	{
		if (!ships.Contains(ship))
		{
			ships.Add(ship);
			if (ships.Count == 1)
			{
				ChangeFlagship(ship);
			}
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool RemoveShip(BaseShip ship)
	{
		if (ships.Contains(ship))
		{
			ships.Remove(ship);
			if (ships.Count == 0)
			{
				//Fleet is empty
				Debug.Log("LostFleet");
				LostFleet();
			}
			return true;
		}
		else
		{
			return false;
		}
	}

	void LostFleet()
	{
		admiral.LostFleet();
		FleetsManager.RemoveFleet(this);

		Loom.QueueOnMainThread(_LostFleet);
	}

	void _LostFleet()
	{
		if (onLostFleet != null)
		{
			onLostFleet();
		}
	}


	public void LeaveTheCity()
	{
		location.LeaveTheCity();
	}

	public bool MoveTo(ShipTargetPoint targetPoint)
	{
		if (isMoving)
		{
			Debug.LogWarning("I moving already!");
			return false;
		}

//		Debug.Log(name + " Move To");

		stopMoving = false;
		isMoving = true;

		destination = targetPoint;
		startPosition = location.GetPosition();
		Loom.RunAsync(Moving);

		return true;
	}

	void StopMoving()
	{
		stopMoving = true;
		isMoving = false;
	}

	void Moving()
	{
		float elapsedTime = 0;
		int timeTick = 10;
		float distance = Vector2.Distance(startPosition, destination.targetPoint);

		do
		{
			isMoving = true;
			Vector2 newPosition = Vector2.Lerp(startPosition, destination.targetPoint, elapsedTime);
			location.SetPosition(newPosition);

			System.Threading.Thread.Sleep(timeTick);
			elapsedTime += (float)timeTick / 1000.0f * stats.speed / distance * 5;

			if (!stopMoving && !fighting)
			{
//				_OnChangeLocation();
				Loom.QueueOnMainThread(_OnChangeLocation);
			}

		} while (!IsGetDestination() && !stopMoving);

		if (fighting)
		{
//			Loom.QueueOnMainThread(_OnFighting);
		}
		else
		{
//			_OnGetDestination();
			Loom.QueueOnMainThread(_OnGetDestination);
		}
	}

	void _OnChangeLocation()
	{
		if (!stopMoving && !fighting)
		{
			if (onChangeLocation != null)
			{
				onChangeLocation();
			}
		}
	}

	void _OnGetDestination()
	{
//		stopMoving = true;
//		fighting = false;
		isMoving = false;

		//Enter the city if it is a city
		location.EnterTheCity(destination.GetTargetCity());

		if (onGetDestination != null)
		{
			onGetDestination();
		}
	}

	void _OnFighting()
	{
		if (onFighting != null)
		{
			onFighting();
		}
	}

	bool IsGetDestination()
	{
		if (Vector2.Distance(location.GetPosition(), destination.targetPoint) <= 0.01f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	Fleet otherFleetTemp;

	public void Fight(Fleet otherFleet, bool attack)
	{
//		Debug.Log("FIGHT!");
		fighting = true;
		StopMoving();

		_OnFighting();
//		Loom.QueueOnMainThread(_OnFighting);


		if (attack)
		{
			otherFleetTemp = otherFleet;
			Loom.RunAsync(_BattleSimulation);
		}
	}

	void _BattleSimulation()
	{
		BattleSimulator.SimulateBattle(this, otherFleetTemp);
		otherFleetTemp = null;
	}

	BattleResult battleResult;

	public void OnFinishFighting(BattleResult battleResult)
	{
		//TODO
		fighting = false;
		this.battleResult = battleResult;

		Debug.Log(name + " " + battleResult.status.ToString());

		for (int i = 0; i < battleResult.shipsOnStart.Count; i++)
		{
			battleResult.shipsOnStart[i].OnFinishFighting(battleResult.status, battleResult.GetShipStatus(battleResult.shipsOnStart[i]));

			if (battleResult.GetShipStatus(battleResult.shipsOnStart[i]) == ShipStatus.Crashed)
			{
				RemoveShip(battleResult.shipsOnStart[i]);
			}
		}

		switch (battleResult.status)
		{
			case BattleStatus.Win:
				break;
			case BattleStatus.Defeat:
				break;
			case BattleStatus.EnemyEscaped:
				break;
		}

//		_OnFinishFighting();
		Loom.QueueOnMainThread(_OnFinishFighting);
	}

	void _OnFinishFighting()
	{
		

		if (onFinishFighting != null)
		{
			onFinishFighting(battleResult);
		}
	}


}
