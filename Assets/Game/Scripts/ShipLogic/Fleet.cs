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
	public BaseShip flagship { get; private set; }
	public List<BaseShip> ships { get; private set; }
	FleetStats stats;


	public Location location { get; private set; }
	Vector2 startPosition;
	public ShipTargetPoint destination;
	public System.Action onChangeLocation;
	public System.Action onGetDestination;
	public System.Action onFighting;

	bool stopMoving = false;
	bool fighting = false;


	public bool isFleetEmpty
	{
		get
		{
			return ships.Count == 0 ? true : false;
		}
	}


	public Fleet(BaseCharacter admiral)
	{
		ships = new List<BaseShip>();
		stats = new FleetStats(this);
		location = (Location)admiral.location.Clone();

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
			}
			return true;
		}
		else
		{
			return false;
		}
	}



	public void LeaveTheCity()
	{
		location.LeaveTheCity();
	}

	public bool MoveTo(ShipTargetPoint targetPoint)
	{
		stopMoving = false;
		fighting = false;

		destination = targetPoint;
		startPosition = location.GetPosition();
		Loom.RunAsync(Moving);

		return true;
	}

	void StopMoving()
	{
		stopMoving = true;
	}

	void Moving()
	{
		float elapsedTime = 0;
		int timeTick = 10;
		float distance = Vector2.Distance(startPosition, destination.targetPoint);

		do
		{
			Vector2 newPosition = Vector2.Lerp(startPosition, destination.targetPoint, elapsedTime);
			location.SetPosition(newPosition);

			System.Threading.Thread.Sleep(timeTick);
			elapsedTime += (float)timeTick / 1000.0f * stats.speed / distance * 5;

			Loom.QueueOnMainThread(_OnChangeLocation);


		} while (!IsGetDestination() && !stopMoving);

		if (fighting)
		{
			Loom.QueueOnMainThread(_OnFighting);
		}
		else
		{
			Loom.QueueOnMainThread(_OnGetDestination);
		}
	}

	void _OnChangeLocation()
	{
		if (onChangeLocation != null)
		{
			onChangeLocation();
		}
	}

	void _OnGetDestination()
	{
		stopMoving = true;
		fighting = false;

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


	public void Fight(Fleet otherFleet, bool attack)
	{
		Debug.Log("FIGHT!");
		fighting = true;
		StopMoving();

		if (attack)
		{
			BattleSimulator.SimulateBattle(this, otherFleet);
		}
	}



	public void OnFinishFighting(FleetBattleResult battleResult)
	{
		//TODO
		Debug.Log(battleResult.result.ToString());

		foreach (BaseShip ship in battleResult.shipsOnStart)
		{
			if (battleResult.IsShipAlive(ship))
			{
				//Ship alive
				ship.OnFinishFighting(battleResult.result, true);
			}
			else
			{
				//Ship was destroyed
				ship.OnFinishFighting(battleResult.result, false);
			}
		}

		switch (battleResult.result)
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
