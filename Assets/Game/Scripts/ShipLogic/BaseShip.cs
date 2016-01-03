using UnityEngine;
using System.Collections;

public class BaseShip : Logic
{
	public ShipStats stats { get; private set; }
	public Team team { get; private set; }
	public Location location { get; private set; }

	Vector2 startPosition;
	public ShipTargetPoint destination;

	public System.Action onChangeLocation;
	public System.Action onGetDestination;
	public System.Action onFighting;


	bool stopMoving = false;
	bool fighting = false;

	public override void Init()
	{
		base.Init();

		stats = new ShipStats();
	}

	public void Init(ShipStats stats)
	{
		base.Init();

		this.stats = new ShipStats(stats);
	}

	public void ChangeTeam(Team team)
	{
		this.team = team;
		location = new Location();
		location.EnterTheCity(team.captain.location.GetCity());
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


	public void Fight(BaseShip otherShip, bool attack)
	{
		Debug.Log("FIGHT!");
		fighting = true;
		StopMoving();

		if (attack)
		{
			BattleSimulator.SimulateButtle(this, otherShip);
		}
	}

	public void OnFinishFighting(ButtleResult result)
	{
		//TODO
		switch (result)
		{
			case ButtleResult.Win:
				break;
			case ButtleResult.Defeat:
				break;
			case ButtleResult.EnemyEscaped:
				break;
		}
	}

}
