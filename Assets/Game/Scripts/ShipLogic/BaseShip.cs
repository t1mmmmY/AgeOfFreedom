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

	public bool MoveTo(ShipTargetPoint targetPoint)
	{
		destination = targetPoint;
		startPosition = location.GetPosition();
		Loom.RunAsync(Moving);

		return true;

//		City targetCity = targetPoint.GetTargetCity();
//		if (targetCity != null)
//		{
//			
//			
//			return true;
//		}
//		else
//		{
//			return false;
//		}
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


		} while (!IsGetDestination());

		Loom.QueueOnMainThread(_OnGetDestination);
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
		//Enter the city if it is a city
		location.EnterTheCity(destination.GetTargetCity());

		if (onGetDestination != null)
		{
			onGetDestination();
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

}
