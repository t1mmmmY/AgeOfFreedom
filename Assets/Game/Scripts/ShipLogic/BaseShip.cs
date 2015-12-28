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
		Debug.Log("Move to " + targetPoint.GetTargetCity().ToString());
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
			elapsedTime += (float)timeTick / 1000.0f * stats.speed;

			if (onChangeLocation != null)
			{
				onChangeLocation();
			}

		} while (!IsGetDestination());

		Debug.Log("Get Destination point!");
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
