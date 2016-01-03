using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipsVisualizationManager : BaseSingleton<ShipsVisualizationManager> 
{
	[SerializeField] ShipVisual shipPrefab;

	List<ShipVisual> allShips;

	protected override void Awake()
	{
		allShips = new List<ShipVisual>();

		base.Awake();
	}

	public ShipVisual CreateShip(BaseShip ship, Rect rect)
	{
		GameObject go = GameObject.Instantiate<GameObject>(shipPrefab.gameObject);

		go.transform.parent = this.transform;
		go.transform.position = RandomPositionInRect(rect);

		ShipVisual shipVisual = go.GetComponent<ShipVisual>();
		shipVisual.Init(ship);

		allShips.Add(shipVisual);

		return shipVisual;
	}

	Vector3 RandomPositionInRect(Rect rect)
	{
		return new Vector3(Random.Range(rect.xMin, rect.xMax),
			1,
			Random.Range(rect.yMin, rect.yMax));
	}

}
