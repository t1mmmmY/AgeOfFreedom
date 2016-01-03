using UnityEngine;
using System.Collections;

public class ShipVisual : Visualisation 
{
	BaseShip ship;

	public override void Init(Logic logic)
	{
		base.Init(logic);

		ship = (BaseShip)logic;
//		ship.onChangeLocation += OnChangeLocation;
	}

//	void OnDestroy()
//	{
//		ship.onChangeLocation -= OnChangeLocation;
//	}
//
//	void OnChangeLocation()
//	{
//		Vector3 position = transform.position;
//		position = new Vector3(ship.location.GetPosition().x, position.y, ship.location.GetPosition().y);
//		transform.position = position;
//	}
}
