using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FleetsVisualizationManager : BaseSingleton<FleetsVisualizationManager> 
{
	[SerializeField] FleetVisual fleetPrefab;

	List<FleetVisual> allFleets;

	protected override void Awake()
	{
		allFleets = new List<FleetVisual>();

		base.Awake();
	}

	public FleetVisual CreateFleet(Fleet fleet, Rect rect)
	{
		GameObject go = GameObject.Instantiate<GameObject>(fleetPrefab.gameObject);

		go.transform.parent = this.transform;
		go.transform.position = RandomPositionInRect(rect);
		go.name = "Fleet " + GetNumber();

		FleetVisual fleetVisual = go.GetComponent<FleetVisual>();
		fleetVisual.Init(fleet);

		allFleets.Add(fleetVisual);

		return fleetVisual;
	}

	public int GetNumber()
	{
		return allFleets.Count;
	}

	Vector3 RandomPositionInRect(Rect rect)
	{
		return new Vector3(Random.Range(rect.xMin, rect.xMax),
			1,
			Random.Range(rect.yMin, rect.yMax));
	}

}
